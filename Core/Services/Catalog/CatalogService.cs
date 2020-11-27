using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;
using Data.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Services.Catalog
{
	public class CatalogService
	{
		private readonly CameraContext _context;
		private readonly CameraConverter _converter;

		public CatalogService(CameraContext context)
		{
			this._context = context;
			this._converter = new CameraConverter(context);
		} 
		
		//Create
		public async Task Insert(CameraDTO cameraDTO)
		{
			if(await DoesCameraExistAsync(cameraDTO.Model)) 
				throw new ArgumentException($"Camera {cameraDTO.Model} already exists!");

			Camera camera = await _converter.DtoToClassAsync(null, cameraDTO);

			await this._context.Cameras.AddAsync(camera);
			await this._context.SaveChangesAsync();
		}

		//Read
		public async Task<IEnumerable<Camera>> GetCatalogAsync()
		{
			return await this._context.Cameras
				.Include("Brand")
				.ToListAsync()
				.ConfigureAwait(false);
		}

		public async Task<Camera> GetCameraAsync(int id)
		{
			var camera = await this._context.Cameras
				.Include(x => x.Brand)
				.OrderBy(x => x.Brand)
				.FirstOrDefaultAsync(x => x.Id == id);

			return camera;
		}
		
		public async Task<CameraDTO> GetCameraDTOAsync(int id)
		{
			Camera camera = await this._context.Cameras
				.OrderBy(x => x.Brand)
				.FirstOrDefaultAsync(x => x.Id == id);

			CameraDTO cameraDTO = await _converter.ClassToDtoAsync(camera);

			return cameraDTO;
		}
	
		//Update
		public async Task Update(int id, CameraDTO cameraDTO)
		{
			Camera cameraToModify = await this._context.Cameras
				.Include(x => x.Brand)
				.FirstOrDefaultAsync(x => x.Id == id);

			if(cameraToModify == null)
				throw new ArgumentException($"Camera {cameraDTO.Brand}: {cameraDTO.Model} does not exist!");
				
			//if(DoesCameraExist(cameraDTO.Model))
			//	throw new ArgumentException("Model already exists!");

			Camera camera = await _converter.DtoToClassAsync(id, cameraDTO);

			//TODO: Reflection breaks the Update
			//foreach (var property in typeof(Camera).GetProperties())
			//property.SetValue(cameraToModify, property.GetValue(camera));

			await this._context.SaveChangesAsync();
		}

		//Delete
		public async Task Delete(int id)
		{
			var cameraToDelete = await this._context.Cameras
				.FirstOrDefaultAsync(x => x.Id == id);
			
			this._context.Remove(cameraToDelete);
			await this._context.SaveChangesAsync();
		}

		//Validations
		private async Task<bool> DoesCameraExistAsync(string model)
		{
			var doesCameraExist = await this._context.Cameras
				.FirstOrDefaultAsync(x => x.Model == model);

			return doesCameraExist != null;
		}
	}
}