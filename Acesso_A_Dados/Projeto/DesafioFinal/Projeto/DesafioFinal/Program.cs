using DesafioFinal.Models;
using DesafioFinal.Repositories;
using Microsoft.Data.SqlClient;

namespace DesafioFinal;
class Program
{
    private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";
    static void Main(string[] args)
    {     
        var connection = new SqlConnection(CONNECTION_STRING);

        connection.Open();


        connection.Close();
    }

    public static void ReadUsersWithRoles(SqlConnection connection)
    {   
        var repository = new RepositoryUser(connection);
        var items = repository.GetWithRoles();

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name}");

            foreach(var role in item.Roles)
            {
                Console.WriteLine($"-{role.Name}");
            }
        }  
    }

    public static void ReadRoles (SqlConnection connection)
    {
        var repository = new Repository<Role>(connection);
        var items = repository.GetAll();

        foreach(var item in items)
        {
            Console.WriteLine($"{item.Name}");
        }
    }

    public static void ReadTags (SqlConnection connection)
    {
        var repository = new Repository<Tag>(connection);
        var items = repository.GetAll();

        foreach(var item in items)
        {
            Console.WriteLine($"{item.Name}");
        }
    }

    public static void CreateUser(SqlConnection connection)
    {
        var user = new User()
        {
            Email = "email@balta.io",
            Bio = "Biografia",
            Image = "Imagem",
            Name = "Name",
            PasswordHash = "hash",
            Slug = "Slug"
        };
        var repository = new Repository<User>(connection);
        
        repository.Create(user);
    }

    public static void UpdateUser(SqlConnection connection)
     {
        var repository = new Repository<User>(connection);
        var getUser = repository.Get(1);
        repository.Update(getUser);
    }

}
