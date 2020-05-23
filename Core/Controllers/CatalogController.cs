using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
     public class CatalogController : Controller 
     {
          public CatalogController() { }
          
          [HttpGet]
          public IActionResult ShowCatalog()
          {
               //List<Camera> cameras = from Cameras in Camera_Shop
               //                       select cameras;
               //IEnumerable<Camera> cameraCollection = cameras.AsEnumerable();

               //return View(cameraCollection);
               return View();
          }
     }
}