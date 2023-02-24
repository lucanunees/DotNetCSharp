using Microsoft.Data.SqlClient;

namespace DataAcess;
class Program
{
    /*
    Conectar o CSharp com o banco de dados utilizando a connection string.
    Necessario baixar o pacote Microsoft.Data.SqlClient
    
    Sintaxe no terminal:
    "dotnet add package Microsoft.Data.SqlClient"
    
    Obs.: Para realizar o download de uma versão especifica, no final acrescentar --version 2.3.1
    É Preciso adicionar o USING.
    */
    static void Main(string[] args)
    {
        const string connectionString 
        = "Server=localhost,1433;Database=balta;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";

        // A Conexão com o banco é feita através desse objeto chamado SQLConnection. 
        // Dentro do método eu passo a minha string de conexão.
        var connection = new SqlConnection(connectionString);

        //Abrir e fechar a conexão.
        connection.Open();

        connection.Close();
        //connection.Dispose(); --> esse comando irá destruir o objeto, é valido a utilização caso não seja feita mas nenhum conexão.

        // Segunda forma de se fazer a conexão e mais otimizada, que já realiza o open e o dispose.
        using(var connection2 = new SqlConnection(connectionString)){

            Console.WriteLine("Realizando da forma mais otimizada.");
        }

        Console.WriteLine("Hello, World!");
    }
}
 