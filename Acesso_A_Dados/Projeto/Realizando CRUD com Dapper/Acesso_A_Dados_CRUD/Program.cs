using Acesso_A_Dados_CRUD.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Acesso_A_Dados_CRUD;
class Program
{
    //Criando um CRUD = CREATE, READ, UPDATE E DELETE.
    static void Main(string[] args)
    {
        const string connectionString
        = "Server=localhost,1433;Database=balta;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            UpdateCategory(connection);
      
            ListCategories(connection);
            
            //CreateCategory(connection); -- Deixei comentado para não gerar outra categoria.      
        }
    }

    static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void CreateCategory(SqlConnection connection)
    {
        // Criei a categoria e passei as informações.  
        var category = new Category();  
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "cloud";
        category.Summary = "Aprenda sobre cloud AWS (Amazon Web Services)";
        category.Order = 8;
        category.Description = "";
        category.Featured = false;

        // Para evitar um ataque do tipo SQL Injection, jamais concatenar os valores direto no insert, devemos utilizar parametros.
        //Exemplo na anotações Aula_Dapper.

        var insertSql = @"INSERT INTO 
                          [Category] 
                        VALUES (
                            @Id, 
                            @Title, 
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured
                            )";

        //Ele me retorna a quantidade de linhas afetadas.
        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });

        Console.WriteLine($"{rows} linhas inseridas.");

    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = @"UPDATE 
                             [Category] 
                                SET [Title] = @Title
                            WHERE [Id] = @id";

        var rows = connection.Execute(updateQuery, new{ //Passando os parametros dentro do new.
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"), //Passar o ID que precisa ser Atualizado.
            Title = "Front-End" //Title que sera alterado.
        });

        Console.WriteLine($"{rows} Registros atualizados.");
    }
}
