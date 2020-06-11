using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Camera_Shop.Models
{
	[Table("Users")]
	public class User : IdentityUser
	{
		[Required]
		[MinLength(3)]
		public override string UserName
		{
			get => base.UserName;
			set => base.UserName = value;
		}
	}
}