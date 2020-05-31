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
		[HttpGet]
		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult CreatePost(Camera camera)
		{
			try
			{
				//TODO: Figure out how to validate product without id and possibly
				//without going trough every property. If needed, use Reflections

				Insert(camera);
				
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

		//Update
		[HttpGet]
		public IActionResult Edit(int id) => View(GetCamera(id));

		[HttpPost]
		public IActionResult EditPost(int id, Camera camera)
		{
			try
			{
				if(!DoesCameraExist(id))
					throw new ArgumentException($"Camera {camera.Brand}: {camera.Model} does not exist!");
				
				Update(id, camera);

				return RedirectToAction("ShowCatalog");
			}
			catch(Exception e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Delete
		[HttpGet]
		public IActionResult Delete(int id) => View(GetCamera(id));
		
		[HttpPost]
		public IActionResult DeletePost(int id)
		{
			try
			{
				DeleteById(id);
				
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
		
		//Errors
		[HttpGet]
		public IActionResult Error(string errorMessage)
		{
			ErrorViewModel error = new ErrorViewModel();
			error.ErrorMessage = errorMessage;

			return View("Error", error);
		}
		

		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
		/* =-=- Private methods -=-= */
		/* =-=-=-=-=-=-=-=-=-=-=-=-= */
	
		//Create
		private void Insert(Camera camera)
		{
			camera.Id = NewId();
			
			this._context.Cameras.Add(camera);
			this._context.SaveChanges();
		}

		private int NewId()
		{
			var getCameras = 
					from c in this._context.Cameras
					select c;

			int maxId = 0;
			
			if(getCameras.Any())
				maxId = (from c in this._context.Cameras
					select c)
					.Max(x => x.Id) + 1;

			return maxId;
		}

		//Read
		private IEnumerable<Camera> GetCatalog() => new List<Camera>(this._context.Cameras).AsEnumerable();

		private Camera GetCamera(int id)
		{
			var cameras = 
				from s in this._context.Cameras
				where s.Id == id
				select s;

			return cameras.FirstOrDefault();
		}
	
		//Update
		private void Update(int id, Camera camera)
		{
			var cameraToModify = (
				from c in this._context.Cameras
				where c.Id == id
				select c).FirstOrDefault();

			foreach(var property in camera.GetType().GetProperties())
				property.SetValue(cameraToModify, property.GetValue(camera));

			this._context.SaveChanges();
		}

		//Delete
		private void DeleteById(int id)
		{
			var cameraToDelete = (
					from c in this._context.Cameras
					where c.Id == id
					select c).FirstOrDefault();
			
			this._context.Remove(cameraToDelete);
			this._context.SaveChanges();
		}

		//Validations
		private bool DoesCameraExist(int id)
		{
			var doesCameraExist =
				from c in this._context.Cameras
				where c.Id == id
				select c;

			bool exists = doesCameraExist.Count() != 0;

			return exists;
		}
	}
}