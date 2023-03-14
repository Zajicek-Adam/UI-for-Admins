using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using uiforadmins.Data;
using uiforadmins.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace uiforadmins.Pages
{
    public class OtpsModel : PageModel
    {
        private readonly IHttpContextAccessor _hca;

        private readonly ApplicationDBContext _context;

        [BindProperty]
        public List<Otp> Otps { get; set; } = new List<Otp>();

        public List<Champion> Champions { get; set; } = new List<Champion>();

        public string OTPSort { get; set; }
        public string GamesSort { get; set; }

        public string CurrentFilter { get; set; }

        public OtpsModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            Otps = await _context.Otps.ToListAsync();
            Champions = await _context.Champions.ToListAsync();
            _hca.HttpContext.Response.Cookies.Append("TableId", "0");

            OTPSort = String.IsNullOrEmpty(sortOrder) ? "otpname" : "";
            GamesSort = sortOrder == "games" ? "rgames" : "games";

            CurrentFilter = searchString;

            IQueryable<Otp> otpsIQ = from s in _context.Otps select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                otpsIQ = otpsIQ.Where(s => s.OtpName.Contains(searchString));
            }
            if (otpsIQ.Count() == 0 && String.IsNullOrEmpty(searchString))
            {
                otpsIQ = from k in _context.Otps select k;
            }

            switch (sortOrder)
            {
                case "otpname":
                    otpsIQ = otpsIQ.OrderByDescending(s => s.OtpName);
                    break;
                case "games":
                    otpsIQ = otpsIQ.OrderBy(s => s.GamesPlayed);
                    break;
                case "rgames":
                    otpsIQ = otpsIQ.OrderByDescending(s => s.GamesPlayed);
                    break;
                default:
                    otpsIQ = otpsIQ.OrderBy(s => s.OtpName);
                    break;
            }

            Otps = await otpsIQ.AsNoTracking().ToListAsync();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int item)
        {
            Otps = _context.Otps.ToList();
            Otp _sitem = Otps.First(i => i.OtpId == item);
            _context.Otps.Remove(_sitem);
            await _context.SaveChangesAsync();
            return RedirectToPage("Otps");
        }
    }
}
