using System;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Camera_Shop.Services.Account
{
	public class AccountService
	{
		private readonly DbRepository<User> _repository;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		
		public AccountService(CameraContext context, UserManager<User> userManager, 
			SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			this._repository = new DbRepository<User>(context);
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._httpContextAccessor = httpContextAccessor;
		}

		//Create 
		public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
		{
			User user = new()
			{
				UserName = model.UserName,
				Email = model.Email
			};
			
			return await this._userManager.CreateAsync(user, model.Password);
		}
		
		//Read
		public async Task<User> GetLoggedUserAsync()
		{
			return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
		}
		
		//Update
		public async Task UpdateAsync(int id, User user)
		{
			//Null check
			if(user == null)
				throw new ArgumentNullException("User cannot be null!");
			
			var currentUser = GetUser(id) ??
				throw new ArgumentNullException("No logged in user!");

			if(await UserExists(user.UserName))
				throw new ArgumentException("Username already exists. Please user a different one!");

			await this._repository.EditAsync(id, user);
		}
		
		//Delete
		public async Task DeleteAsync(int id)
		{
			var user = GetUser(id) ??
				throw new ArgumentNullException("User cannot be null!");
			
			await LogoutAsync();
			await this._repository.DeleteAsync(user);
		}

		//Misc
		public async Task<SignInResult> SignInWithPassAsync(string username,
			string password, bool rememberMe)
		{
			return await this._signInManager
				.PasswordSignInAsync(username, password, rememberMe, false);
		}
		
		public async Task LogoutAsync()
		{
			await this._signInManager.SignOutAsync();
		}

		private async Task<User> GetUser(int id)
		{
			return await this._repository.FindByIdAsync(id);
		}
		
		//Validations
		private async Task<bool> UserExists(string username)
		{
			var prop = typeof(User).GetProperty("UserName");
			return await this._repository.DoesExist(prop, username);
		}
	}
}