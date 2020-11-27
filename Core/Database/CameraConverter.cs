using System;
using System.Threading.Tasks;
using Data.Models.Classes;
using Data.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Database
{
	public class CameraConverter : IClassConverter<Camera, CameraDTO>
	{
		private readonly CameraContext _context;

		public CameraConverter(CameraContext context) 
		{
			_context = context;
		}

		public async Task<Camera> DtoToClassAsync(int? cameraId, CameraDTO cameraDTO)
		{
			Camera camera = new();

			if (cameraId != null)
				camera.Id = (int)cameraId;

			camera.Brand = await GetBrandByNameAsync(cameraDTO.Brand);
			camera.Model = cameraDTO.Model;
			camera.Megapixels = cameraDTO.Megapixels;
			camera.BaseISO = cameraDTO.BaseISO;
			camera.MaxISO= cameraDTO.MaxISO;

			return camera;
		}

		public async Task<CameraDTO> ClassToDtoAsync(Camera camera)
		{
			CameraDTO cameraDTO = new();

			cameraDTO.Id = camera.Id;
			cameraDTO.Brand = await GetBrandNameByIdAsync(camera.BrandId);
			cameraDTO.Model = camera.Model;
			cameraDTO.Megapixels = camera.Megapixels;
			cameraDTO.BaseISO = camera.BaseISO;
			cameraDTO.MaxISO = camera.MaxISO;

			return cameraDTO;
		}

		private async Task<string> GetBrandNameByIdAsync(int brandId)
		{
			Camera cam = await _context.Cameras.FirstOrDefaultAsync();

			Brand brandName = await _context.Brands
				.FirstOrDefaultAsync(x => x.Id == brandId);

			return brandName.Name ?? throw new ArgumentException("Invalid brand id!");
		}
	
		private async Task<Brand> GetBrandByNameAsync(string brandName)
		{
			Brand brand = await _context.Brands
				.FirstOrDefaultAsync(x => x.Name == brandName);

			return brand ?? throw new ArgumentException("Brand doesn't exist!");
		}
	}
}