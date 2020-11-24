using System;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.ViewModels;
using Camera_Shop.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Camera_Shop.Controllers
{
	public class AccountController : Controller
	{
		private readonly AccountService _service;

		public AccountController(CameraContext context, UserManager<User> userManager,
			SignInManager<User> signInManager, IHttpContextAccessor httpContext)
		{
			this._service = new AccountService(context, userManager, signInManager, httpContext);
		}

		//Create
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await this._service.CreateUserAsync(model);

					if (result.Succeeded)
					{
						await this._service.SignInWithPassAsync(model.UserName,
							model.Password, model.RememberMe);
						return RedirectToAction("Index", "Home");
					}
					else
					{
						List<string> errors = new(1);
						foreach (var err in result.Errors)
							errors.Add(err.Description);

						ArgumentException exception = new("Register failed!");
						exception.Data.Add("Error", errors.ToArray());

						throw exception;
					}
				}
				else
				{
					//Invalid Model state. Repeat Register
					throw new ArgumentException("Problem with Register occurred! Please try again!");
				}
			}
			catch (ArgumentException e)
			{
				ErrorViewModel errorViewModel = new ErrorViewModel();
				errorViewModel.ErrorMessage = e.Message;

				if (e.Data.Count != 0)
					errorViewModel.Errors = e.Data["Error"] as string[];
			
				//Unsuccessful user creation. Show error 
				return View("~/Views/Error/Error.cshtml", errorViewModel);
			}
		}

		//Read
		[HttpGet]
		public IActionResult UserProfile()
		{
			var user = this._service.GetLoggedUser();

			return View(user);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginPost(LoginViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await this._service.SignInWithPassAsync(model.Username,
						model.Password, model.RememberMe);

					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}

					throw new ArgumentException("Login failed!");
				}

				//Invalid Model state. Repeat Login
				throw new ArgumentException(
					"Problem with Login occured! Please try again!");
			}
			catch (ArgumentException e)
			{
				//Unsuccessful user login. Show error
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
			}
		}

		[HttpGet]
		public IActionResult Logout()
		{
			this._service.LogoutAsync();

			return RedirectToAction("Index", "Home");
		}

		//Edit
		[HttpGet]
		public IActionResult Edit()
		{
			return View(this._service.GetLoggedUser());
		}

		[HttpPost]
		public IActionResult EditPost(int id, User user)
		{
			try
			{
				this._service.Update(id, user);

				//TODO: Reload the _Layout page to update the username link

				return RedirectToAction("UserProfile", "Account");
			}
			catch (ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
			}
		}

		//Delete
		[HttpGet]
		public IActionResult Delete()
		{
			return View(this._service.GetLoggedUser());
		}

		[HttpPost]
		public IActionResult DeletePost(int id)
		{
			this._service.Delete(id);

			return RedirectToAction("Index", "Home");
		}
	}
}