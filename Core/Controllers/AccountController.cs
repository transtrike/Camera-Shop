using System.Collections.Generic;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class AccountController : Controller
	{
		//private readonly AccountService _service;
		private readonly CameraContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		
		public AccountController(CameraContext context, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			//this._service = new AccountService(context, userManager, signInManager);

			this._context = context;
			this._userManager = userManager;
			this._signInManager = signInManager;
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
			if(ModelState.IsValid)
			{
				var user = new User
				{
					UserName = model.UserName,
					Email = model.Email
				};
				var result = await this._userManager.CreateAsync(user, model.Password);

				if(result.Succeeded)
				{
					await this._signInManager.SignInAsync(user, model.RememberMe);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					var errors = new List<string>();

					foreach(var error in result.Errors)
					{
						errors.Add(error.Description);
					}
					
					//Unsuccessful user creation. Show error 
					return View("~/Views/Error/Error.cshtml", 
						new ErrorViewModel("Register failed!", errors.ToArray()));
				}
			}
			
			//Invalid Model state. Repeat Register
			return RedirectToAction("Register");
		}
		
		//Read
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if(ModelState.IsValid)
			{
				var result = await this._signInManager
					.PasswordSignInAsync(model.Username, model.Password,
						model.RememberMe, false);

				if(result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}

				//Unsuccessful user login. Show error
				return View("~/Views/Error/Error.cshtml", 
					new ErrorViewModel("Login error!"));
			}

			//Invalid Model state. Repeat Login
			return RedirectToAction("Login");
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await this._signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}