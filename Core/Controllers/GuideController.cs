using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	[Authorize]
	public class GuideController : Controller
	{

		// GET
		public IActionResult Index() => View();
	}
}