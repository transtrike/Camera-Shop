using System;
using System.Threading.Tasks;
using Data.Models.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class ErrorController : Controller
	{
		[HttpPost]
		[Route("/Error")]
		public async Task<IActionResult> Error()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var exception = context?.Error;

			ErrorViewModel errorViewModel = new();
			errorViewModel.ErrorMessage = exception.Message;

			if (exception is ArgumentException)
				errorViewModel.ArgumentException = exception as ArgumentException;
			else
				errorViewModel.Exception = exception as Exception;

			return View(errorViewModel);
		}
	}
}