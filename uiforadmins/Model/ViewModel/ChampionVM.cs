using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uiforadmins.Model
{
    public class ChampionVM
    {
        [DisplayName("Champion")]
        [Required]
        public string? ChampionName { get; set; }

        [DisplayName("Games")]
        [Required]
        public int ChampionGames { get; set; }

        [DisplayName("Winrate")]
        [Required]
        public int ChampionWinrate { get; set; }

        [DisplayName("Build")]
        [Required]
        [ForeignKey("Build")]
        public int BuildId { get; set; }
    }
}
