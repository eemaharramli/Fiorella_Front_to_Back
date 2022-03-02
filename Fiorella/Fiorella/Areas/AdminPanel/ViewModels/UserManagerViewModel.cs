using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Fiorella.Models.ViewModels
{
    public class UserManagerViewModel
    {
        public List<User> Users { get; set; }
        public List<IdentityRole> IdentityRoles { get; set; }
        public List<IdentityUserRole<string>> IdentityUserRoles { get; set; }
    }
}