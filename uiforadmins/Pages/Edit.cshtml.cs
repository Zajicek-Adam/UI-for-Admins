using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel;
using uiforadmins.Data;
using uiforadmins.Model;

namespace uiforadmins.Pages
{
    public class EditModel : PageModel
    {
        [TempData]
        public int TableId { get; set; }

        private readonly ApplicationDBContext _context;

        private readonly IHttpContextAccessor _hca;



        [BindProperty]
        public Otp FormOtp { get; set; }

        [BindProperty]
        public Champion FormChampion { get; set; }

        [BindProperty]
        public Item FormItem { get; set; }

        [BindProperty]
        public Build FormBuild { get; set; }

        public List<Otp>? Otps { get; set; }
        public List<Build>? Builds { get; set; }
        public List<Item>? Items { get; set; }

        public List<Champion> Champions { get; set; } = new List<Champion>();

        public int Stored { get; set; }

        public EditModel(ApplicationDBContext context, IHttpContextAccessor hca)
        {
            _context = context;
            _hca = hca;
        }
        public async Task OnGetAsync(int id)
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

            switch (TableId)
            {
                case 0:
                    FormOtp = Otps.First(i => i.OtpId == id);
                    break;
                case 1:
                    FormChampion = Champions.First(i => i.ChampionId == id);
                    break;
                case 2:
                    FormItem = Items.First(i => i.ItemId == id);
                    break;
                case 3:
                    FormBuild = Builds.First(i => i.BuildId == id);
                    break;
            }
            Stored = id;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (FormOtp.OtpId != 0)
            {
                Otp _otp = _context.Otps.ToListAsync().Result.First(i => i.OtpId == FormOtp.OtpId);
                if(!(FormOtp.OtpName == null))
                {
                    _otp.OtpName = FormOtp.OtpName;
                }
                else{
                    _otp.OtpName = "";
                }

                _otp.OtpRank = FormOtp.OtpRank;
                _otp.ChampionId = FormOtp.ChampionId;
                _otp.GamesPlayed = FormOtp.GamesPlayed;
                _otp.Winrate = FormOtp.Winrate;
                await _context.SaveChangesAsync();
                return RedirectToPage("Otps");
            }
            else if (FormChampion.ChampionId != 0)
            {
                Champion champ = _context.Champions.ToListAsync().Result.First(i => i.ChampionId == FormChampion.ChampionId);

                if (!(FormChampion.ChampionName == null))
                {
                    champ.ChampionName = FormChampion.ChampionName;
                }
                else
                {
                    champ.ChampionName = "";
                }
                champ.ChampionWinrate = FormChampion.ChampionWinrate;
                champ.ChampionGames = FormChampion.ChampionGames;
                champ.BuildId = FormChampion.BuildId;
                champ.ChampionOtps = FormChampion.ChampionOtps;
                await _context.SaveChangesAsync();
                return RedirectToPage("Champion");
            }
            else if (FormItem.ItemId != 0)
            {
                Item item = _context.Items.ToListAsync().Result.First(i => i.ItemId == FormItem.ItemId);
                if (!(FormItem.ItemName == null))
                {
                    item.ItemName = FormItem.ItemName;
                }
                else
                {
                    item.ItemName = "";
                }
                if (!(FormItem.ItemDescription == null))
                {
                    item.ItemDescription = FormItem.ItemDescription;
                }
                else
                {
                    item.ItemDescription = "";
                }
                item.ItemCost = FormItem.ItemCost;
                item.ItemRarity = FormItem.ItemRarity;
                item.Builds = FormItem.Builds;
                item.BuildItems = FormItem.BuildItems;
                await _context.SaveChangesAsync();
                return RedirectToPage("Item");
            }
            else if (FormBuild.BuildId != 0)
            {
                Build build = _context.Build.ToListAsync().Result.First(i => i.BuildId == FormBuild.BuildId);
                if (!(FormBuild.Description == null))
                {
                    build.Description = FormBuild.Description;
                }
                else
                {
                    build.Description = "";
                }
                build.Champions = FormBuild.Champions;
                build.Winrate = FormBuild.Winrate;
                build.Playrate = FormBuild.Playrate;
                build.BuildItems = FormBuild.BuildItems;
                build.Items = FormBuild.Items;
                await _context.SaveChangesAsync();
                return RedirectToPage("Build");
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}

