using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;

namespace Camera_Shop.Services.Brands
{
	public class BrandService
	{
		private readonly DbRepository<Brand> _repository;
		
		public BrandService(CameraContext context)
		{
			this._repository = new DbRepository<Brand>(context);
		}

		//Create
		public async Task CreateBrandAsync(Brand brand)
		{
			//Null check
			if(brand == null)
				throw new ArgumentNullException("Brand cannot be empty!");
			
			//Check for brand in database
			if(await DoesBrandExist(brand.Name))
				throw new ArgumentException($"Brand {brand.Name} exists!");
			
			await this._repository.AddAsync(brand);
		}
		
		//Read
		public async Task<IEnumerable<Brand>> GetAllBrands()
		{
			return await this._repository.QueryAll();
		}
		
		public async Task<Brand> GetBrand(int id)
		{
			return await this._repository.FindByIdAsync(id);
		}
		
		//Update
		public async Task UpdateBrandAsync(int id, Brand brand)
		{
			//Null check for given brand
			if(brand == null)
				throw new ArgumentNullException("Brand cannot be empty!");
			
			//Check for brand in database
			if(await DoesBrandExist(brand.Name))
				throw new ArgumentException($"Brand {brand.Name} exists!");

			var brandToModify = await this._repository.FindByIdAsync(id);

			//Null check for brand that should be modified
			if(brandToModify == null)
				throw new ArgumentException($"Brand {brand.Name} does not exist!");
			
			await this._repository.EditAsync(id, brand);
		}
		
		//Delete
		public async Task DeleteBrandAsync(int id)
		{
			var brandToDelete = await this._repository.FindByIdAsync(id);

			//Null check for brand that should be deleted
			if(brandToDelete == null)
			{
				throw new ArgumentException("Brand doesn't exist");
			}
			
			await this._repository.DeleteAsync(brandToDelete);
		}
		
		//Validations
		private async Task<bool> DoesBrandExist(string brandName)
		{
			var property = typeof(Brand).GetProperty("Name");
			return await this._repository.DoesExist(property, brandName);
		}
	}
}