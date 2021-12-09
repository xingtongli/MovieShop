using ApplicationCore.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Details(int id)
        {
            var movieDetails = _movieService.GetMovieDetailsById(id);
            return View(movieDetails);
        }
    }
}
