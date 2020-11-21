using System;
using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;
using Data.Models.Classes;
using Camera_Shop.Repository;

namespace Camera_Shop.Services.Brands
{
	public class BrandService
	{
		private readonly CameraRepository<Brand> _repository;
		
		public BrandService(CameraContext context)
		{
			this._repository = new CameraRepository<Brand>(context);
		}

		//Create
		public void CreateBrand(Brand brand)
		{
			//Null check
			if(brand == null)
			{
				throw new ArgumentNullException("Brand cannot be null!");
			}
			
			//Check for brand in database
			if(DoesBrandExist(brand))
			{
				throw new ArgumentException($"Brand {brand.Name} exists!");
			}
			
			this._repository.Add(brand);
		}
		
		//Read
		public IEnumerable<Brand> GetAllBrands()
		{
			return this._repository.QueryAll();
		}
		
		public Brand GetBrand(string id)
		{
			return this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);
		}
		
		//Update
		public void UpdateBrand(string id, Brand brand)
		{
			//Null check for given brand
			if(brand == null)
			{
				throw new ArgumentNullException("Brand cannot be null!");
			}
			
			//Check for brand in database
			if(DoesBrandExist(brand))
			{
				throw new ArgumentException($"Brand {brand.Name} exists!");
			}

			var brandToModify = this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);

			//Null check for brand that should be modified
			if(brandToModify == null)
			{
				throw new ArgumentException($"Brand {brand.Name} does not exist!");
			}
			
			this._repository.Edit(id, brand);
		}
		
		//Delete
		public void DeleteBrand(string id)
		{
			var brandToDelete = this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);

			//Null check for brand that should be deleted
			if(brandToDelete == null)
			{
				throw new ArgumentException("Brand doesn't exist");
			}
			
			this._repository.Delete(brandToDelete);
		}
		
		//Validations
		private bool DoesBrandExist(Brand brand)
		{
			return this._repository.QueryAll()
				.Any(x => x.Name == brand.Name);
		}
	}
}