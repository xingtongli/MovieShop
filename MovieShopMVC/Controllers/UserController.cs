﻿using ApplicationCore.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // var isAuthenticated = User.Identity.IsAuthenticated;

            // go to User Service and call User Repository and get the Movies Purchased by user who loged in
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // pass above user id to USer Service
            var purchases = await _userService.GetUserPurchasedMovies(userId);
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var favorites = await _userService.GetUserFavoritedMovies(userId);
            return View(favorites);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _userService.GetUserDetails(userId);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _userService.GetUserDetails(userId);
            var editProfile = await _userService.EditUserProfile(profile);
            return RedirectToAction("Updataed Profile");
        }
    }
}
