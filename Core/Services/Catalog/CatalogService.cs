using System;
using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;
using Camera_Shop.Models.Classes;
using Camera_Shop.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Services.Catalog
{
	public class CatalogService
	{
		private readonly CameraContext _context;

		public CatalogService(CameraContext context) => this._context = context;
		
		//Create
		public void Insert(CameraDTO cameraDTO)
		{
			if(DoesCameraExist(cameraDTO.Model) && GetBrandByName(cameraDTO.Brand) != null)
			{
				throw new ArgumentException($"Camera {cameraDTO.Model} already exists!");
			}
			
			var camera = new Camera();
			camera.Brand = GetBrandByName(cameraDTO.Brand);
			camera.Model = cameraDTO.Model;
			camera.Megapixels = cameraDTO.Megapixels;
			camera.BaseISO = cameraDTO.BaseISO;
			camera.MaxISO = cameraDTO.MaxISO;

			this._context.Cameras.Add(camera);
			this._context.SaveChanges();
		}

		//Read
		public IEnumerable<Camera> GetCatalog() => new List<Camera>(
			this._context.Cameras.Include("Brand"))
			.AsEnumerable();

		public Camera GetCamera(int id)
		{
			var camera = this._context.Cameras
				.Include(x => x.Brand)
				.FirstOrDefault(x => x.Id == id);

			return camera;
		}
		
		public CameraDTO GetCameraDTO(int id)
		{
			var camera = this._context.Cameras
				.FirstOrDefault(x => x.Id == id);

			var cameraDTO = new CameraDTO();
			cameraDTO.Id = camera.Id;
			cameraDTO.Brand = GetBrandNameById(camera.BrandId);
			cameraDTO.Model = camera.Model;
			cameraDTO.Megapixels = camera.Megapixels;
			cameraDTO.BaseISO = camera.BaseISO;
			cameraDTO.MaxISO = camera.MaxISO;
			
			return cameraDTO;
		}
	
		//Update
		public void Update(int id, CameraDTO cameraDTO)
		{
			var cameraToModify = this._context.Cameras
				.FirstOrDefault(x => x.Id == id);

			if(cameraToModify == null)
			{
				throw new ArgumentException($"Camera {cameraDTO.Brand}: {cameraDTO.Model} does not exist!");
			}
			
			cameraToModify.Brand = GetBrandByName(cameraDTO.Brand);
			cameraToModify.Megapixels = cameraDTO.Megapixels;
			cameraToModify.BaseISO = cameraDTO.BaseISO;
			cameraToModify.MaxISO= cameraDTO.MaxISO;
			
			this._context.SaveChanges();
		}

		//Delete
		public void Delete(int id)
		{
			var cameraToDelete = this._context.Cameras
				.FirstOrDefault(x => x.Id == id);
			
			this._context.Remove(cameraToDelete);
			this._context.SaveChanges();
		}

		//Validations
		private bool DoesCameraExist(string model)
		{
			var doesCameraExist = this._context.Cameras
				.FirstOrDefault(x => x.Model == model);

			return doesCameraExist != null;
		}

		private Brand GetBrandByName(string brandName)
		{
			var brand = this._context.Brands
				.FirstOrDefault(x => x.Name == brandName);

			return brand != null ? 
				brand : throw new ArgumentException("Brand doesn't exist!");
		}

		private string GetBrandNameById(int id)
		{
			var brandName = this._context.Brands
				.FirstOrDefault(x => x.Id == id);

			return brandName.Name;
		}
	}
}