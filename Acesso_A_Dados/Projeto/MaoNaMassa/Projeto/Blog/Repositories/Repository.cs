using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Models
{
    public class Repository<TModel> where TModel : class
    {
        // Setando está condição "where", estou dizendo que só posso receber uma classe, se 
        // deixar sem essa condição, ele pode receber qualquer coisa.
        //Desta forma eu estou criando uma classe generica, Estou esperando um type de uma das models.
        //Desta forma eu defino qual tipo será essa classe/repositorio, por isso utilizar o T = Type

        //Dentro desse repositorio teremos o CRUD porém vai ser generico, para 
        //qualquer método poder chamar.

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