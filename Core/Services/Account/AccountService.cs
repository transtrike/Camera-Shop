using System.Threading.Tasks;
using Camera_Shop.Database;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Services.Account
{
	public class AccountService
	{
		private readonly CameraContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		
		public AccountService(CameraContext context, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this._context = context;
			this._userManager = userManager;
			this._signInManager = signInManager;
		}
		
		public Task<IActionResult> RegisterUser(RegisterViewModel model)
		{
			return null;
		}
	}
}