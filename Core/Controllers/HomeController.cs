using System.Diagnostics;
using System.Threading.Tasks;
using Data.Models;
using Data.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
     public class HomeController : Controller
     {
          [HttpGet]
          public async Task<IActionResult> Index() => View();
     }
}