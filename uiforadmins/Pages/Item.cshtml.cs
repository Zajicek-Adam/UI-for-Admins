using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class ItemModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        private readonly IHttpContextAccessor _hca;


        public List<Build> Builds { get; set; } = new List<Build>();
        public List<Item> Items { get; set; } = new List<Item>();
        public string ItemSort { get; set; }
        public string RaritySort { get; set; }
        public string CostSort { get; set; }

        public string CurrentFilter { get; set; }

        public ItemModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            Builds = await _context.Build.ToListAsync();
            Items = await _context.Items.ToListAsync();
            _hca.HttpContext.Response.Cookies.Append("TableId", "2");

            ItemSort = String.IsNullOrEmpty(sortOrder) ? "item" : "";
            CostSort = sortOrder == "cost" ? "rcost" : "cost";
            RaritySort = sortOrder == "rarity" ? "rrarity" : "rarity";

            CurrentFilter = searchString;

            IQueryable<Item> itemIQ = from s in _context.Items select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                itemIQ = itemIQ.Where(s => s.ItemName.Contains(searchString));
            }
            if (itemIQ.Count() == 0 && String.IsNullOrEmpty(searchString))
            {
                itemIQ = from k in _context.Items select k;
            }

            switch (sortOrder)
            {
                case "item":
                    itemIQ = itemIQ.OrderByDescending(s => s.ItemName);
                    break;
                case "cost":
                    itemIQ = itemIQ.OrderBy(s => s.ItemCost);
                    break;
                case "rcost":
                    itemIQ = itemIQ.OrderByDescending(s => s.ItemCost);
                    break;
                case "rarity":
                    itemIQ = itemIQ.OrderBy(s => s.ItemRarity);
                    break;
                case "rrarity":
                    itemIQ = itemIQ.OrderByDescending(s => s.ItemRarity);
                    break;
                default:
                    itemIQ = itemIQ.OrderBy(s => s.ItemName);
                    break;
            }

            Items = await itemIQ.AsNoTracking().ToListAsync();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int item)
        {
            Items = _context.Items.ToList();
            Item _sitem = Items.First(i => i.ItemId == item);
            _context.Items.Remove(_sitem);
            await _context.SaveChangesAsync();
            return RedirectToPage("Item");
        }
    }
}
