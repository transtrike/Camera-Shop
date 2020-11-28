using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class HomeController : Controller
     {
          [HttpGet]
          public async Task<IActionResult> Index() => View();
     }
}