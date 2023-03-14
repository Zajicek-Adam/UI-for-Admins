using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uiforadmins.Model
{
    public class Champion
    {
        [Key]
        public int ChampionId { get; set; }

        [Required]
        public string? ChampionName { get; set; }

        [Required]
        public int ChampionGames { get; set; }

        [Required]
        public int ChampionWinrate { get; set; }

        [Required]
        [ForeignKey("Build")]
        public int BuildId { get; set; }

        public List<Otp>? ChampionOtps { get; set; }
    }
}
