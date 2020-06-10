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

		/*private int _id;
		private string _username;
		private string _name;
		private int _age;
		private string _password;
		private string _email;

		public User(string email, string password)
		{
			this.Email = email;
			this.Password = password;
		}
		
		public User(int id, string username, string email, string name, int age, string password)
		{
			this.Id = id;
			this.Username = username;
			this.Email = email;
			this.Name = name;
			this.Age = age;
			this.Password = password;
		}

		[Key]
		public int Id
		{
			get => this._id;
			set => this._id = value;
		}

		[Required]
		[MinLength(3)]
		public string Username
		{
			get => this._username;
			set => this._username = value;
		}

		[Required]
		public string Email
		{
			get => this._email;
			set => this._email = value;
		}

		[Required]
		[Range(12, 150)]
		public int Age
		{
			get => this._age;
			set => this._age = value;
		}

		[Required]
		[MinLength(3)]
		public string Password
		{
			get => this._password;
			set => this._password = value;
		}*/
	}
}