using System.ComponentModel.DataAnnotations;

using Specs = Camera_Shop.Enitites.CameraSpecifications;

namespace Camera_Shop.Enitites
{
     public class Camera
     {
          private int _databaseId;
          private string _name;
          private Specs _cameraSpecs;
          
          public Camera(int databaseId, string name, Specs cameraSpecs)
          {
               this._databaseId = databaseId;
               this._name = name;
               this._cameraSpecs = cameraSpecs;
          }
          
          public Camera() {}

          [Key]
          public string Name { get; set; }
     }
}