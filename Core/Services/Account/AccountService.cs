using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.ViewModels;
using Camera_Shop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Camera_Shop.Services.Account
{
	public class AccountService
	{
		private readonly CameraRepository<User> _repository;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IHttpContextAccessor _httpContext;
		
		public AccountService(CameraContext context, UserManager<User> userManager, 
			SignInManager<User> signInManager, IHttpContextAccessor httpContext)
		{
			this._repository = new CameraRepository<User>(context);
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
		public async Task<User> GetLoggedUser()
		{
			return await _userManager.GetUserAsync(_httpContext.HttpContext.User);
		}
		
		//Update
		public void Update(int id, User user)
		{
			//Null check
			if(user == null)
			{
				throw new ArgumentNullException("User cannot be null!");
			}
			
			var currentUser = GetUser(id);
			
			if(currentUser != null 
			   && user != null
			   && !UserExists(user.UserName))
			{
				this._repository.Edit(id, user);
			}
			else
			{
				throw new ArgumentException("User is either null or already exists!");
			}
		}
		
		//Delete
		public void Delete(int id)
		{
			var user = GetUser(id);
			//Null check
			if(user == null)
			{
				throw new ArgumentNullException("User cannot be null!");
			}
			
			LogoutAsync();

			this._repository.Delete(user);
		}

		//Misc
		public async Task<SignInResult> SignInWithPassAsync(string username,
			string password, bool rememberMe)
		{
			return await this._signInManager
				.PasswordSignInAsync(username, password,
					rememberMe, false);
		}
		
		public async void LogoutAsync()
		{
			await this._signInManager.SignOutAsync();
		}

		private User GetUser(int id)
		{
			return this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);
		}
		
		//Validations
		private bool UserExists(string username)
		{
			return _repository.QueryAll()
				.Any(x => x.UserName == username);
		}
		
		public ErrorViewModel CollectErrors(IdentityResult result)
		{
			var errors = new List<string>();
			
			foreach(var error in result.Errors)
			{
				errors.Add(error.Description);
			}

			var errorViewModel = new ErrorViewModel();
			errorViewModel.Errors = errors.ToArray();
			
			return errorViewModel;
		}
	}
}