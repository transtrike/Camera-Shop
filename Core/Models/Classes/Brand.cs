using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camera_Shop.Models.Classes
{
	[Table("Brands")]
	public class Brand
	{
		private string _name;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		[Required]
		[MinLength(3)]
		public string Name
		{
			get => this._name;
			set
			{
				if(value.Length < 3)
				{
					throw new ArgumentException("Name cannot be less than 3!");
				}
				
				this._name = value;
			}

		}

		public ICollection<Camera> Cameras { get; set; }
	}
}