using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace uiforadmins.Model
{
    public class ItemVM
    {
        [DisplayName("Item")]
        [Required]
        public string ItemName { get; set; } = string.Empty;

        [DisplayName("Description")]
        [Required]
        public string ItemDescription { get; set; } = string.Empty;

        [DisplayName("Rarity")]
        [Required]
        public Rarity ItemRarity { get; set; }

        [DisplayName("Cost")]
        [Required]
        public int ItemCost { get; set; }
    }
}
