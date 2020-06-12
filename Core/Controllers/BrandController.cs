using System;
using Camera_Shop.Database;
using Camera_Shop.Models.Classes;
using Camera_Shop.Models.ViewModels;
using Camera_Shop.Services.Brands;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult Create()
		{
			return View();
		}
		
		[HttpPost]
		public IActionResult CreatePost(Brand brand)
		{
			try
			{
				this._service.CreateBrand(brand);
			
				return RedirectToAction("ShowBrands");
			}
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
			}
		}
		
		//Read
		[HttpGet]
		public IActionResult ShowBrands()
		{
			return View(this._service.GetAllBrands());
		}
		
		//Update
		[HttpGet]
		public IActionResult Edit(int id)
		{
			return View(this._service.GetBrand(id));
		}

		[HttpPost]
		public IActionResult EditPost(int id, Brand brand)
		{
			try
			{
				this._service.UpdateBrand(id, brand);
			
				return RedirectToAction("ShowBrands");
			}
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
			}
		}
		
		//Delete
		[HttpGet]
		public IActionResult Delete(int id)
		{
			return View(this._service.GetBrand(id));
		}

		[HttpPost]
		public IActionResult DeletePost(int id)
		{
			try
			{
				this._service.DeleteBrand(id);
			
				return RedirectToAction("ShowBrands");
			}
			catch(ArgumentException e)
			{
				return View("~/Views/Error/Error.cshtml",
					new ErrorViewModel(e.Message));
			}
		}
	}
}