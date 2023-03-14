using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uiforadmins.Model
{
    public class Otp
    {
        [Key]
        public int OtpId { get; set; }

        [Required]
        public string OtpName { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Champion")]
        public int  ChampionId { get; set; }

        [Required]
        public Rank OtpRank { get; set; }
        [Required]
        public int GamesPlayed { get; set; }
        [Required]
        public int Winrate { get; set; }
    }
}
