using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class CreateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Nova Tag");
            Console.WriteLine("------------------------------------");
            
            Console.WriteLine();
            Console.WriteLine("Name: ");           
            var name = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine("Slug: ");
            var slug = Console.ReadLine();
           
            Create(new Tag
            {
                Name = name,
                Slug = slug,
            });
            Console.ReadKey();
            MenuTagScreen.Load();
        } 

        private static void Create(Tag tag)
        {        
            try
            {
                var repository = new Repository<Tag>();
                repository.Create(tag);
                Console.WriteLine("Tag cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel cadastrar a nova tag.");
                Console.WriteLine($"Erro: {ex.Message}");
            }        
        }
    }  
}