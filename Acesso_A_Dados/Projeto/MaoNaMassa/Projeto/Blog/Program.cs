using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog;
class Program
{
    private const string CONNECTION_STRING = @"Server=localhost,1433;DataBase=Blog;User ID=sa;Password=Punto@2015";
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static void ReadUser()
    {
        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var users = connection.GetAll<User>();

            foreach (var item in users)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
    }
}
