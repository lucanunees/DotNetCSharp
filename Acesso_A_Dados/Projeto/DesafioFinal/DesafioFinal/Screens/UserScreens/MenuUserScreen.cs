namespace Blog.Screens.UserScreens
{
    public static class MenuUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Gestão de Usuário");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar usuário.");
            Console.WriteLine("2 - Cadastrar usuário.");
            Console.WriteLine("3 - Atualizar usuário.");
            Console.WriteLine("4 - Excluir usuário.");
            Console.WriteLine("");
        
            //Colocando o ponto de exclamação, ele esta forçando que seja uma string.
            var options = short.Parse(Console.ReadLine()!);

            switch (options)
            {
                case 1:
                    ListUserScreen.Load();
                    break;
                case 2:
                    CreateUserScreen.Load();
                    break;
                case 3:
                    UpdateUserScreen.Load();
                    break;
                case 4:
                    DeleteUserScreen.Load();
                    break;
                default: 
                    Load();
                    break;
            } 
        }   
    }  
}