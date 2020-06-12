using System.Diagnostics;
using Camera_Shop.Models;
using Camera_Shop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
     public class HomeController : Controller
     {
          public HomeController() {}

          [HttpGet]
          public IActionResult Index() => View();

          [HttpGet]
          [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
          public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
     }
}