using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	[Authorize]
	public class GuideController : Controller
	{
		[HttpGet]
		public IActionResult Index() => View();
	}
}