using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class ChampionModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        [BindProperty]
        public List<Otp> Otps { get; set; } = new List<Otp>();

        public List<Champion> Champions { get; set; } = new List<Champion>();

        private readonly IHttpContextAccessor _hca;

        public List<Build> Builds { get; set; } = new List<Build>();

        public string ChampSort { get; set; }
        public string BuildSort { get; set; }
        public string GamesSort { get; set; }

        public string CurrentFilter { get; set; }


        public ChampionModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            Otps = await _context.Otps.ToListAsync();
            Champions = await _context.Champions.ToListAsync();
            Builds = await _context.Build.ToListAsync();
            _hca.HttpContext.Response.Cookies.Append("TableId", "1");

            ChampSort = String.IsNullOrEmpty(sortOrder) ? "champ" : "";
            BuildSort = sortOrder == "build" ? "rbuild" : "build";
            GamesSort = sortOrder == "games" ? "rgames" : "games";

            IQueryable<Champion> champsIQ = from s in _context.Champions select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                champsIQ = champsIQ.Where(s => s.ChampionName.Contains(searchString));
                Champions = champsIQ.ToListAsync().Result;
            }
            if (champsIQ.Count() == 0 && String.IsNullOrEmpty(searchString))
            {
                champsIQ = from k in _context.Champions select k;
            }

            switch (sortOrder)
            {
                case "champ":
                    champsIQ = champsIQ.OrderByDescending(s => s.ChampionName);
                    break;
                case "games":
                    champsIQ = champsIQ.OrderBy(s => s.ChampionGames);
                    break;
                case "rgames":
                    champsIQ = champsIQ.OrderByDescending(s => s.ChampionGames);
                    break;
                default:
                    champsIQ = champsIQ.OrderBy(s => s.ChampionName);
                    break;
            }
            List<Champion> _champs = await champsIQ.AsNoTracking().ToListAsync();
            //?XD KPOP BANGARANG CODE
            for (int i = 0; i < Champions.Count; i++)
            {
                if (Champions[i].ChampionId == _champs[i].ChampionId)
                {
                    _champs[i].ChampionOtps = Champions[i].ChampionOtps;
                }
                for (int j = Champions.Count - 1; j >= 0; j--)
                {
                    if (Champions[j].ChampionId == _champs[i].ChampionId)
                    {
                        _champs[i].ChampionOtps = Champions[j].ChampionOtps;
                    }
                }
            }
            Champions = _champs;
        }
        public async Task<IActionResult> OnGetDeleteAsync(int item)
        {
            Champions = _context.Champions.ToList();
            Champion _sitem = Champions.First(i => i.ChampionId == item);
            _context.Champions.Remove(_sitem);
            await _context.SaveChangesAsync();
            return RedirectToPage("Champion");
        }
    }
}
