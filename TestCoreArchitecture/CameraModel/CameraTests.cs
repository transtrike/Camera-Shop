using System;
using Camera_Shop.Models;
using NUnit.Framework;

namespace Camera_Shop.Tests.CameraModel
{
     public class CameraServiceTests
     {
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
               
               var ex = Assert.Throws<ArgumentException>(() =>  camera.Brand = brand);

               Assert.AreEqual(ex.Message, "Brand can't be null!");
          }
          
          [Test]
          [TestCase("T7i")]
          [TestCase("a6600")]
          [TestCase("D5500")]
          [TestCase("600D")]
          public void CameraModelShouldBeSetCorrectly(string model)
          {
               Camera camera = new Camera();
               camera.Model = model;
               
               if(camera.Model == model)
                    Assert.Pass($"Camera Id set to {model}");
               else
                    Assert.Fail($"Unsuccessful set of Camera Id to {model}");    
          }

          [Test]
          [TestCase(null)]
          public void CameraShouldNotSetModelIfNull(string model)
          {
               Camera camera = new Camera();
               
               var ex = Assert.Throws<ArgumentException>(() =>  camera.Model = model);

               Assert.AreEqual(ex.Message, "Model can't be null!");
          }

          [Test]
          [TestCase(50)]
          [TestCase(24)]
          [TestCase(1)]
          public void CameraMegapixelsShouldBeSetCorrectly(decimal megapixels)
          {
               Camera camera = new Camera();
               camera.Megapixels = megapixels;

               if(camera.Megapixels == megapixels)
                    Assert.Pass("Megapixels set correctly");
               else
                    Assert.Pass("Megapixels can not be set correctly");
          }

          [Test]
          [TestCase(64)]
          [TestCase(100)]
          [TestCase(200)]
          public void CameraShouldSetBaseISOCorrectly(int baseISO)
          {
               Camera camera = new Camera();

               camera.BaseISO = baseISO;

               if(camera.BaseISO == baseISO)
                    Assert.Pass("Base ISO sets correctly");
               else
                    Assert.Fail("Base ISO can not set correctly");
          }
          
          [Test]
          [TestCase(31)]
          [TestCase(0)]
          [TestCase(-30)]
          public void CameraShouldNotSetBaseISOIfBelow32(int baseISO)
          {
               Camera camera = new Camera();

               var ex = Assert.Throws<ArgumentException>(() => camera.BaseISO = baseISO);
               
               Assert.AreEqual(ex.Message, "Base ISO cannot be less than or equal to 0!");
          }
          
          [Test]
          [TestCase(64)]
          [TestCase(100)]
          [TestCase(200)]
          public void CameraShouldSetMaxISOCorrectly(int maxISO)
          {
               Camera camera = new Camera();

               camera.MaxISO = maxISO;

               if(camera.MaxISO == maxISO)
                    Assert.Pass("Max ISO sets correctly");
               else
                    Assert.Fail("Max ISO can not set correctly");
          }
          
          [Test]
          [TestCase(31)]
          [TestCase(0)]
          [TestCase(-30)]
          public void CameraShouldNotSetMaxISOIfBelow32(int maxISO)
          {
               Camera camera = new Camera();

               var ex = Assert.Throws<ArgumentException>(() => camera.MaxISO = maxISO);
               
               Assert.AreEqual(ex.Message, "Max ISO cannot be less than or equal to 0!");
          }
     }
}