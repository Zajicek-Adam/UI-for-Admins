using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uiforadmins.Model;

namespace uiforadmins.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Build> Build { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Champion>()
               .HasData(new Champion { ChampionId = 1, ChampionWinrate = 55, ChampionGames = 40, ChampionName = "Aurelion Sol", ChampionOtps = new List<Otp>(), BuildId = 2 },
                        new Champion { ChampionId = 2, ChampionWinrate = 51, ChampionGames = 22, ChampionName = "Skarner", ChampionOtps = new List<Otp>(), BuildId = 1 });

            builder.Entity<Item>()
                .HasData(new Item { ItemId = 1, ItemRarity = Rarity.Mythic, ItemName = "Kraken Slayer", ItemDescription = "Anti-Tank AD Mythic item", ItemCost = 3400, BuildItems = new List<BuildItem>(), Builds = new List<Build>() },
                         new Item { ItemId = 2, ItemRarity = Rarity.Mythic, ItemName = "Galeforce", ItemDescription = "Execute AD Mythic item", ItemCost = 3400, BuildItems = new List<BuildItem>(), Builds = new List<Build>() },
                         new Item { ItemId = 3, ItemRarity = Rarity.Mythic, ItemName = "Shieldbow", ItemDescription = "Survivability AD Mythic item", ItemCost = 3400, BuildItems = new List<BuildItem>(), Builds = new List<Build>() });

            builder.Entity<Otp>()
                .HasData(new Otp { OtpId = 1, OtpName = "Flamekite", Winrate = 90, GamesPlayed = 800, OtpRank = Rank.Master, ChampionId = 2 });

            builder.Entity<Build>()
                .HasData(new Build { BuildId = 1, Description = "AP roaming", BuildItems = new List<BuildItem>(), Champions = new List<Champion>(), Items = new List<Item>(), Playrate = 35, Winrate = 55 },
                         new Build { BuildId = 2, Description = "Off-tank CDR" ,BuildItems = new List<BuildItem>(), Champions = new List<Champion>(), Items = new List<Item>(), Playrate = 15, Winrate = 53 });

            builder.Entity<Item>()
               .HasMany(c => c.Builds)
               .WithMany(c => c.Items)
               .UsingEntity<BuildItem>(
                   j => j
                       .HasOne(cb => cb.Build)
                       .WithMany(b => b.BuildItems)
                       .HasForeignKey(cb => cb.BuildId),
                   j => j
                       .HasOne(cb => cb.Item)
                       .WithMany(c => c.BuildItems)
                       .HasForeignKey(cb => cb.ItemId),
                   j =>
                   {
                       j.HasKey(b => new { b.ItemId, b.BuildId });
                   }
               );
        }
    }
}
