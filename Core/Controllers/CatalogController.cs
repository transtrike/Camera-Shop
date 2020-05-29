using System;
using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Camera_Shop.Controllers
{
	public class CatalogController : Controller
	{
		private readonly CameraContext _context;
		
		public CatalogController(CameraContext context) => this._context = context;
		
		
		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
		/* =-=- Public methods =-=-= */
		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
 
		
		//Create
		[HttpPost]
		public IActionResult InsertIntoDatabase(Camera camera)
		{
			try
			{
				//Both come without ID
				if(DoesCameraExist(camera))
					return RedirectToAction("CameraExists", camera);

				camera = InsertId(camera);
				InsertCameraIntoDatabase(camera);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(ArgumentException e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Read
		[HttpGet]
		public IActionResult ShowCatalog() => View(GetCatalog());

		[HttpGet]
		public IActionResult CreateCamera() => View();

		[HttpGet]
		public IActionResult EditCamera(int cameraId) => View(GetCamera(cameraId));

		[HttpGet]
		public IActionResult DeleteCamera(int cameraId) => View(GetCamera(cameraId));

		//Update
		[HttpPost]
		public IActionResult CreateACamera(Camera camera)
		{
			try
			{
				if(DoesCameraExist(camera))
					throw new ArgumentException($"Camera {camera.Brand}: {camera.Model} exists!");

				InsertIntoDatabase(camera);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(ArgumentException e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			} 
		}
		
		[HttpPost]
		public IActionResult EditACamera(Camera camera)
		{
			try
			{
				if(!DoesCameraExist(camera))
					throw new ArgumentException($"Camera {camera.Brand}: {camera.Model} does not exist!");
				
				UpdateCamera(camera);

				return RedirectToAction("ShowCatalog");
			}
			catch(Exception e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Delete
		[HttpPost]
		public IActionResult DeleteACamera(Camera camera)
		{
			try
			{
				DeleteCameraFromDatabase(camera);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(Exception e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Validations
		[HttpGet]
		public IActionResult CameraExists(Camera camera) => View(camera);
		

		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
		/* =-=- Private methods -=-= */
		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
		

		//Create
		private void InsertCameraIntoDatabase(Camera cameraToInsert)
		{
			/*var connection = Connection.GetConnection;
			connection.Open();

			Camera camera = new Camera();
			camera.Id = cameraToInsert.Id;
			camera.Brand = cameraToInsert.Brand;
			camera.Model = cameraToInsert.Model;
			camera.Megapixels = cameraToInsert.Megapixels;
			camera.BaseISO = cameraToInsert.BaseISO;
			camera.MaxISO = cameraToInsert.MaxISO;

			var query =
				$"INSERT INTO \"Cameras\"(\"Id\", \"Brand\", \"Model\", \"Megapixels\", \"BaseISO\", \"MaxISO\") " +
				$"VALUES ({camera.Id}, '{camera.Brand}', '{camera.Model}', '{camera.Megapixels}', '{camera.BaseISO}', '{camera.MaxISO}');";
			var command = new NpgsqlCommand(query, connection);

			/*var query = $"INSERT INTO \"Cameras\"(\"Id\", \"Brand\", \"Model\", \"Megapixels\", \"BaseISO\", \"MaxISO\") " +
							$"VALUES ('@id', '@brand', '@model', '@megapixels', '@baseiso', '@maxiso');";
			var command = new NpgsqlCommand(query, connection);

			command.Parameters.AddWithValue("id", $"{camera.Id}");
			command.Parameters.AddWithValue("brand", $"{camera.Brand}");
			command.Parameters.AddWithValue("model", $"{camera.Model}");
			command.Parameters.AddWithValue("megapixel", $"{camera.Megapixels}");
			command.Parameters.AddWithValue("baseiso", $"{camera.BaseISO}");
			command.Parameters.AddWithValue("maxiso", $"{camera.MaxISO}");

			int affectedRows = command.ExecuteNonQuery();
			connection.Close();*/

			this._context.Cameras.Add(cameraToInsert);
			this._context.SaveChanges();
		}

		private Camera InsertId(Camera camera)
		{
			/*var connection = Connection.GetConnection;
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

			connection.Close();*/

			var maxInt = 
					from c in this._context.Cameras
					select c;

			if(maxInt.Any() == false)
				camera.Id = 0;
			else
				camera.Id = 
					(from c in this._context.Cameras
					select c).Max(x => x.Id) + 1;

			return camera;
		}

		//Read
		private IEnumerable<Camera> GetCatalog() => new List<Camera>(this._context.Cameras).AsEnumerable();

		private Camera GetCamera(int id)
		{
			/*var connection = Connection.GetConnection;
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
			return camera;*/
			
			var cameras = 
				from s in this._context.Cameras
				where s.Id == id
				select s;

			return cameras.FirstOrDefault();
		}

		//Update
		private void UpdateCamera(Camera camera)
		{
			/*var connection = Connection.GetConnection;
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
			connection.Close();*/

			var cameraToModify = (
				from c in this._context.Cameras
				where c.Id == camera.Id
				select c).FirstOrDefault();

			foreach(var property in camera.GetType().GetProperties())
				property.SetValue(cameraToModify, property.GetValue(camera));

			this._context.SaveChanges();
		}

		//Delete
		private void DeleteCameraFromDatabase(Camera camera)
		{
			/*var connection = Connection.GetConnection;
			connection.Open();

			var query = $"DELETE FROM \"Cameras\" WHERE \"Id\" = {camera.Id};";
			var command = new NpgsqlCommand(query, connection);
			command.ExecuteNonQuery();

			connection.Close();*/
			
			var cameraToDelete = (
					from c in this._context.Cameras
					where c.Id == camera.Id
					select c).FirstOrDefault();
			
			this._context.Remove(cameraToDelete);
			this._context.SaveChanges();
		}

		//Validations
		private bool DoesCameraExist(Camera camera)
		{
			/*var connection = Connection.GetConnection;
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
			return exists;*/

			var doesCameraExist =
				(from c in this._context.Cameras
				where c.Brand == camera.Brand &
				      c.Model == camera.Model
				select c).ToList();

			bool exists = doesCameraExist.Count != 0;

			return exists;
		}

		//Errors
		[HttpGet]
		public IActionResult Error(string errorMessage)
		{
			ErrorViewModel error = new ErrorViewModel();
			error.ErrorMessage = errorMessage;

			return View("Error", error);
		}
	}
}