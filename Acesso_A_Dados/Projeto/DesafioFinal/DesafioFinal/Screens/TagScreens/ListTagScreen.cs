using DesafioFinal.Models;
using DesafioFinal.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class ListTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de Tags");
            Console.WriteLine("------------------------------------");
            List();
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void List()
        {
            var repository = new Repository<Tag>();
            var tags = repository.GetAll();
            foreach (var item in tags)
                Console.WriteLine($" Id: {item.Id} - Nome: {item.Name}  Slug: ({item.Slug})");
        }
    }
}