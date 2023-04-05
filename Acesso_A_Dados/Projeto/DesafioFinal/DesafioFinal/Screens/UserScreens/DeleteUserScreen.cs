using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class DeleteUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Deletar Usuário cadastrado");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Qual usuário você deseja Excluir?");
            ListUserScreen.List();
            Console.WriteLine();
            
            Console.WriteLine("ID: ");
            var id = Console.ReadLine();

            Delete(new User
            {
                Id = int.Parse(id),
            });
            Console.ReadKey();
            MenuUserScreen.Load();
        } 

        private static void Delete(User user)
        {
            try
            {
                var repository = new Repository<User>();
                repository.Delete(user.Id);
                Console.WriteLine("Usuário excluido com sucesso!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Não foi possivel Deletar o usuario informado.");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }  
}