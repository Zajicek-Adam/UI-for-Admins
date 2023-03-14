using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace uiforadmins.Model
{
    public class Build
    {
        [Key]
        public int BuildId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Winrate { get; set; }

        [Required]
        public int Playrate { get; set; }
        public List<Champion> Champions { get; set; }

        [Required]
        public List<BuildItem> BuildItems { get; set; }
        public List<Item> Items { get; set; }

    }
}
