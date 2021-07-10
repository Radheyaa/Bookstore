using Bookstore.Models;
using Bookstore.Repository;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;
        private IUserService _userService { get; }

        public AccountController(IAccountRepository accountRepository, IUserService userService)
        {
            _accountRepository = accountRepository;
            _userService = userService;
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(signUpModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(signUpModel);
                }

                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = signUpModel.Email });

            }
            return View(signUpModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.SignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("GetAllBooks", "Book");
                }

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not Allowed");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("GetAllBooks", "Book");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePassword(changePasswordModel);

                if (result.Succeeded) 
                {
                    ViewBag.IsSucceede = true;
                    ModelState.Clear();
                    return View();
                }

                foreach ( var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
                               
            }
            return View(changePasswordModel);
        }


        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                email = email
            };

            if(!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
               var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel emailConfirmModel)
        {

            var user = await _accountRepository.GetUserbyEmailAsync(emailConfirmModel.email);

            if(user != null) 
            {
                if (user.EmailConfirmed)
                {
                    emailConfirmModel.EmailVerified = true;
                    return View();
                }

                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                emailConfirmModel.EmailSent = true;
                ModelState.Clear();
            }
            else 
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(emailConfirmModel);
        }


    }
}
