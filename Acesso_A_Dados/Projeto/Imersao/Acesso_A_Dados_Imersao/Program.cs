using System.Data;
using Acesso_A_Dados_Imersao.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Acesso_A_Dados_Imersao;
class Program
{
    //Criando um CRUD = CREATE, READ, UPDATE E DELETE.
    static void Main(string[] args)
    {
        const string connectionString
        = "Server=localhost,1433;Database=balta;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            //UpdateCategory(connection);
            //ManyCreateCategory(connection);
            //ListCategories(connection);
            //CreateCategory(connection); -- Deixei comentado para não gerar outra categoria.      
            //ExecuteProcedure(connection);
            ExecuteReadProcedure(connection);
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

    //Podemos criar varias categorias de uma vez, utilizando array ao realizar o execute
    static void ManyCreateCategory(SqlConnection connection)
    {
        // Criei a categoria e passei as informações.  
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "SqlServer";
        category.Url = "Banco de Dados";
        category.Summary = "Aprenda sobre SqlServer";
        category.Order = 10;
        category.Description = "";
        category.Featured = false;

        var newCategory = new Category();
        newCategory.Id = Guid.NewGuid();
        newCategory.Title = "Git";
        newCategory.Url = "cloud";
        newCategory.Summary = "Aprenda sobre Git";
        newCategory.Order = 11;
        newCategory.Description = "";
        newCategory.Featured = false;

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
        //Utilizando o array dentro do new, podemos passar mais de um insert.
        var rows = connection.Execute(insertSql, new[]{
            new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured


        },
            new
            {
            newCategory.Id,
            newCategory.Title,
            newCategory.Url,
            newCategory.Summary,
            newCategory.Order,
            newCategory.Description,
            newCategory.Featured
            }

        });

        Console.WriteLine($"{rows} linhas inseridas.");

    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = @"UPDATE 
                             [Category] 
                                SET [Title] = @Title
                            WHERE [Id] = @id";

        var rows = connection.Execute(updateQuery, new
        { //Passando os parametros dentro do new.
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"), //Passar o ID que precisa ser Atualizado.
            Title = "Front-End" //Title que sera alterado.
        });

        Console.WriteLine($"{rows} Registros atualizados.");
    }

    static void ExecuteProcedure(SqlConnection connection)
    {

        //Só passar o nome da procedure.
        var procedure = "[spDeleteStudent]";

        //Para acrescentar mais parametros, só adicionar a pos a "".
        var pars = new { StudentId = "27ccfdb1-9945-4751-b1b3-c0917ed12f12" };

        var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

        Console.WriteLine($"{affectedRows}, quantidade de registros deletados.");

    }

    static void ExecuteReadProcedure(SqlConnection connection){

        var procedure = "[spGetCoursesByCategory]";
        
        var pars = new {CategoryId = "b4c5af73-7e02-4ff7-951c-f69ee1729cac"};

        //No caso ao invés de utilizar connection.execute, utilizamos o connection.query, que irá retornar uma lista.
        var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

        //Se eu quisesse que fosse retornado um tipo de classe especifica, eu poderia utilizar:
        //  var courses = connection.Query<Courses>(procedure, pars, commandType: CommandType.StoredProcedure);
        //Sem as <> ele gera um objeto dinamico. 


        //Desta forma criamos um objeto dinamico, sendo assim não sendo possivel o acesso as propriedades.
        //O Corretor seria criar uma classe com as propriedades e tipar esse objeto.
        foreach (var item in courses){

            Console.WriteLine($"{item.Id}-{item.Title}");
        }
    }


}
