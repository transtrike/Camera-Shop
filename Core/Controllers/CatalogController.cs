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
          public IActionResult InsertIntoDatabase(Camera camera)
          {
               if(DoesCameraExist(camera))
                    return RedirectToAction("CameraExists", camera);
               else
                    InsertCameraIntoDatabase(camera);
               
               return RedirectToAction("ShowCatalog");
          }

          [HttpGet]
          public IActionResult CameraExists(Camera camera)
          {
               return View(camera);
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

          /* =-=-=-=-=-=-=-=-=-=-=-=-= */
          /* =-=- Private methods -=-= */
          /* =-=-=-=-=-=-=-=-=-=-=-=-= */
          
          //Create
          //TODO: Add Specs add to Database too
          private void InsertCameraIntoDatabase(Camera camera)
          {
               var connection = Connection.GetConnection;
               connection.Open();
               
               var queryForId = $"SELECT MAX(\"Cameras\".\"Id\") FROM \"Cameras\";";
               var commandForId = new NpgsqlCommand(queryForId, connection);
               var reader = commandForId.ExecuteReader();
               
               reader.Read();
               var maxNumber = reader.GetInt32("max");
               camera.Id = maxNumber + 1;

               connection.Close();
               
               //Reopen connection
               
               connection.Open();
               
               var queryForCameraInsert = $"INSERT INTO \"Cameras\"(\"Id\", \"Brand\", \"Model\", \"SpecificationsId\") " +
                       "VALUES (@id, @brand, @model, @specsId);";
               var commandForCameraInsert = new NpgsqlCommand(queryForCameraInsert, connection);
               
               commandForCameraInsert.Parameters.AddWithValue("@id", camera.Id);
               commandForCameraInsert.Parameters.AddWithValue("@brand", $"{camera.Brand}");
               commandForCameraInsert.Parameters.AddWithValue("@model", $"{camera.Model}");
               if(camera.Specifications == null)
                    commandForCameraInsert.Parameters.AddWithValue("@specsId", DBNull.Value);
               else
                    commandForCameraInsert.Parameters.AddWithValue("@specsId", $"{camera.Specifications.Id}");
               
               commandForCameraInsert.ExecuteNonQuery();
               
               //Add Specs to the database too
               connection.Close();
          }
          
          //Read
          private IEnumerable<Camera> GetCatalog()
          {
               var connection = Connection.GetConnection;
               List<Camera> cameras = new List<Camera>();
               connection.Open();

               var query = "SELECT * FROM \"Cameras\";";
               var command = new NpgsqlCommand(query, connection);
               var reader = command.ExecuteReader();

               while(reader.Read())
               {
                    Camera camera = new Camera();

                    camera.Id = reader.GetInt32("Id");
                    camera.Brand = reader.GetString("Brand");
                    camera.Model = reader.GetString("Model");
                    if(reader.GetValue("SpecificationsId") == DBNull.Value)
                         camera.Specifications = null;
                    else
                         camera.Specifications = new CameraSpecifications(reader.GetInt32("SpecificationsId"));

                    cameras.Add(camera);
               }
               
               connection.Close();
               return cameras.AsEnumerable();
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

          private bool DoesCameraExist(Camera camera)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               var query = $"SELECT \"Id\" FROM \"Cameras\" " +
                              $"WHERE \"Brand\" = '{camera.Brand}' AND \"Model\" = '{camera.Model}';";
               var command = new NpgsqlCommand(query, connection);
               var reader = command.ExecuteReader();

               bool exists = reader.HasRows;
               
               connection.Close();
               return exists;
          }
     }
}