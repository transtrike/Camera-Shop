using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}