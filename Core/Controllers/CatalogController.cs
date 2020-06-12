using System;
using Microsoft.AspNetCore.Mvc;
using Camera_Shop.Database;
using Camera_Shop.Models.Classes;
using Camera_Shop.Models.DTOs;
using Camera_Shop.Models.ViewModels;
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
		public IActionResult CreatePost(CameraDTO camera)
		{
			try
			{
				this._service.Insert(camera);
				
				return RedirectToAction("ShowCatalog");
			}
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
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
			return View(this._service.GetCameraDTO(id));
		}

		[HttpPost]
		public IActionResult EditPost(int id, CameraDTO cameraDTO)
		{
			try
			{
				this._service.Update(id, cameraDTO);

				return RedirectToAction("ShowCatalog");
			}
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml", new ErrorViewModel(e.Message));
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
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml", new ErrorViewModel(e.Message));
			}
		}

		//Validations
		[HttpGet]
		public IActionResult CameraExists(Camera camera)
		{
			return View(camera);
		}
	}
}