using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog;
class Program
{
    private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";
    static void Main(string[] args)
    {
        // Desta forma não preciso estanciar toda vez dentro do método.
        var connection = new SqlConnection(CONNECTION_STRING);
        
        connection.Open(); 
        ReadUsers(connection);
        ReadRoles(connection);
        // ReadUser();
        // CreateUser();
        // UpdateUser();
        // DeleteUser();
        connection.Close();
    }

    /* ================ CRUD ================ */
        //Iremos utilizar o Repositoy Patter
    public static void CreateUser()
    {
        // Para inserir um usuario, eu preciso criar um objeto usuario.
        var user = new User()
        {
            Bio = "Equipe lucas.io",
            Email = "Equipe@dev.io",
            Image = "Https://",
            Name = "Equipe lucas.io",
            PasswordHash = "HASH",
            Slug = "Equipe-lucas"
        };

        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            //Ele retorna um inteiro, com a quantidade de linhas afetas.
            var rows = connection.Insert<User>(user);
            {
                Console.WriteLine($"Cadastro realizado com sucesso. Linhas inseridas - {rows}.");
            }
        }
    }

/* public static void ReadUser()

    public static void ReadUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            //Aqui iremos pegar somente 1 usuario cujo o ID é = 1   
            var user = connection.Get<User>(1);
            Console.WriteLine($"{user.Name}");
        }
    }
*/

/*  public static void ReadUsers()
    public static void ReadUsers()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            // Aqui ele já realiza o select * na tabela,
            // porém ficar espero porque ele pluraliza a tabela ou seja a tabela esta User, ele vai buscar como User
            // para corrigir isso precisa realizar uma modificação na models.
            var users = connection.GetAll<User>();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name}");
            }
        }
    }
*/

    public static void UpdateUser()
    {
        var userUpdate = new User()
        {
            Id = 3,
            Bio = "Equipe de dev lucas.io",
            Email = "Equipe@dev.io",
            Image = "Https://",
            Name = "Equipe de dev lucas.io",
            PasswordHash = "HASH",
            Slug = "Equipe-lucas"
        };

        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var result = connection.Update<User>(userUpdate);
            {
                Console.WriteLine($"Atualização com sucesso. Resultado do update - {result}.");
            }
        }
    }

    public static void DeleteUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var userDelete = connection.Get<User>(3);
            var result = connection.Delete<User>(userDelete);
            {
                Console.WriteLine($"Usuario deletado com sucesso. Resultado - {result}");
            }
        }
    }


    /*================ Repository Pattern ================*/
    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        var users = repository.GetAll();

        //Quando tem apenas uma linha podemos tirar as {}
        foreach(var user in users)       
            Console.WriteLine($"Usuário: {user.Name}");       
    }

    public static void ReadUser(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        var getUser = repository.Get(2);   
        Console.WriteLine($"Usuário: {getUser.Name}");
    }

    public static void ReadRoles(SqlConnection connection)
    {
        var repository = new RoleRepository(connection);
        var roles = repository.Get();

        foreach (var role in roles)
            Console.WriteLine(role.Name);
    }
}
