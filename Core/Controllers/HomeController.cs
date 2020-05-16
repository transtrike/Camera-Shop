using System.Diagnostics;
using Camera_Shop.Database;
using Camera_Shop.Enitites;
using Camera_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Camera_Shop.Controllers
{
     public class HomeController : Controller
     {
          private readonly SqlConnection _connection;
          public HomeController() => this._connection = Connection.GetConnection();

          public string SendEntityToDb(string name, int age)
          {
               return $"Hello Camera {name}, {age}";
          }
          
          public IActionResult Index() => View();
  
          [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
          public IActionResult Error()
          {
               return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
          }
     }
}