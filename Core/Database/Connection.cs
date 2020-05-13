using Microsoft.Data.SqlClient;

namespace ITCareer_Project.Database
{
     public class Connection
     {
          private const string ConnectionString = "Server=localhost; Port=3306; Uid=root; Password={password}; UseAffectedRows=True";

          public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
     }
}