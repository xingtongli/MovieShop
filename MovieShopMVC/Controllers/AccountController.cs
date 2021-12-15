﻿using ApplicationCore.Models;
using ApplicationCore.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            var user = await _accountService.RegisterUser(userRegisterRequestModel);

            if (user == 0)
            {
                // email already exists
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var user = await _accountService.ValidateUser(loginRequestModel);
            if (user == null)
            {
                // hey please chek you email  
                // send message to the view sating please enter correct email/password
            }
            // we need to create a cookie => will have information Claims (MovieShopAuthCookie)
            // claims will have (FirstName, last name, TimeZone)
            // We ussually encrypt the data we store in cookies
            // Cookie Based Authentication
            // Cookie will have expiration time
            // Cookie => Browser => 

            // redirect to home page

            // create claims that we are going to store in the cookie
            var claims = new List<Claim> {

                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.GetValueOrDefault().ToString()),
                new Claim("Language", "English")
            };

            // Identity object that is going to store the claimns and tell it to store those inside the cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create the cookie 
            // ASP.NET(both core and old asp.net) we have one very very important class called HttpContext
            // HttpContext captures everthing about http request
            // what kind of http method GET/POST/PUT, URL, FORM, Cookies, Headers

            // create the cookie, 
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // redirect to my home page
            return LocalRedirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // invalidate the cookie
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
