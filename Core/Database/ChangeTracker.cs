using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Camera_Shop.Database
{
     public class MyChangeTracker<TEntity>
          where TEntity : class, new()
     {
          private List<TEntity> _added;
          private List<TEntity> _removed;
          private List<TEntity> _allEntities;

          public MyChangeTracker(IEnumerable<TEntity> entities)
          {
               this._added = new List<TEntity>();
               this._removed = new List<TEntity>();
               this._allEntities = CloneAllEntities(entities);
          }

          public IReadOnlyCollection<TEntity> Added => this._added.AsReadOnly();
          public IReadOnlyCollection<TEntity> Removed => this._removed.AsReadOnly();
          public IReadOnlyCollection<TEntity> AllEntities => this._allEntities.AsReadOnly();
          public void Add(TEntity entity) => this._added.Add(entity);
          public void Remove(TEntity entity) => this._removed.Add(entity);
          public void Modified(TEntity entity) => this._allEntities.Add(entity);

          private List<TEntity> CloneAllEntities(IEnumerable<TEntity> entities)
          {
               List<TEntity> copiedEntities = new List<TEntity>();

               PropertyInfo[] properties = typeof(TEntity).GetProperties().ToArray();

               foreach(var entity in entities)
               {
                    TEntity copyEntity = Activator.CreateInstance<TEntity>();

                    foreach(var property in properties)
                    {
                         object value = property.GetValue(entity);
                         property.SetValue(copiedEntities, value);
                    }
                    
                    copiedEntities.Add(copyEntity);
               }
               
               return copiedEntities;
          }
     }
}