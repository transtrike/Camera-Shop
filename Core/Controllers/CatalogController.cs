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
          public CatalogController()
          {
          }

          //Create
          [HttpGet]
          public IActionResult CreateCamera() => View();
          
          //Read
          [HttpGet]
          public IActionResult ShowCatalog() => View(GetCatalog());
          
          [HttpGet]
          public IActionResult ShowSpecification(int specsId) => View(GetSpecs(specsId));

          //Update
          [HttpGet]
          public IActionResult EditCamera(int cameraId) => View(GetCamera(cameraId));

          [HttpPost]
          public IActionResult EditACamera(Camera camera)
          {
               UpdateCameraInDatabase(camera);
               
               return RedirectToAction("ShowCatalog");
          }
          
          [HttpPost]
          public IActionResult InsertIntoDatabase(CameraDto cameraDto)
          {
               //Both come without ID
               Camera camera = cameraDto.Camera;
               CameraSpecifications specs = cameraDto.CameraSpecifications;

               if(DoesCameraExist(camera) || DoesCameraSpecsExist(specs))
                    return RedirectToAction("CameraExists", cameraDto.Camera);

               InsertIdIntoClasses(cameraDto);
               InsertCameraIntoDatabase(cameraDto);

               return RedirectToAction("ShowCatalog");
          }
          
          //Delete
          [HttpGet]
          public IActionResult DeleteCamera() => View();

          //Validations
          [HttpGet]
          public IActionResult CameraExists(Camera camera) => View(camera);

          /* =-=-=-=-=-=-=-=-=-=-=-=-= */
          /* =-=- Private methods -=-= */
          /* =-=-=-=-=-=-=-=-=-=-=-=-= */

          //Create
          private void InsertCameraIntoDatabase(CameraDto cameraDto)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               //Insert Camera
               {
                    var queryForCameraInsert =
                         $"INSERT INTO \"Cameras\"(\"Id\", \"Brand\", \"Model\", \"SpecificationsId\") " +
                         "VALUES (@id, @brand, @model, @specsId);";
                    var commandForCameraInsert = new NpgsqlCommand(queryForCameraInsert, connection);

                    commandForCameraInsert.Parameters.AddWithValue("@id", cameraDto.Camera.Id);
                    commandForCameraInsert.Parameters.AddWithValue("@brand", $"{cameraDto.Camera.Brand}");
                    commandForCameraInsert.Parameters.AddWithValue("@model", $"{cameraDto.Camera.Model}");
                    if(cameraDto.Camera.Specifications == null)
                         commandForCameraInsert.Parameters.AddWithValue("@specsId", DBNull.Value);
                    else
                         commandForCameraInsert.Parameters.AddWithValue("@specsId",
                              $"{cameraDto.Camera.Specifications.Id}");

                    commandForCameraInsert.ExecuteNonQuery();
               }

               connection.Close();

               if(cameraDto.CameraSpecifications == null)
                    return;

               //Reopen connection

               connection.Open();

               //Insert CameraSpecifications
               {
                    var queryForCameraInsert =
                         $"INSERT INTO \"CameraSpecifications\"(\"Id\", \"Megapixels\", \"BaseISO\", \"MaxISO\") " +
                         "VALUES (@id, @megapixels, @baseISO, @maxISO);";
                    var commandForCameraInsert = new NpgsqlCommand(queryForCameraInsert, connection);

                    commandForCameraInsert.Parameters.AddWithValue("@id", $"{cameraDto.CameraSpecifications.Id}");
                    commandForCameraInsert.Parameters.AddWithValue("@megapixels", $"{cameraDto.CameraSpecifications.Megapixels}");
                    commandForCameraInsert.Parameters.AddWithValue("@baseISO", $"{cameraDto.CameraSpecifications.BaseISO}");
                    commandForCameraInsert.Parameters.AddWithValue("@maxISO", $"{cameraDto.CameraSpecifications.MaxISO}");

                    commandForCameraInsert.ExecuteNonQuery();
               }

               connection.Close();
          }

          private void InsertIdIntoClasses(CameraDto cameraDto)
          {
               var connection = Connection.GetConnection;
               connection.Open();
               var queryForId = $"SELECT MAX(\"Cameras\".\"Id\") FROM \"Cameras\";";
               var commandForId = new NpgsqlCommand(queryForId, connection);
               var reader = commandForId.ExecuteReader();

               reader.Read();
               var maxNumber = reader.GetInt32("max");
               cameraDto.Camera.Id = maxNumber + 1;

               if(cameraDto.Camera.Specifications != null)
                    cameraDto.Camera.Specifications.Id = maxNumber + 1;

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
                    {
                         camera.Specifications = new CameraSpecifications(reader.GetInt32("SpecificationsId"));

                         connection.Close();
                         //Reopen connection
                         connection.Open();
                         
                         query = $"SELECT * FROM \"CameraSpecifications\" " +
                                 $"WHERE \"Id\" = '{camera.Specifications.Id}';";
                         command = new NpgsqlCommand(query, connection);
                         reader = command.ExecuteReader();

                         while(reader.Read())
                         {
                              camera.Specifications.Megapixels = reader.GetInt32("Megapixels");
                              camera.Specifications.BaseISO = reader.GetInt32("BaseISO");
                              camera.Specifications.MaxISO = reader.GetInt32("MaxISO");
                         }
                    }

                    cameras.Add(camera);
               }

               connection.Close();
               return cameras.AsEnumerable();
          }

          private Camera GetCamera(int id)
          {
               var connection = Connection.GetConnection;
               Camera camera = new Camera();
               connection.Open();

               var query = $"SELECT * FROM \"Cameras\" " +
                              $"WHERE \"Id\" = '{id}';";
               var command = new NpgsqlCommand(query, connection);
               
               var reader = command.ExecuteReader();
               
               reader.Read();

               camera.Id = reader.GetInt32("Id");
               camera.Brand = reader.GetString("Brand");
               camera.Model = reader.GetString("Model");

               CameraSpecifications specifications = null;
               var specsFromDb = reader.GetValue("SpecificationsId");
               
               if(specsFromDb == DBNull.Value)
                    camera.Specifications = specifications;
               else
               {
                    specifications = new CameraSpecifications((int)specsFromDb);
                    
                    connection.Close();
                    //Reopen connection
                    connection.Open();

                    query = $"SELECT * FROM \"CameraSpecifications\" " +
                              $"WHERE \"Id\" = '{specifications.Id}';";
                    command = new NpgsqlCommand(query, connection);
                    
                    reader = command.ExecuteReader();
                    
                    reader.Read();

                    specifications.Megapixels = reader.GetInt32("Megapixels");
                    specifications.BaseISO = reader.GetInt32("BaseISO");
                    specifications.MaxISO = reader.GetInt32("MaxISO");

                    camera.Specifications = specifications;
               }

               connection.Close();
               return camera;
          }

          //TODO: Can be optimized
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
          
          //Update
          private void UpdateCameraInDatabase(Camera camera)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               var query = "UPDATE \"Cameras\" " +
                                   "SET " +
                                        $"\"Brand\" = '{camera.Brand}', " +
                                        $"\"Model\" = '{camera.Model}' " +
                                   $"WHERE \"Id\" = '{camera.Id}';"; 
               
               var command = new NpgsqlCommand(query, connection);

               //command.Parameters.AddWithValue("brand", camera.Brand);
               //command.Parameters.AddWithValue("model", camera.Model);

               command.ExecuteNonQuery();
               connection.Close();
               
               if(camera.Specifications == null)
                    return;

               //Reopen connection
               connection.Open();

               query = "UPDATE \"CameraSpecifications\" " +
                       "SET " + 
                         $"\"Megapixel\" = '{camera.Specifications.Megapixels}', " +
                         $"\"BaseISO\" = '{camera.Specifications.BaseISO}', " +
                         $"\"MaxISO\" = '{camera.Specifications.MaxISO}' " +
                       $"WHERE \"Id\" = '{camera.Specifications.Id}';";
               
               //command.Parameters.AddWithValue("@megapixel", $"{camera.Specifications.Megapixels}");
               //command.Parameters.AddWithValue("@baseISO", $"{camera.Specifications.BaseISO}");
               //command.Parameters.AddWithValue("@maxISO", $"{camera.Specifications.MaxISO}");
               
               connection.Close();
          }
          
          //Delete

          //Validations
          private bool DoesCameraExist(Camera camera)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               var query = $"SELECT \"Id\" FROM \"Cameras\" " +
                           $"WHERE \"Brand\" = '@brand' AND \"Model\" = '@model';";
               var command = new NpgsqlCommand(query, connection);

               command.Parameters.AddWithValue("@brand", $"{camera.Brand}");
               command.Parameters.AddWithValue("@model", $"{camera.Model}");

               var reader = command.ExecuteReader();

               bool exists = reader.HasRows;

               connection.Close();
               return exists;
          }

          private bool DoesCameraSpecsExist(CameraSpecifications specs)
          {
               var connection = Connection.GetConnection;
               connection.Open();

               var query = $"SELECT \"Id\" FROM \"Cameras\" " +
                           $"WHERE \"Megapixels\" = '@megapixels' " +
                           $"AND \"BaseISO\" = '@baseISO' " +
                           $"AND \"MaxISO\" = '@maxISO';";
               var command = new NpgsqlCommand(query, connection);

               command.Parameters.AddWithValue("@megapixels", $"{specs.Megapixels}");
               command.Parameters.AddWithValue("@baseISO", $"{specs.BaseISO}");
               command.Parameters.AddWithValue("@maxISO", $"{specs.MaxISO}");

               var reader = command.ExecuteReader();

               bool exists = reader.HasRows;

               connection.Close();
               return exists;
          }
     }
}