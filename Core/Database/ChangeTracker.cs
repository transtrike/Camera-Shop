using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Camera_Shop.Database
{
     public class ChangeTracker<TEntity>
          where TEntity : class, new()
     {
          private List<TEntity> _added;
          private List<TEntity> _removed;
          private List<TEntity> _allEntities;

          public ChangeTracker(IEnumerable<TEntity> entities)
          {
               this._added = new List<TEntity>();
               this._removed = new List<TEntity>();
               this._allEntities = CloneEntities(entities);
          }
          
          public IReadOnlyCollection<TEntity> Added => this._added.AsReadOnly();
          public IReadOnlyCollection<TEntity> Removed => this._removed.AsReadOnly();
          public IReadOnlyCollection<TEntity> AllEntities => this._allEntities.AsReadOnly();

          private List<TEntity> CloneEntities(IEnumerable<TEntity> entities)
          {
               List<TEntity> copiedEntities = new List<TEntity>();

               PropertyInfo[] properties = typeof(TEntity).GetProperties()
                    .Where(x => CameraContext.AllowedSqlTypes.Contains(x.DeclaringType))
                    .ToArray();

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