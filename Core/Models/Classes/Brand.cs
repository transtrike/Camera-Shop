using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camera_Shop.Models.Classes
{
	[Table("Brands")]
	public class Brand
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		[MinLength(3)]
		public string Name { get; set; }
		
		public ICollection<Camera> Cameras { get; set; }
	}
}