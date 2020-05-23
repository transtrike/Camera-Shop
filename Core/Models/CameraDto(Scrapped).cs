using System.ComponentModel.DataAnnotations;
using Specs = Camera_Shop.Models.CameraSpecifications;

namespace Camera_Shop.Models
{
     public class CameraDto
     {
          public int Id { get; set; }
          
          [Required]
          public string Model { get; set; }
          
          public Specs Specs { get; set; }
     }
}