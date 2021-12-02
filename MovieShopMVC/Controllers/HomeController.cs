using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private MovieService _movieService;
        public HomeController()
        {
            _movieService = new MovieService();
        }

        public IActionResult Index()
        {
            //3 way to pass the data/models from controller action methods to views.
            //1, pass the models in the view method
            //2, viewbag
            //3, viewdata
            var movieCards = _movieService.GetHighestGrossingMovies();
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