using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Specs = Camera_Shop.Models.CameraSpecifications;

namespace Camera_Shop.Models
{
     [Table("Cameras")]
     public class Camera
     {
          private int _id;
          private string _model;
          private Specs _specs;

          public Camera(int id, string model, Specs specs)
          {
               this.Id = id;
               this.Model = model;
               this.Specifications = specs;
               
               if (specs != null)
                    this._specs.Id = this._id;
          }

          // For Reflection purposes
          public Camera() { }

          [Key]
          public int Id
          {
               get => this._id;
               private set => this._id = value;
          }

          [NotNull]
          public string Model
          {
               get => this._model;
               private set
               {
                    if(value == null)
                         throw new ArgumentException("Model can't be null!");
                    
                    this._model = value;
               }
          }

          [AllowNull]
          public Specs Specifications
          {
               get => this._specs;
               set => this._specs = value;
          }
     }
}