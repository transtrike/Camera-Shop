using System.Collections.Generic;
using System.Linq;
using Camera_Shop.Database;

namespace Camera_Shop.Repository
{
	public class CameraRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly CameraContext _context;

		public CameraRepository(CameraContext context)
		{
			this._context = context;
		}
		
		public void Add(TEntity entity)
		{
			this._context
				.Set<TEntity>()
				.Add(entity);
			
			this._context.SaveChanges();
		}

		public IEnumerable<TEntity> QueryAll()
		{
			return this._context
				.Set<TEntity>()
				.AsEnumerable();
		}

		public void Edit(string id, TEntity entity)
		{
			var dbSet = this._context.Set<TEntity>();
			var entityToModify = dbSet.Find(id);
			
			foreach(var propertyInfo in entity.GetType().GetProperties())
			{
				propertyInfo.SetValue(entityToModify, propertyInfo.GetValue(entity));
			}
				
			dbSet.Update(entityToModify);
			
			this._context.SaveChanges();
		}
		
		public void Delete(TEntity entity)
		{
			this._context
				.Set<TEntity>()
				.Remove(entity);
			
			this._context.SaveChanges();
		}
	}
}