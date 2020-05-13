using System.Collections;
using System.Collections.Generic;

namespace ITCareer_Project.Database
{
     public class DbSet<TCamera> : ICollection<TCamera>
          where TCamera : class, new()
     {
          private int count = 0;
          private bool isReadOnly = false;

          public DbSet() {}

          public int Count => this.count;
          
          public bool IsReadOnly => this.isReadOnly;

          public void Add(TCamera item)
          {
               throw new System.NotImplementedException();
          }

          public void Clear()
          {
               throw new System.NotImplementedException();
          }

          public bool Contains(TCamera item)
          {
               throw new System.NotImplementedException();
          }

          public void CopyTo(TCamera[] array, int arrayIndex)
          {
               throw new System.NotImplementedException();
          }

          public bool Remove(TCamera item)
          {
               throw new System.NotImplementedException();
          }
          
          public IEnumerator<TCamera> GetEnumerator() => throw new System.NotImplementedException();

          IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 
     }
}