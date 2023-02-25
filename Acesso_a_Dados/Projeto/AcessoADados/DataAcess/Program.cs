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

        Console.WriteLine("Conexão aberta e fechada!");
        connection.Close();
        //connection.Dispose(); --> esse comando irá destruir o objeto, é valido a utilização caso não seja feita mas nenhum conexão.

        // Segunda forma de se fazer a conexão e mais otimizada, que já realiza o open e o dispose.
        using(var connection2 = new SqlConnection(connectionString))
        {
            Console.WriteLine("Conectado ao banco.");
            connection2.Open();

            //Estou realizando um comando e utilizando o using para garantir que após a execução
			//O objeto será destruido.
            using(var command = new SqlCommand())
            {
                //Garantindo que a minha conexão está aberta.
                command.Connection = connection2;

                //Passando o tipo de comando que será executado.
                command.CommandType = System.Data.CommandType.Text;

                //Realizando o comando à ser executado no banco.
                command.CommandText = 
                "SELECT [Id], [Title] FROM [Category] WITH (NOLOCK)";

                //Para realizar a leitura do SELECT
                var reader = command.ExecuteReader();

                //Para percororrer os dados.
                // Como o reader é um leitor que não volta, no caso do while enquanto estiver dados ele vai continuar lendo.
                while(reader.Read())
                {
                    //Desta forma eu estou percorrendo a leitura da query, 
                    //pegando as informações do reader coluna 0 que é o ID e a string title, coluna 1
                    Console.WriteLine($"{reader.GetGuid(0)}-{reader.GetString(1)}");
                }   
            }
        }
    }
}
 