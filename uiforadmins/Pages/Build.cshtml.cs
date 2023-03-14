using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class BuildModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        private readonly IHttpContextAccessor _hca;


        public List<Build> Builds { get; set; } = new List<Build>();
        public List<Item> Items { get; set; } = new List<Item>();

        public string BuildSort { get; set; }
        public string PlayrateSort { get; set; }
        public string WinrateSort { get; set; }

        public string CurrentFilter { get; set; }

        public BuildModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            Builds = await _context.Build.ToListAsync();
            Items = await _context.Items.ToListAsync();
            _hca.HttpContext.Response.Cookies.Append("TableId", "3");

            BuildSort = String.IsNullOrEmpty(sortOrder) ? "build" : "";
            PlayrateSort = sortOrder == "playrate" ? "rplayrate" : "playrate";
            WinrateSort = sortOrder == "winrate" ? "rwinrate" : "winrate";
            CurrentFilter = searchString;

            IQueryable<Build> buildIQ = from s in _context.Build select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                buildIQ = buildIQ.Where(s => s.Description.Contains(searchString));
            }
            if (buildIQ.Count() == 0 && String.IsNullOrEmpty(searchString))
            {
                buildIQ = from k in _context.Build select k;
            }

            switch (sortOrder)
            {
                case "build":
                    buildIQ = buildIQ.OrderByDescending(s => s.Description);
                    break;
                case "playrate":
                    buildIQ = buildIQ.OrderBy(s => s.Playrate);
                    break;
                case "rplayrate":
                    buildIQ = buildIQ.OrderByDescending(s => s.Playrate);
                    break;
                case "winrate":
                    buildIQ = buildIQ.OrderBy(s => s.Winrate);
                    break;
                case "rwinrate":
                    buildIQ = buildIQ.OrderByDescending(s => s.Winrate);
                    break;
                default:
                    buildIQ = buildIQ.OrderBy(s => s.Description);
                    break;
            }

            Builds = await buildIQ.AsNoTracking().ToListAsync();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int item)
        {
            Builds = _context.Build.ToList();
            Build _sitem = Builds.First(i => i.BuildId == item);
            _context.Build.Remove(_sitem);
            await _context.SaveChangesAsync();
            return RedirectToPage("Build");
        }
    }
}
