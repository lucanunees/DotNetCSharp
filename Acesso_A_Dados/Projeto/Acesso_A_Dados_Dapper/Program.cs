using Acesso_A_Dados_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Acesso_A_Dados_Dapper;
class Program
{
    static void Main(string[] args)
    {
        const string connectionString
        = "Server=localhost,1433;Database=balta;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";

        //Criando a estrutura de insert, que pode ser montado fora do Using para evitar muito processamento
        //dentro da conexão com o banco. Lembrando que o Dapper abre e fecha automatico a conexão.
        var category = new Category();  // Criei a categoria e passei as informações.  
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

        using (var connection = new SqlConnection(connectionString))
        {
            //Esse execute que realiza o insert, acrescentando a virgula eu passo os parametros.
            //Utilizar sempre a mesma nomenclatura dos parametros.
            
            //Ele me retorna a quantidade de linhas afetadas.
            var rows = connection.Execute(insertSql, new {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{rows} linhas inseridas.");
            //Neste caso estou retornando a lista já no formato da minha classe.
            //E ele só funciona se as colunas da classe e da tabela no SQL forem iguais.
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        Console.WriteLine("Operação com o banco de dados realizada com sucesso.");
    }
}
