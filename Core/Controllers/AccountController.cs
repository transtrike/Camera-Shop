using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
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

		[HttpGet]
		public IActionResult UserProfile(string id)
		{
			return View(GetUser(id));
		}
		
		//Edit
		[HttpGet]
		public IActionResult Edit(string id)
		{
			return View(GetUser(id));
		}

		[HttpPost]
		public IActionResult EditPost(string id, User user)
		{
			if(!UserExists(id))
			{
				return View("Error", new ErrorViewModel("Person already exists!"));
			}
			
			Update(id, user);
			RelogUser(user);
			
			return RedirectToAction("UserProfile", "Account", id);
		}
		
		//Delete
		[HttpGet]
		public IActionResult Delete(string id)
		{
			return View(GetUser(id));
		}
		
		[HttpPost]
		public IActionResult DeletePost(string id)
		{
			var user = (from u in this._context.Users
				where u.Id == id
				select u).FirstOrDefault();

			this._context.Users.Remove(user);

			return RedirectToAction("Index", "Home");
		}

		//Private methods
		private User GetUser(string id)
		{
			var user = (from u in this._context.Users
				where u.Id == id
				select u).FirstOrDefault();
			
			return user;
		}

		private void Update(string id, User user)
		{
			var cameraToModify = (
				from u in this._context.Users
				where u.Id == id
				select u).FirstOrDefault();

			foreach(var property in user.GetType().GetProperties())
				property.SetValue(cameraToModify, property.GetValue(user));

			this._context.SaveChanges();
		}

		private void RelogUser(User user)
		{
			//SignOut using id
			//SignIn using user
			
		}
		
		private bool UserExists(string id)
		{
			var user = from u in this._context.Users
				where u.Id == id
				select u;

			bool exists = user.Any();
				
			return exists;
		}
	}
}