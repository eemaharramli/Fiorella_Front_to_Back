using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models.ViewModels
{
    public class ChangePassword
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required, DataType(DataType.Password), MinLength(5)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}