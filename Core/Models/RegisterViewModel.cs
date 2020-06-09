using System.ComponentModel.DataAnnotations;

namespace Camera_Shop.Models
{
	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }
		
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", 
			ErrorMessage = "Password and Confirm Password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}