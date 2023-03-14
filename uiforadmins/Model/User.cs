using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace uiforadmins.Model
{
    public class User : IdentityUser<int>
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; } 
    }
}
