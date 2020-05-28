using System;
using Camera_Shop.Models;
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
               this._localCamera.Id = value;

               if(this._localCamera.Id == value)
                    Assert.Pass($"Camera Id set to {value}");
               else
                    Assert.Fail();
          }
     }
}