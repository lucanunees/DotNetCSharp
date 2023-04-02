﻿using Blog.Screens.TagScreens;
using Microsoft.Data.SqlClient;

namespace DesafioFinal;
class Program
{
    private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=Punto@2015;Trusted_Connection=False; TrustServerCertificate=True;";
    static void Main(string[] args)
    {     
        var connection = new SqlConnection(CONNECTION_STRING);

        connection.Open();

        Load();

        Console.ReadKey();
        connection.Close();      
    }

    private static void Load()
    {
        Console.Clear();
        Console.WriteLine("Menu Blog");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("O que deseja fazer?");
        Console.WriteLine();
        Console.WriteLine("1 - Gestão de usuário.");
        Console.WriteLine("2 - Gestão de perfil.");
        Console.WriteLine("3 - Gestão de categoria.");
        Console.WriteLine("4 - Gestão de tag.");
        Console.WriteLine("5 - Vincular perfil/usuário.");
        Console.WriteLine("6 - Vincular post/tag");
        Console.WriteLine("7 - Relatórios");
        Console.WriteLine("");
        Console.WriteLine("");

        var option = short.Parse(Console.ReadLine()!);

        switch(option)
        {
            case 1:             
                break;
            case 2: 
                break;
            case 3:
                break;
            case 4: 
                MenuTagScreen.Load();
                break;
            case 5:
                break;
            case 6: 
                break;
            case 7:
                break;
            default:
                break;
        }
    }
}
