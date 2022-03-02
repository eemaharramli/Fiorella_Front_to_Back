using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Fiorella.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Fullname { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}