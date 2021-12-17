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
        private readonly ILogger<HomeController> _logger;
        public HomeController(IMovieService movieService, IGenreService genreService,ILogger<HomeController> logger)
        {
            _movieService = movieService;
            _genreService = genreService;
            _logger = logger;
        }
        // u1, u2 u3  u100=>
        // thread pool => collection of threads => 100 t1....t100
        // thread => worker in a factory =>
        public async Task<IActionResult> Index()
        {
            //3 way to pass the data/models from controller action methods to views.
            //1, pass the models in the view method
            //2, viewbag
            //3, viewdata
            // I/O bound operation => database calls, file calls, http call
            // CPU bound operation => resizing an image, rading pixel image, calculating Pi number
            // calculating some algorthm 
            var  movieCards = await _movieService.GetHighestGrossingMovies();
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