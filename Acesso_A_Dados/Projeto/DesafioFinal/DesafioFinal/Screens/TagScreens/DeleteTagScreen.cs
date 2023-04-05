using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class DeleteTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Deletar Tag cadastrada");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Qual tag você deseja Deletar?");
            ListTagScreen.List();
            Console.WriteLine();
            
            Console.WriteLine("ID: ");
            var id = Console.ReadLine();

            Delete(new Tag
            {
                Id = int.Parse(id),
            });
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        private static void Delete(Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>();
                repository.Delete(tag.Id);
                Console.WriteLine("Tag excluida com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel excluir a tag solicitada.");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}