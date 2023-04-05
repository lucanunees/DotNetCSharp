using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class ListUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de Usuários");
            Console.WriteLine("------------------------------------");
            List();
            Console.ReadKey();
            MenuUserScreen.Load();
        } 

        public static void List()
        {
            var repository = new RepositoryUser();
            var user = repository.GetWithRoles();

            foreach(var item in user)
            {
                Console.WriteLine($"Id: {item.Id} - Name: {item.Name}");

                foreach (var roles in item.Roles)
                {
                    Console.WriteLine($"Role: {roles.Name}");
                }
            }
        }
    }  
}