using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class GuideController : Controller
	{
		// GET
		public async Task<IActionResult> Index() => View();
	}
}