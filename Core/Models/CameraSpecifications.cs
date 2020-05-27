using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camera_Shop.Models
{
     [Table("CameraSpecifications")]
     public class CameraSpecifications
     {
          private int _id;
          private decimal _megapixels;
          private int _baseISO;
          private int _maxISO;

          public CameraSpecifications(int id) => this.Id = id;
               
          public CameraSpecifications() { }

          [Key]
          [Required]
          public int Id
          {
               get => this._id;
               set
               {
                    if(value < 0)
                         throw new ArgumentException("Id cannot be less than 0!");
                    
                    this._id = value;
               }
          }

          [Required]
          public decimal Megapixels
          {
               get => this._megapixels;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Megapixels cannot be less than or equal to 0!");
                    
                    this._megapixels = value;
               }

          }

          [Required]
          public int BaseISO
          {
               get => this._baseISO;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Base ISO cannot be less than or equal to 0!");
                    
                    this._baseISO = value;
               }
          }

          [Required]
          public int MaxISO
          {
               get => this._maxISO;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Max ISO cannot be less than or equal to 0!");
                    if(value < this._baseISO)
                         throw new ArgumentException("Max ISO cannot be less than base ISO!");
                    
                    this._maxISO = value;
               }
          }
     }
}