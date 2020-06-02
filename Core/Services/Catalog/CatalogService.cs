using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;
using Camera_Shop.Models;

namespace Camera_Shop.Services.Catalog
{
	public class CatalogService
	{
		private CameraContext _context;

		public CatalogService(CameraContext context) => this._context = context;
		
		//Create
		public void Insert(Camera camera)
		{
			//camera.Id = NewId();
			
			this._context.Cameras.Add(camera);
			this._context.SaveChanges();
		}

		//Read
		public IEnumerable<Camera> GetCatalog() => new List<Camera>(this._context.Cameras).AsEnumerable();

		public Camera GetCamera(int id)
		{
			var cameras = 
				from s in this._context.Cameras
				where s.Id == id
				select s;

			return cameras.FirstOrDefault();
		}
	
		//Update
		public void Update(int id, Camera camera)
		{
			var cameraToModify = (
				from c in this._context.Cameras
				where c.Id == id
				select c).FirstOrDefault();

			foreach(var property in camera.GetType().GetProperties())
				property.SetValue(cameraToModify, property.GetValue(camera));

			this._context.SaveChanges();
		}

		//Delete
		public void Delete(int id)
		{
			var cameraToDelete = (
				from c in this._context.Cameras
				where c.Id == id
				select c).FirstOrDefault();
			
			this._context.Remove(cameraToDelete);
			this._context.SaveChanges();
		}

		//Validations
		public bool DoesCameraExist(int id)
		{
			var doesCameraExist =
				from c in this._context.Cameras
				where c.Id == id
				select c;

			bool exists = doesCameraExist.Count() != 0;

			return exists;
		}
	}
}