using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class UpdateUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Atualização de  Usuário");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Qual usuario você deseja atualizar?");
            ListUserScreen.List();
            Console.WriteLine();
            
            Console.WriteLine();
            Console.WriteLine("Name: ");           
            var name = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine("Email: ");
            var email = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("PasswordHash: ");
            var passwordHash = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine("Bio: ");
            var bio = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine("Image: ");
            var image = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Slug: ");
            var slug = Console.ReadLine();
           
            Update(new User
            {              
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Bio = bio,
                Image = image,
                Slug = slug
            });
            Console.ReadKey();
            MenuUserScreen.Load();
        } 

        private static void Update(User user)
        {
            try
            {
                var repository = new Repository<User>();
                repository.Update(user);
                Console.WriteLine("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel atualizar o usuário solicitado.");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }  
}