using ApplicationCore.Models;
using ApplicationCore.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var purchase = await _userService.GetUserPurchasedMovies(model);

            if (purchase == null)
            {
                return Unauthorized("purchase failed");
            }
            return Ok(purchase);
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite([FromBody] FavoriteModel model)
        {
            var favorite = await _userService.GetUserFavoritedMovies(model);

            if (favorite == null)
            {
                return Unauthorized("Favorite failed");
            }
            return Ok(favorite);
        }

        [HttpGet]
        [Route("/{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> GetFavorite(int userId, int movieId)
        {
            var favorite = await _userService.GetUserFavoritedMovies(movieId, userId);

            if (favorite == null)
            {
                return NotFound();
            }
            return Ok(favorite);
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> WriteReview([FromBody] ReviewResponseModel model)
        {
            var review = await _userService.WriteReview(model);

            if (!review)
            {
                return Unauthorized("Failed");
            }
            return Ok(review);
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewResponseModel model)
        {
            var review = await _userService.UpdateReview(model);

            if (!review)
            {
                return Unauthorized("Failed");
            }
            return Ok(review);
        }

        [HttpDelete]
        [Route("{userId:int}/movie/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int userId, int movieId)
        {
            var review = await _userService.DeleteReview(movieId, userId);

            if (!review)
            {
                return Unauthorized("Failed");
            }
            return Ok(review);
        }

        [HttpGet]
        [Route("{userid:int}/purchases")]
        public async Task<IActionResult> GetPurchases(int userId)
        {
            var purchases = await _userService.GetUserPurchasedMovies(userId);

            if (!purchases.Any())
            {
                return NotFound();
            }
            return Ok(purchases);
        }

        [HttpGet]
        [Route("{userId:int}/favorites")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _userService.GetUserFavoritedMovies(userId);

            if (!favorites.Any())
            {
                return NotFound();
            }
            return Ok(favorites);
        }

        [HttpGet]
        [Route("{userId:int}/reviews")]
        public async Task<IActionResult> GetReviews(int userId)
        {
            var reviews = await _userService.GetUserReviews(userId);

            if (!reviews.Any())
            {
                return NotFound();
            }
            return Ok(reviews);
        }
    }
}
