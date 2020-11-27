using System;
using System.Threading.Tasks;
using Data.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class ErrorController : Controller
	{
		[Route("/Error")]
		public async Task<IActionResult> Error(ArgumentException e)
		{
			System.Console.WriteLine(e.Message);
			foreach (var item in e.Data.Values)
				System.Console.WriteLine(item);

			return View(new ErrorViewModel(e));
		}
	}
}