using System;
using Camera_Shop.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Camera_Shop.Tests.CameraModel
{
     public class CameraTests
     {
          private Camera _localCamera;

          [SetUp]
          public void SetUp()
          {
               this._localCamera = new Camera(0, "Canon", "T7i", 24, 100, 25400);
          }
          
          [Test]
          public void TestShouldAlwaysPass()
          {
               Assert.Pass();
          }

          [Test]
          [TestCase(1)]
          [TestCase(Int32.MaxValue)]
          [TestCase(Int32.MinValue)]
          public void CameraIdShouldBeSetCorrectly(int value)
          {
               Camera camera = new Camera();
               camera.Id = value;

               if(camera.Id == value)
                    Assert.Pass($"Camera Id set to {value}");
               else
                    Assert.Fail($"Unsuccessful set of Camera Id to {value}");
          }

          [Test]
          [TestCase("Canon")]
          [TestCase("Sony")]
          [TestCase("Panasonic")]
          [TestCase("Fujifilm")]
          public void CameraBrandShouldBeSetCorrectly(string brand)
          {
               Camera camera = new Camera();
               camera.Brand = brand;

               if(camera.Brand == brand)
                    Assert.Pass($"Camera Brand set to {brand}");
               else
                    Assert.Fail($"Unsuccessful set of Camera Brand to {brand}");
          }

          [Test]
          [TestCase(null)]
          public void CameraShouldNotSetBrandIfNull(string brand)
          {
               Camera camera = new Camera();
               Assert.Catch<ArgumentException>(() =>  camera.Brand = brand);
          }
          
          [Test]
          [TestCase("T7i")]
          [TestCase("a6600")]
          [TestCase("D5500")]
          [TestCase("600D")]
          public void CameraModelShouldBeSetCorrectly(string model)
          {
               Camera camera = new Camera();
               camera.Brand = model;
               
               if(camera.Model == model)
                    Assert.Pass($"Camera Id set to {model}");
               else
                    Assert.Fail($"Unsuccessful set of Camera Id to {model}");    
          }

          [Test]
          [TestCase(null)]
          public void CameraShouldNotSetModelIfNull(string brand)
          {
               Camera camera = new Camera();
               Assert.Catch<ArgumentException>(() =>  camera.Model = brand);
          }
          
          
     }
}