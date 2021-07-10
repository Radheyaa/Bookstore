﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string getUserId()
        {
            return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated() 
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

    }

}
