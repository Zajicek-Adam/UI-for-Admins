using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uiforadmins.Model
{
    public class OtpVM
    {
        [DisplayName("Name")]
        [Required]
        public string OtpName { get; set; } = string.Empty;

        [DisplayName("Champion")]
        [Required]
        [ForeignKey("Champion")]
        public int  ChampionId { get; set; }

        [DisplayName("Rank")]
        [Required]
        public Rank OtpRank { get; set; }

        [DisplayName("Games")]
        [Required]
        public int GamesPlayed { get; set; }

        [DisplayName("Winrate")]
        [Required]
        public int Winrate { get; set; }
    }
}
