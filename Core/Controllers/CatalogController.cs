using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Camera_Shop.Database;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Camera_Shop.Controllers
{
     public class CatalogController : Controller
     {
          public CatalogController() { }

          [HttpGet]
          public IActionResult ShowCatalog()
          {
               return View(GetCatalog());
          }

          [HttpGet]
          public IActionResult CreateCamera()
          {
               return View();
          }
          
          [HttpPost]
          public IActionResult ProcessCamera()
          {
               //Create the camera from properties array and put it in database
               //return the view from the ShowCameras() 
               //Not correct current return View()
               return RedirectToAction("ShowCatalog");
          }

          [HttpGet]
          public IActionResult EditCamera()
          {
               return View();
          }

          [HttpGet]
          public IActionResult DeleteCamera()
          {
               return View();
          }

          [HttpGet]
          public IActionResult ShowSpecification(int specsId)
          {
               return View(GetSpecs(specsId));
          }

          //Private methods
          private IEnumerable<Camera> GetCatalog()
          {
               var connection = Connection.GetConnection;
               List<Camera> cameras = new List<Camera>();
               connection.Open();

               var query = "SELECT * FROM \"Cameras\";";
               var command = new NpgsqlCommand(query, connection);
               var reader = command.ExecuteReader();

               while(reader.HasRows)
               {
                    reader.Read();
                    Camera camera = new Camera();

                    camera.Id = reader.GetInt32("Id");
                    camera.Brand = reader.GetString("Brand");
                    camera.Model = reader.GetString("Model");
                    if(reader.GetValue("SpecificationsId") == DBNull.Value)
                         camera.Specifications = null;
                    else
                         camera.Specifications = new CameraSpecifications(reader.GetInt32("SpecificationsId"));

                    cameras.Add(camera);
                    reader.NextResult();
               }
               
               connection.Close();
               return cameras.AsEnumerable();
          }
          
          private Camera CreateACamera()
          {
               var connection = Connection.GetConnection;
               connection.Open();
               Camera camera = new Camera();
               
               
               
               connection.Close();
               return camera;
          }

          //TODO: Can be optimized
          //TODO: Test to see if works, after implementing the CREATE from CRUD
          private CameraSpecifications GetSpecs(int id)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               var query = "SELECT * FROM \"CameraSpecifications\" " +
                           "WHERE \"Id\" = (@id);";
               var command = new NpgsqlCommand(query, connection);
               command.Parameters.AddWithValue("id", id);
               NpgsqlDataReader result = command.ExecuteReader();

               CameraSpecifications specs = new CameraSpecifications(id);
               PropertyInfo[] properties = specs.GetType().GetProperties();
               Type specsType = specs.GetType();

               if(!result.HasRows)
                    specs = null;
               else
                    while(result.Read())
                         for(int i = 0; i < result.FieldCount; i++)
                              specsType.GetProperty($"{properties[i].Name}")
                                   .SetValue(specs, result.GetValue(i), null);

               connection.Close();
               return specs;
          }
     }
}