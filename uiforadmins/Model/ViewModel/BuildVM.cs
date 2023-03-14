using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace uiforadmins.Model
{
    public class BuildVM
    {
        [Key]
        public int BuildId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Winrate { get; set; }

        [Required]
        public int Playrate { get; set; }

    }
}
