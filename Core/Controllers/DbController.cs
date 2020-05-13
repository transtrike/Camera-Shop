using System.Linq;
using ITCareer_Project.Database;
using Microsoft.Data.SqlClient;
using MySQL.Data.EntityFrameworkCore;

namespace ITCareer_Project.Controllers
{
     public class DBController
     {
          private SqlConnection _connection;
         
          public DBController()
          {
               this._connection = Connection.GetConnection();
          }

          //Scrapped
          public int ExecuteCommand(string query)
          {
               int affectedRows = 0;
               
               return affectedRows;
          }
          
          //CRUD Commands (currently not what I wanted)
          public void SelectFromDb() {}
          public void ReadFromDb() {}
          public void UpdateDb() {}
          public void DeleteFromDb() {}
     }
}