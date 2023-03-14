using System.ComponentModel.DataAnnotations;

namespace uiforadmins.Model
{
    public class BuildItem
    {
        [Key]
        public int BuildId { get; set; }
        [Key]
        public int ItemId { get; set; }
        public Item Item { get; set; } 
        public Build Build { get; set; }
    }
}
