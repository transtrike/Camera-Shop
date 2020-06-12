using System;
using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;
using Camera_Shop.Models.Classes;

namespace Camera_Shop.Services.Brands
{
	public class BrandService
	{
		private readonly CameraContext _context;
		
		public BrandService(CameraContext context)
		{
			this._context = context;
		}

		//Create
		public void CreateBrand(Brand brand)
		{
			if(DoesBrandExist(brand))
			{
				throw new ArgumentException($"Brand {brand.Name} exists!");
			}
			
			this._context.Brands.Add(brand);
			this._context.SaveChanges();
		}
		
		//Read
		public IEnumerable<Brand> GetAllBrands()
		{
			return this._context.Brands.AsEnumerable();
		}
		
		public Brand GetBrand(int id)
		{
			return this._context.Brands
				.FirstOrDefault(x => x.Id == id);
		}
		
		//Update
		public void UpdateBrand(int id, Brand brand)
		{
			if(DoesBrandExist(brand))
			{
				throw new ArgumentException($"Brand {brand.Name} exists!");
			}

			var brandToModify = this._context.Brands
				.FirstOrDefault(x => x.Id == id);

			if(brandToModify == null)
			{
				throw new ArgumentException($"Brand {brand.Name} does not exist!");
			}

			brandToModify.Id = brand.Id;
			brandToModify.Name = brand.Name;
			
			this._context.SaveChanges();
		}
		
		//Delete
		public void DeleteBrand(int id)
		{
			var brandToDelete = this._context.Brands
				.FirstOrDefault(x => x.Id == id);

			this._context.Remove(brandToDelete);
			this._context.SaveChanges();
		}
		
		//Validations
		private bool DoesBrandExist(Brand brand)
		{
			return this._context.Brands
				.Any(x => x.Name == brand.Name);
		}
	}
}