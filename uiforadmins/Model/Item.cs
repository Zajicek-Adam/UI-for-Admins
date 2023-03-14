using System.ComponentModel.DataAnnotations;

namespace uiforadmins.Model
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        public string ItemDescription { get; set; } = string.Empty;

        [Required]
        public Rarity ItemRarity { get; set; }

        [Required]
        public int ItemCost { get; set; }

        public List<BuildItem> BuildItems { get; set; }

        public List<Build> Builds { get; set; }
    }
}
