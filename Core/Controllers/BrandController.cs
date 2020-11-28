using System;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.ViewModels;
using Camera_Shop.Services.Brands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Camera_Shop.Controllers
{
	public class BrandController : Controller
	{
		private readonly BrandService _service;

		public BrandController(CameraContext context)
		{
			this._service = new BrandService(context);
		}

		//Create
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePost(Brand brand)
		{
			await this._service.CreateBrandAsync(brand);

			return RedirectToAction("ShowBrands");
		}

		//Read
		[HttpGet]
		public async Task<IActionResult> ShowBrands()
		{
			return View(this._service.GetAllBrands());
		}

		//Update
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return View(this._service.GetBrand(id));
		}

		[HttpPost]
		public async Task<IActionResult> EditPost(int id, Brand brand)
		{
			await this._service.UpdateBrandAsync(id, brand);

			return RedirectToAction("ShowBrands");
		}

		//Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			return View(this._service.GetBrand(id));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await this._service.DeleteBrandAsync(id);

			return RedirectToAction("ShowBrands");
		}
	}
}