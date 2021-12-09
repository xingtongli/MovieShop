using ApplicationCore.ServicesInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public HomeController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            //3 way to pass the data/models from controller action methods to views.
            //1, pass the models in the view method
            //2, viewbag
            //3, viewdata
            
            var movieCards = _movieService.GetHighestGrossingMovies();
            return View(movieCards);
        }

        public IActionResult Privacy()
        {
            // for testing , to see if i am getting list of genres
            var genres = _genreService.GetAllGenres();
            return View(genres);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}