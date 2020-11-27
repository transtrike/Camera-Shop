using System.Collections.Generic;

namespace Camera_Shop.Database
{
	public interface IRepository<TEntity>
	{
		//Add Entity to database
		void Add(TEntity schema);

		//Return all instances of Entity from database
		IEnumerable<TEntity> QueryAll();
		
		//Modify Entity from database
		void Edit(int id, TEntity entity);
		void Edit(string id, TEntity entity);

		//Delete Entity from database
		void Delete(TEntity entity);
	}
}