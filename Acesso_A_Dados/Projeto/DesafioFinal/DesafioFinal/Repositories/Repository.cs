using Blog;
using Dapper.Contrib.Extensions;
using DesafioFinal.Models;

namespace DesafioFinal.Repositories
{
    public class Repository<TModel> where TModel : class
    {
        public IEnumerable<TModel> GetAll()
        => Database.Connection.GetAll<TModel>();

        public TModel Get(int id)
        => Database.Connection.Get<TModel>(id);

        public void Create(TModel model)
        => Database.Connection.Insert<TModel>(model);

        public void Update(TModel model)
        {
            Database.Connection.Update<TModel>(model);
        }

        public void Delete(TModel model)
        {
            Database.Connection.Update<TModel>(model);
        }

        public void Delete(int id)
        {
            var model = Database.Connection.Get<TModel>(id);
            Database.Connection.Delete<TModel>(model);
        }
    }
}