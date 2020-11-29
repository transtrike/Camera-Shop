using Camera_Shop.Database;
using Data.Models.Classes;
using Camera_Shop.Services.Brands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Camera_Shop.Controllers
{
	[Authorize(Policy = "Logged")]
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
		public async Task<IActionResult> CreatePost(Brand brand)
		{
			await this._service.CreateBrandAsync(brand);

			return RedirectToAction("ShowBrands");
		}

		//Read
		[HttpGet]
		public IActionResult ShowBrands()
		{
			return View(this._service.GetAllBrands());
		}

		public IActionResult ViewBrand(int id) => View(this._service.GetBrand(id));

		//Update
		[HttpGet]
		public IActionResult Edit(int id)
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
		public IActionResult Delete(int id)
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