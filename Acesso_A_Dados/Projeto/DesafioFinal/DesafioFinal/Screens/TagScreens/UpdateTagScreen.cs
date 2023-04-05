using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class UpdateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Atualizar Tag cadastrada");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Qual tag ou slug você deseja atualizar?");
            ListTagScreen.List();
            Console.WriteLine();
            
            Console.WriteLine("ID: ");
            var id = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine("Slug: ");
            var slug = Console.ReadLine();

            Update(new Tag
            {
                Id = int.Parse(id),
                Name = name,
                Slug = slug,
            });
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        private static void Update(Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>();
                repository.Update(tag);
                Console.WriteLine("Tag atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel realizar a atualização da tag Solicitada.");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}