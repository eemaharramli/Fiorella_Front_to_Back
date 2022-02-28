using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must contain at least 5 character"), MinLength(5), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}