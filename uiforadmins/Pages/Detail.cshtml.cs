using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class DetailModel : PageModel
    {
        private readonly IHttpContextAccessor _hca;
        private readonly ApplicationDBContext _context;

        [TempData]
        public int Stored { get; set; }
        [BindProperty]
        public Build FormBuild { get; set; }

        [BindProperty]
        public int SelectedItemId { get; set; }

        public List<Item> Items { get; set; }

        public List<Build> Builds { get; set; }

        public DetailModel(IHttpContextAccessor hca, ApplicationDBContext context)
        {
            _hca = hca;
            _context = context;
        }

        public async Task OnGetAsync(int buildId)
        {
            Builds = await _context.Build.ToListAsync();
            Items = await _context.Items.ToListAsync();


            FormBuild = Builds.First(i => i.BuildId == buildId);

            _context.Entry(FormBuild).Collection(a => a.Items).Load();

            Stored = buildId;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Build build = _context.Build.ToListAsync().Result.First(i => i.BuildId == FormBuild.BuildId);
            try
            {
                if (build.Items == null)
                {
                    build.Items = new List<Item>() { _context.Items.ToListAsync().Result.First(j => j.ItemId == SelectedItemId) };
                    Console.WriteLine("Empty :((");
                }
                else
                {
                    build.Items.Add(_context.Items.ToListAsync().Result.First(j => j.ItemId == SelectedItemId));
                    Console.WriteLine("Not empty :))))");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("Build");
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("Build");
        }
    }
}
