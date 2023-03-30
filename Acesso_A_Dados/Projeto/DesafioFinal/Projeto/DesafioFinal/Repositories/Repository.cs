using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace DesafioFinal.Repositories
{
    public class Repository<TModel> where TModel : class
    {
        private readonly SqlConnection _connection;
        public Repository(SqlConnection connection)
        => _connection = connection;

        public IEnumerable<TModel> GetAll()
        => _connection.GetAll<TModel>();

        public TModel Get(int id)
        => _connection.Get<TModel>(id);

        public void Create(TModel model)
        => _connection.Insert<TModel>(model);

        public void Update(TModel model)
        {                 

        }

        public void Delete(TModel model)
        {

        }
        
        public void Delete(int id)
        {
            var model = _connection.Get<TModel>(id);
            _connection.Delete<TModel>(model);
        }
    }
}