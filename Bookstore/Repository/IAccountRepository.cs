using Bookstore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpModel signupmodel);
        Task<SignInResult> SignInAsync(SignInModel signInmodel);

        Task SignOutAsync();

        Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<ApplicationUser> GetUserbyEmailAsync(string email);
    }
}