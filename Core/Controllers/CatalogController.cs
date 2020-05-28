using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Camera_Shop.Database;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Camera_Shop.Controllers
{
	public class CatalogController : Controller
	{
		//Create
		[HttpGet]
		public IActionResult CreateCamera() => View();

		//Read
		[HttpGet]
		public IActionResult ShowCatalog() => View(GetCatalog());

		//Update
		[HttpGet]
		public IActionResult EditCamera(int cameraId) => View(GetCamera(cameraId));

		[HttpPost]
		public IActionResult EditACamera(Camera camera)
		{
			UpdateCamera(camera);

			return RedirectToAction("ShowCatalog");
		}

		[HttpPost]
		public IActionResult InsertIntoDatabase(Camera camera)
		{
			//Both come without ID
			if(DoesCameraExist(camera))
				return RedirectToAction("CameraExists", camera);

			InsertId(camera);
			InsertCameraIntoDatabase(camera);

			return RedirectToAction("ShowCatalog");
		}

		//Delete
		[HttpGet]
		public IActionResult DeleteCamera(int cameraId) => View(GetCamera(cameraId));
		
		[HttpPost]
		public IActionResult DeleteACamera(Camera camera)
		{
			DeleteCameraFromDatabase(camera);
			
			return RedirectToAction("ShowCatalog");
		}

		//Validations
		[HttpGet]
		public IActionResult CameraExists(Camera camera) => View(camera);

		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
		/* =-=- Private methods -=-= */
		/* =-=-=-=-=-=-=-=-=-=-=-=-= */

		//Create
		private void InsertCameraIntoDatabase(Camera camera)
		{
			var connection = Connection.GetConnection;
			connection.Open();

			var queryForCameraInsert = $"INSERT INTO \"Cameras\"(\"Id\", \"Brand\", \"Model\", \"Megapixels\", \"BaseISO\", \"MaxISO\") " +
			                           $"VALUES ({camera.Id}, '{camera.Brand}', '{camera.Model}', '{camera.Megapixels}', '{camera.BaseISO}', '{camera.MaxISO}');";
			var commandForCameraInsert = new NpgsqlCommand(queryForCameraInsert, connection);
			
			//commandForCameraInsert.Parameters.AddWithValue("@specsId", DBNull.Value);
			//commandForCameraInsert.Parameters.AddWithValue("@specsId", $"{cameraDto.Camera.Specifications.Id}");
			//commandForCameraInsert.Parameters.AddWithValue("@id", $"{cameraDto.Camera.Id}");
			//commandForCameraInsert.Parameters.AddWithValue("@brand", $"{cameraDto.Camera.Brand}");
			//commandForCameraInsert.Parameters.AddWithValue("@model", $"{cameraDto.Camera.Model}");

			commandForCameraInsert.ExecuteNonQuery();
			connection.Close();
		}

		private void InsertId(Camera camera)
		{
			var connection = Connection.GetConnection;
			connection.Open();
			
			var queryForId = $"SELECT MAX(\"Cameras\".\"Id\") FROM \"Cameras\";";
			var commandForId = new NpgsqlCommand(queryForId, connection);
			var reader = commandForId.ExecuteReader();

			reader.Read();

			var number = 0;
			var value = reader.GetValue("max");
			if(value != DBNull.Value)
				number = reader.GetInt32("max") + 1;
			camera.Id = number;

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
				camera.Megapixels = reader.GetDecimal("Megapixels");
				camera.BaseISO = reader.GetInt32("BaseISO");
				camera.MaxISO = reader.GetInt32("MaxISO");

				cameras.Add(camera);
			}

			connection.Close();
			return cameras.AsEnumerable();
		}

		private Camera GetCamera(int id)
		{
			var connection = Connection.GetConnection;
			connection.Open();

			var query = $"SELECT * FROM \"Cameras\" " +
			            $"WHERE \"Id\" = '{id}';";
			var command = new NpgsqlCommand(query, connection);
			var reader = command.ExecuteReader();

			reader.Read();

			Camera camera = new Camera();
			camera.Id = reader.GetInt32("Id");
			camera.Brand = reader.GetString("Brand");
			camera.Model = reader.GetString("Model");
			camera.Megapixels = reader.GetDecimal("Megapixels");
			camera.BaseISO = reader.GetInt32("BaseISO");
			camera.MaxISO = reader.GetInt32("MaxISO");
			
			connection.Close();
			return camera;
		}

		//Update
		private void UpdateCamera(Camera camera)
		{
			var connection = Connection.GetConnection;
			connection.Open();

			var query = "UPDATE \"Cameras\" " +
			            "SET " +
			            	$"\"Brand\" = '{camera.Brand}', " +
			            	$"\"Model\" = '{camera.Model}', " +
			            	$"\"Megapixels\" = '{camera.Megapixels}', " +
			            	$"\"BaseISO\" = '{camera.BaseISO}', " +
			            	$"\"MaxISO\" = '{camera.MaxISO}' " +
			            $"WHERE \"Id\" = '{camera.Id}';";

			var command = new NpgsqlCommand(query, connection);

			//command.Parameters.AddWithValue("brand", camera.Brand);
			//command.Parameters.AddWithValue("model", camera.Model);

			command.ExecuteNonQuery();
			connection.Close();
		}

		//Delete
		private void DeleteCameraFromDatabase(Camera camera)
		{
			var connection = Connection.GetConnection;
			connection.Open();

			var query = $"DELETE FROM \"Cameras\" WHERE \"Id\" = {camera.Id};";
			var command = new NpgsqlCommand(query, connection);
			command.ExecuteNonQuery();

			connection.Close();
		}
		
		//Validations
		private bool DoesCameraExist(Camera camera)
		{
			var connection = Connection.GetConnection;
			connection.Open();

			var query = $"SELECT \"Id\" FROM \"Cameras\" " +
			            $"WHERE \"Brand\" = '{camera.Brand}' " +
			            $"AND \"Model\" = '{camera.Model}' " +
			            $"AND \"Megapixels\" = '{camera.Megapixels}' " +
			            $"AND \"BaseISO\" = '{camera.BaseISO}' " +
			            $"AND \"MaxISO\" = '{camera.MaxISO}';";
			var command = new NpgsqlCommand(query, connection);

			//command.Parameters.AddWithValue("@brand", $"{camera.Brand}");
			//command.Parameters.AddWithValue("@model", $"{camera.Model}");

			var reader = command.ExecuteReader();
			bool exists = reader.HasRows;

			connection.Close();
			return exists;
		}
	}
}