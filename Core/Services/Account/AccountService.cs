using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Camera_Shop.Models.Classes;
using Camera_Shop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Camera_Shop.Services.Account
{
	public class AccountService
	{
		private readonly CameraContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IHttpContextAccessor _httpContext;
		
		public AccountService(CameraContext context, UserManager<User> userManager, 
			SignInManager<User> signInManager, IHttpContextAccessor httpContext)
		{
			this._context = context;
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._httpContext = httpContext;
		}

		//Create 
		public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
		{
			var user = new User
			{
				UserName = model.UserName,
				Email = model.Email
			};
			
			return await this._userManager.CreateAsync(user, model.Password);
		}
		
		//Read
		public User GetLoggedUser()
		{
			var userId = _httpContext.HttpContext.User
				.FindFirst(ClaimTypes.NameIdentifier).Value;
			
			return GetUser(userId);
		}
		
		//Update
		public void Update(string id, User user)
		{
			var currentUser = GetUser(id);
			
			if(currentUser != null 
			   && user != null
			   && !UserExists(user.UserName))
			{
				currentUser.UserName = user.UserName;
				currentUser.Email = user.Email;
				this._context.SaveChanges();
			}
			else
			{
				throw new ArgumentException("User is either null or already exists!");
			}
		}
		
		//Delete
		public void DeleteUser(string id)
		{
			Logout();

			var user = GetUser(id);
			this._context.Users.Remove(user);
			this._context.SaveChangesAsync();
		}

		//Misc
		public async Task<SignInResult> SignInWithPassAsync(string username,
			string password, bool rememberMe)
		{
			var result = await this._signInManager
				.PasswordSignInAsync(username, password,
					rememberMe, false);
			
			return result;
		}
		
		public async void Logout()
		{
			await this._signInManager.SignOutAsync();
		}

		private User GetUser(string id)
		{
			var user = this._context.Users
				.FirstOrDefault(x => x.Id == id);

			return user;
		}
		
		//Validations
		private bool UserExists(string username)
		{
			var exists = this._context.Users
				.Any(x => x.UserName == username);

			return exists;
		}
		
		public ErrorViewModel CollectErrors(IdentityResult result)
		{
			var errors = new List<string>();
			
			foreach(var error in result.Errors)
			{
				errors.Add(error.Description);
			}

			var errorViewModel = new ErrorViewModel();
			errorViewModel.ErrorMessages = errors.ToArray();
			
			return errorViewModel;
		}
	}
}