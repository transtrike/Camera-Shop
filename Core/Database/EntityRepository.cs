using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Database
{
	public class EntityRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly CameraContext _context;

		public EntityRepository(CameraContext context)
		{
			this._context = context;
		}
		
		public async Task AddAsync(TEntity entity)
		{
			await this._context
				.Set<TEntity>()
				.AddAsync(entity);
			
			await this._context.SaveChangesAsync();
		}

		public IEnumerable<TEntity> QueryAll()
		{
			return this._context
				.Set<TEntity>()
				.AsNoTracking()
				.AsEnumerable();
		}

		public async Task<Entity> FindByIdAsync<Entity>(object id)
			where Entity : class
		{
			return await this._context
				.Set<Entity>()
				.FindAsync(id);
		}

		public async Task EditAsync(object id, TEntity entity)
		{
			DbSet<TEntity> dbSet = this._context.Set<TEntity>();
			TEntity entityToModify = await dbSet.FindAsync(id);
			
			foreach(var propertyInfo in entity.GetType().GetProperties())
				propertyInfo.SetValue(entityToModify, propertyInfo.GetValue(entity));
				
			dbSet.Update(entityToModify);
			
			await this._context.SaveChangesAsync();
		}
		
		public async Task DeleteAsync(TEntity entity)
		{
			this._context
				.Set<TEntity>()
				.Remove(entity);
			
			await this._context.SaveChangesAsync();
		}
	}
}