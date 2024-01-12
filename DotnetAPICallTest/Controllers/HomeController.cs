using DotnetAPICallTest.Models;
using DotnetAPIStartupTesting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetAPICallTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly APIDataSets _data;

        public HomeController(ILogger<HomeController> logger, APIDataSets data)
        {
            _data = data;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = _data.ToDo;
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