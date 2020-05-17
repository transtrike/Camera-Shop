using System;
using Specs = Camera_Shop.Models.CameraSpecifications;

namespace Camera_Shop.Models
{
     public class Camera
     {
          private int _id;
          private string _model;
          private Specs _specs;

          public Camera(int id, string model, Specs specs)
          {
               this._id = id;
               this._model = model;
               this._specs = specs;
          }

          // For Reflection purposes
          public Camera() { }

          //TODO: set int from database
          private int Id
          {
               get => this._id;
               set => this._id = value;
          }

          public string Model
          {
               get => this._model;
               set
               {
                    if(value == null)
                         throw new ArgumentException("Model can't be null!");
                    
                    this._model = value;
               }
          }

          public Specs Specifications
          {
               get => this._specs;
               set => this._specs = value;
          }
     }
}