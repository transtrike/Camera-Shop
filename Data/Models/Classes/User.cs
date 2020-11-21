using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Models.Classes
{
	[Table("Users")]
	public class User : IdentityUser
	{
		[Required]
		[MinLength(3)]
		[Display(Name = "Username")]
		public override string UserName
		{
			get => base.UserName;
			set
			{
				if(value.Length < 3)
				{
					throw new ArgumentException("Username cannot be shorter than 3!");
				}
				
				base.UserName = value;
			}
		}
	}
}