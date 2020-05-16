using Camera_Shop.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Camera_Shop.Controllers
{
     public class DbController : Controller
     {
          private SqlConnection _connection;
         
          public DbController() => this._connection = Connection.GetConnection();

          //Scrapped
          public int ExecuteCommand(string query)
          {
               int affectedRows = 0;
               
               return affectedRows;
          }
          
          //CRUD Commands (currently not what I wanted)
          public void SelectFromDb() {}
          public void ReadFromDb() {}
          public void UpdateToDb() {}
          public void DeleteFromDb() {}
     }
}