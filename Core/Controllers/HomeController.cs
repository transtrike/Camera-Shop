using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class HomeController : Controller
     {
          [HttpGet]
          public IActionResult Index() => View();
     }
}