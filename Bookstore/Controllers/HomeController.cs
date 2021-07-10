using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService UserService;

        public IEmailService _emailService { get; }

        public HomeController(ILogger<HomeController> logger , IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            UserService = userService;
            _emailService = emailService;
        }

        public async Task<ViewResult> Index()
        {
            //UserEmailOptions userEmailOptions = new UserEmailOptions()
            //{
            //    ToEmail = new List<string>() { "test@gmail.com " },
            //    PlaceHolder = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{username}}","Prashant Shetye")
            //    }
            //};

            //await _emailService.SendTestEmail(userEmailOptions);

            //ViewBag.userId = UserService.getUserId();
            //ViewBag.isAuthenticated = UserService.IsAuthenticated();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
