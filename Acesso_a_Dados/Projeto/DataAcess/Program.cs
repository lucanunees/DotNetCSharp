namespace DataAcess;
class Program
{
    //Conectar o CSharp com o banco de dados utilizando a connection string.

    static void Main(string[] args)
    {
        const string connectionString 
        = "Server=localhost,1433;Database=balta;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";

        Console.WriteLine("Hello, World!");
    }
}
 