using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class CreateUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Novo Usuário");
            Console.WriteLine("------------------------------------");
            
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
           
            Create(new User
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

        private static void Create(User user)
        {
            try
            {
                var repository = new Repository<User>();
                repository.Create(user);
                Console.WriteLine("Usuário cadastrado com Sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel realizar o cadastro de um novo usuário.");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }  
}