using Microsoft.Data.SqlClient;

namespace Camera_Shop.Database
{
     public class Connection
     {
          private const string ConnectionString = "Server=localhost;Database=CameraShop;User Id=root;";

          public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
     }
}