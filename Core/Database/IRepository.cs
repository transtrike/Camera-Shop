using System.Collections.Generic;
using System.Threading.Tasks;

namespace Camera_Shop.Database
{
	public interface IRepository<TEntity>
	{
		//Add Entity to database
		Task AddAsync(TEntity schema);

		//Return all instances of Entity from database
		IEnumerable<TEntity> QueryAll();

		//Find entity by id
		Task<TEntity> FindByIdAsync(object id);

		//Modify Entity from database
		Task EditAsync(object id, TEntity entity);

		//Delete Entity from database
		Task DeleteAsync(TEntity entity);
	}
}