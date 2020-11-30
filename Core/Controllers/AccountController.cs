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
		public async Task<IActionResult> RegisterPost(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityResult result = await this._service.CreateUserAsync(model);

				if (result.Succeeded)
				{
					await this._service.SignInWithPassAsync(model.UserName,
						model.Password, model.RememberMe);
					
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ArgumentException argumentException = new("Registration failed!");
					argumentException.Data.Add("Error", result.Errors);

					throw argumentException;
				}
			}
			else
			{
				//Invalid Model state. Repeat Register
				throw new ArgumentException("Problem with registration occurred! Please fill all reqired fields!");
			}
		}

		//Read
		[HttpGet]
		[Authorize(Policy = "Logged")]
		public async Task<IActionResult> UserProfile()
		{
			return View(await this._service.GetLoggedUserAsync());
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginPost(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await this._service.SignInWithPassAsync(model.Username,
					model.Password, model.RememberMe);

				if (result.Succeeded)
					return RedirectToAction("Index", "Home");
				else
					throw new ArgumentException("Login failed! Please try again.");
			}
			else
				//Invalid Model state. Repeat Login
				throw new ArgumentException(
					"Problem with Login occurred! Please try again!");

		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await this._service.LogoutAsync();

			return RedirectToAction("Index", "Home");
		}

		//Edit
		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			return View(await this._service.GetLoggedUserAsync());
		}

		[HttpPost]
		public async Task<IActionResult> EditPost(int id, User user)
		{
			await this._service.UpdateAsync(id, user);

			//TODO: Reload the _Layout page to update the username link

			return RedirectToAction("UserProfile", "Account");
		}

		//Delete
		[HttpGet]
		public async Task<IActionResult> Delete()
		{
			return View(await this._service.GetLoggedUserAsync());
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await this._service.DeleteAsync(id);

			return RedirectToAction("Index", "Home");
		}
	}
}