using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Models.Classes
{
	[Table("Roles")]
	public class Role : IdentityRole<int>
	{
	}
}