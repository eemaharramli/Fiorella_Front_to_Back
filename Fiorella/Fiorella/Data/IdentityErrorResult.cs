using Microsoft.AspNetCore.Identity;

namespace Fiorella.Data
{
    public class IdentityErrorResult : IdentityErrorDescriber 
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "Email",
                Description = "There is already an existing user with your chosen email. Please input a different one email"
            };
        }
        
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "Username",
                Description = "There is already an existing user with your chosen username. Please input a different one username"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = "Password",
                Description = "The require minimal digit count for password is 5"
            };
        }
    }
}