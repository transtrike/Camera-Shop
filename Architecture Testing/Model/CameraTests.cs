using System;
using Camera_Shop.Models.Classes;
using NUnit.Framework;

namespace Camera_Shop.Tests.Model
{
     public class CameraTests
     {
          [Test]
          public void TestShouldAlwaysPass()
          {
               Assert.Pass();
          }

          [Test]
          [TestCase(null)]
          public void CameraShouldNotSetBrandIfNull(Brand brand)
          {
               Camera camera = new Camera();

               Assert.Throws<ArgumentException>(
                    () =>  camera.Brand = brand );
          }

          [Test]
          [TestCase(null)]
          public void CameraShouldNotSetModelIfNull(string model)
          {
               Camera camera = new Camera();
               
               Assert.Throws<ArgumentException>(
                    () =>  camera.Model = model);
          }
          
          [Test]
          [TestCase("DT")]
          public void CameraShouldNotSetModelIfLengthLessThanThree(string model)
          {
               Camera camera = new Camera();
               
               Assert.Throws<ArgumentException>(
                    () =>  camera.Model = model);
          }

          [Test]
          [TestCase(0)]
          public void CameraShouldNotSetMegapixelsIfValueLessThanOne(decimal megapixels)
          {
               Camera camera = new Camera();

               Assert.Throws<ArgumentException>(
                    () => camera.Megapixels = megapixels);
          }

          [Test]
          [TestCase(31)]
          public void CameraShouldNotSetBaseISOIfBelow32(int baseISO)
          {
               Camera camera = new Camera();

               Assert.Throws<ArgumentException>(
                    () => camera.BaseISO = baseISO);
          }

          [Test]
          [TestCase(31)]
          public void CameraShouldNotSetMaxISOIfBelow32(int maxISO)
          {
               Camera camera = new Camera();

               Assert.Throws<ArgumentException>(
                    () => camera.MaxISO = maxISO);
          }
     }
}