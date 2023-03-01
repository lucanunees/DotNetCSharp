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
        
        using (var connection = new SqlConnection(connectionString))
        {
            
            //Neste caso estou retornando a lista já no formato da minha classe.
            //E ele só funciona se as colunas da classe e da tabela no SQL forem iguais.
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id} - {category.Title}");
            }
        }
        
    }
}
