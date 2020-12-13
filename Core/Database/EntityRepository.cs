using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Database
{
	public class DbRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly DbContext _context;
		public DbRepository(DbContext context)
		{
			_context = context;
		}

		//Create
		public async Task AddAsync(TEntity entity)
		{
			await this._context
				.Set<TEntity>()
				.AddAsync(entity);

			await this._context.SaveChangesAsync();
		}

		//Read
		public async Task<TEntity> FindByIdAsync(object id)
		{
			return await this._context
				.Set<TEntity>()
				.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> Query(int count = 0)
		{
			return this._context
				.Set<TEntity>()
				.Take(count)
				.AsEnumerable();
		}

		public async Task<IEnumerable<TEntity>> QueryAll()
		{
			return this._context
				.Set<TEntity>()
				.AsEnumerable();
		}

		public async Task<bool> DoesExist(PropertyInfo property, string name)
		{
			return await this._context.Set<TEntity>().AnyAsync(x => property.Name == name);
		}

		//Update
		public async Task EditAsync(object id, TEntity newEntity)
		{
			//Set the Id property to the given id
			TEntity entity = await FindByIdAsync(id);

			//BREAKS!
			this._context.Entry(entity)
				.CurrentValues
				.SetValues(newEntity);

			this._context.Update<TEntity>(newEntity);

			await this._context.SaveChangesAsync();
		}

		//Delete
		public async Task DeleteAsync(object id)
		{
			TEntity entity = await FindByIdAsync(id);

			this._context.Set<TEntity>().Remove(entity);

			await this._context.SaveChangesAsync();
		}
	}
}