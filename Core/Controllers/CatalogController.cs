using System;
using Microsoft.AspNetCore.Mvc;

using Camera_Shop.Database;
using Camera_Shop.Models;
using Camera_Shop.Services.Catalog;

namespace Camera_Shop.Controllers
{
	public class CatalogController : Controller
	{
		private readonly CatalogService _service;
		
		public CatalogController(CameraContext context)
		{
			this._service = new CatalogService(context);
		}

		//Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreatePost(Camera camera)
		{
			try
			{
				//TODO: Figure out how to validate product without id and possibly
				//without going trough every property. If needed, use Reflections
				
				//!!! Use Model as unique property for checking

				this._service.Insert(camera);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(ArgumentException e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			} 
		}
		
		//Read
		[HttpGet]
		public IActionResult ShowCatalog()
		{
			return View(this._service.GetCatalog());
		}

		//Update
		[HttpGet]
		public IActionResult Edit(int id)
		{
			return View(this._service.GetCamera(id));
		}

		[HttpPost]
		public IActionResult EditPost(int id, Camera camera)
		{
			try
			{
				if(!this._service.DoesCameraExist(id))
					throw new ArgumentException($"Camera {camera.Brand}: {camera.Model} does not exist!");
				
				this._service.Update(id, camera);

				return RedirectToAction("ShowCatalog");
			}
			catch(Exception e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Delete
		[HttpGet]
		public IActionResult Delete(int id)
		{
			return View(this._service.GetCamera(id));
		}

		[HttpPost]
		public IActionResult DeletePost(int id)
		{
			try
			{
				this._service.Delete(id);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(Exception e)
			{
				return RedirectToAction("Error", "Catalog", Error(e.Message));
			}
		}

		//Validations
		[HttpGet]
		public IActionResult CameraExists(Camera camera)
		{
			return View(camera);
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