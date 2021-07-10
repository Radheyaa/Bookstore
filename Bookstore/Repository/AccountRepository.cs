using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Bookstore.Services;
using Microsoft.Extensions.Configuration;

namespace Bookstore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _iconfiguration;

        public IUserService _userService { get; }

        public AccountRepository(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager, IUserService userService ,
            IEmailService emailService, IConfiguration iconfiguration )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _iconfiguration = iconfiguration; 
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel signupmodel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signupmodel.FirstName,
                LastName = signupmodel.LastName,
                Email = signupmodel.Email,
                UserName = signupmodel.Email
            };

            var result = await _userManager.CreateAsync(user, signupmodel.Password);
            if (result.Succeeded) 
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }


        public async Task<ApplicationUser> GetUserbyEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task<SignInResult> SignInAsync(SignInModel signInmodel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInmodel.Email, signInmodel.Password, signInmodel.RememberMe, false);
            
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();            
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.getUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            return result;
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user , string token)
        {
            string appDomain = _iconfiguration.GetSection("Application:AppDomain").Value;
            string ConfirmationLink = _iconfiguration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                ToEmail = new List<string>() { user.Email },
                PlaceHolder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{username}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + ConfirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailforEmailConfirmation(userEmailOptions);
        }
    }
}
