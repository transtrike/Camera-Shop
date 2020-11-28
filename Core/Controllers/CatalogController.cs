using System;
using Microsoft.AspNetCore.Mvc;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.DTOs;
using Data.Models.ViewModels;
using Camera_Shop.Services.Catalog;
using System.Threading.Tasks;

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
		public async Task<IActionResult> CreatePost(CameraDTO camera)
		{
			await this._service.InsertAsync(camera);

			return RedirectToAction("ShowCatalog");
		}

		//Read
		[HttpGet]
		public async Task<IActionResult> ShowCatalog()
		{
			return View(await this._service.GetCatalogAsync());
		}

		//Update
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return View(await this._service.GetCameraDTOAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> EditPost(int id, CameraDTO cameraDTO)
		{
			await this._service.UpdateAsync(id, cameraDTO);

			return RedirectToAction("ShowCatalog");
		}

		//Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			return View(await this._service.GetCameraAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await this._service.DeleteAsync(id);

			return RedirectToAction("ShowCatalog");
		}

		
	}
}