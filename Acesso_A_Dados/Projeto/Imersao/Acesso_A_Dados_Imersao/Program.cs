﻿using System.Data;
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
            // UpdateCategory(connection);
            // ManyCreateCategory(connection);
            // ListCategories(connection);
            // CreateCategory(connection); -- Deixei comentado para não gerar outra categoria.      
            // ExecuteProcedure(connection);
            // ExecuteReadProcedure(connection);
            // ReadView(connection);
            // OneToOne(connection);
            // OneToMany(connection);
            // QuerMultiple(connection);
            // SelectIn(connection);
            // Like(connection);
            Transaction(connection);
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
        //Podemos criar varias categorias de uma vez, utilizando array ao realizar o execute
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

    static void ExecuteReadProcedure(SqlConnection connection)
    {
        var procedure = "[spGetCoursesByCategory]";

        var pars = new { CategoryId = "b4c5af73-7e02-4ff7-951c-f69ee1729cac" };

        //No caso ao invés de utilizar connection.execute, utilizamos o connection.query, que irá retornar uma lista.
        var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

        //Se eu quisesse que fosse retornado um tipo de classe especifica, eu poderia utilizar:
        //  var courses = connection.Query<Courses>(procedure, pars, commandType: CommandType.StoredProcedure);
        //Sem as <> ele gera um objeto dinamico. 

        //Desta forma criamos um objeto dinamico, sendo assim não sendo possivel o acesso as propriedades.
        //O Corretor seria criar uma classe com as propriedades e tipar esse objeto.
        foreach (var item in courses)
        {
            Console.WriteLine($"{item.Id}-{item.Title}");
        }
    }

    static void ReadView(SqlConnection connection)
    {
        var sql = "SELECT * FROM vwCourses";

        var courses = connection.Query(sql);

        foreach (var item in courses)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void OneToOne(SqlConnection connection)
    {
        var sql = @" 
                    SELECT 
                     *
                    FROM
                        [CareerItem]
                    INNER JOIN 
                        [Course]
                            ON [CareerItem].[CourseId] = [Course].[Id]";

        // Eu tenho que realizar a execução da query, passando que eu tenho na query um CareerItem e um course e o resultado final/junção será um CareerItem.
        var items = connection.Query<CareerItem, Course, CareerItem>(
            sql,
            // Após o SQl eu preciso criar uma função passando os parametros careerItem e course, que o resultado em cima pode enviar tanto um careerItem como um course.			
            (careerItem, course) =>
            {
                careerItem.Course = course;
                return careerItem;
            }, splitOn: "Id"); // E o SPLIT é que serapa uma tabela da outra.. que no caso é o Id

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Title} - Curso: {item.Course.Title}");
        }
    }

    static void OneToMany(SqlConnection connection)
    {
        var sql = @"SELECT
                        [Career].[Id],
                        [Career].[Title],
                        [CareerItem].[CareerId],
                        [CareerItem].[Title]
                    FROM 
                        [Career]
                    INNER JOIN 
                        [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                    ORDER BY 
                        [Career].[Title]";


        var careers = new List<Career>();
        //Iremos receber um item de career, populado com um CareerItem E o resultado final vai ser um career.
        var items = connection.Query<Career, CareerItem, Career>(
            sql,

            (career, item) =>
            {
                var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();

                if (car == null)
                {
                    car = career;
                    car.Items.Add(item);
                    careers.Add(car);
                }
                else
                {
                    car.Items.Add(item);
                }
                //Temos que retornar sempre o objeto pai, que no caso é o career.
                return career;
            }, splitOn: "CareerId");

        foreach (var career in careers)
        {
            Console.WriteLine($"{career.Title}");

            foreach (var item in career.Items)
            {
                Console.WriteLine($"- {item.Title}");
            }
        }
    }

    static void QuerMultiple(SqlConnection connection)
    {

        // Pra realizar o sql Multiplo basta eu dividar a query com ;
        var query = $"SELECT * FROM [Category]; SELECT * FROM [Course]";

        using (var multi = connection.QueryMultiple(query))
        {

            //Aqui estou atribuindo os valores do retorno para as variaves e passando a tipagem(model).
            var categories = multi.Read<Category>();
            var courses = multi.Read<Course>();

            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }

            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }


    }

    static void SelectIn(SqlConnection connection)
    {
        // Execução passando os valores fixos.     
        var query = @"SELECT
                        *
                    FROM 
                        Career
                    WHERE
                        [Id] IN ('01ae8a85-b4e8-4194-a0f1-1c6190af54cb', 'e6730d1c-6870-4df3-ae68-438624e04c72')";

        var itemNotPars = connection.Query<Career>(query);

        foreach (var item in itemNotPars)
        {
            Console.WriteLine(item.Title);
        }

        //Caso queira passar os valores dinamicos, através de parametros ficaria desta forma:     
        var item1 = "4327ac7e-963b-4893-9f31-9a3b28a4e72b";

        var item2 = "92d7e864-bea5-4812-80cc-c2f4e94db1af";

        var queryPars = @"SELECT
                        *
                    FROM 
                        Career
                    WHERE
                        [Id] IN @Id";

        var items = connection.Query<Career>(queryPars, new
        {
            Id = new[]{
                item1,
                item2
                //Ou poderia passar assim
                //"4327ac7e-963b-4893-9f31-9a3b28a4e72b",
                //"92d7e864-bea5-4812-80cc-c2f4e94db1af"
            }
        });

        foreach (var item in items)
        {
            Console.WriteLine(item.Title);
        }
    }

    static void Like(SqlConnection connection)
    {
        var parms = "api";
        var query = @"SELECT 
                        * 
                    FROM 
                        [Course] 
                    WHERE 
                        [Title] LIKE @exp";

        var items = connection.Query<Course>(query, new
        {
            exp = $"%{parms}%"
        });

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Title}");
        }
    }

    static void Transaction(SqlConnection connection)
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

        // Eu preciso abrir a conexão.
        connection.Open();

        //Estou criando a transaction na execução da query.
        using (var transaction = connection.BeginTransaction())
        {
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
                //Aqui no final eu preciso passar a transaction que esta sendo feita.
            }, transaction);

            // E por fim confirmar ou desfazer a transacao
            transaction.Rollback();
            //transaction.Commit();      
            Console.WriteLine($"{rows} linhas inseridas");
        }
    }

}
