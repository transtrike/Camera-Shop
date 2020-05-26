namespace Camera_Shop.Database
{
     public class Connection
     {
          public static Npgsql.NpgsqlConnection GetConnection => new Npgsql.NpgsqlConnection("Server=localhost;Port=5432;Database=CameraShop;User Id=postgres;");
     }
}