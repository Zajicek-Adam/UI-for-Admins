using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class CreateModel : PageModel
    {
        public int TableId { get; set; }

        private readonly IHttpContextAccessor _hca;


        private readonly ApplicationDBContext _context;

        public List<Otp> Otps { get; set; } = new List<Otp>();

        public List<Champion> Champions { get; set; } = new List<Champion>();

        public List<Build>? Builds { get; set; }
        public List<Item>? Items { get; set; }


        [BindProperty]
        public Otp FormOtp { get; set; }

        [BindProperty]
        public Champion FormChampion { get; set; }

        [BindProperty]
        public Item FormItem { get; set; }

        [BindProperty]
        public Build FormBuild { get; set; }

        public CreateModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync()
        {
            Otps = await _context.Otps.ToListAsync();
            Champions = await _context.Champions.ToListAsync();
            Builds = await _context.Build.ToListAsync();
            Items = await _context.Items.ToListAsync();
            var cresponse = _hca.HttpContext.Request;
            if (cresponse.Cookies.Count != null)
            {
                TableId = int.Parse(cresponse.Cookies["TableId"]);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var cresponse = _hca.HttpContext.Request;
            TableId = int.Parse(cresponse.Cookies["TableId"]);
            switch (TableId)
            {
                case 0:
                    if (FormOtp.OtpName == null)
                        FormOtp.OtpName = "";
                    _context.Otps.Add(FormOtp);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Otps");
                case 1:
                    if (FormChampion.ChampionName == null)
                        FormChampion.ChampionName = "";
                    _context.Champions.Add(FormChampion);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Champion");
                case 2:
                    if (FormItem.ItemName == null)
                        FormItem.ItemName = "";
                    if (FormItem.ItemDescription == null)
                        FormItem.ItemDescription = "";
                    _context.Items.Add(FormItem);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Item");
                case 3:
                    if (FormBuild.Description == null)
                        FormBuild.Description = "";
                    _context.Build.Add(FormBuild);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Build");
                default:
                    return RedirectToPage("Index");
            }
        }
    }
}

