using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITCareer_Project.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITCareer_Project.Models;
using Microsoft.Data.SqlClient;

namespace ITCareer_Project.Controllers
{
     public class HomeController : Controller
     {
          private readonly SqlConnection _connection;
          public HomeController() => this._connection = Connection.GetConnection();

          public IActionResult Index()
          {
               return View();
          }

          public IActionResult Privacy()
          {
               return View();
          }

          [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
          public IActionResult Error()
          {
               return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
          }
     }
}