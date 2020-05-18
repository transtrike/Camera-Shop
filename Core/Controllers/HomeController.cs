using System.Collections.Generic;
using System.Diagnostics;
using Camera_Shop.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
     public class HomeController : Controller
     {
          public HomeController() {}

          [HttpGet]
          public IActionResult Index() => View();

          [HttpGet]
          public IActionResult ShowCatalog()
          {
               //List<Camera> cameras = from Cameras in Camera_Shop
               //                         select cameras;
               //IEnumerable<Camera> cameraCollection = cameras.AsEnumerable();

               //return View(cameraCollection);
               return View();
          }
          
          [HttpGet]
          [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
          public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
     }
}