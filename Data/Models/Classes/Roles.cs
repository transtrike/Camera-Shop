using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Models.Classes
{
	[Table("Roles")]
	public class Role : IdentityRole<int>
	{
		public const string Admin = "Admin";
		public const string User = "User";
	}
}