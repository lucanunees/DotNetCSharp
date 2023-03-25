using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository
    {
        // Criando a variavel aqui, eu não preciso chamar o using var e estanciar uma conexão.
        // atribuido para readonly desta forma não pode ser mais alterado.
        private readonly SqlConnection _connection;

        // Para não precisar passar vaias conexões e abrir diversas vezes a conexão, podemos compartilhar
        // a conexão utilizando o contrutor.

        public UserRepository(SqlConnection connection)      
            // Desta forma, quando eu instanciar o método UserRepository eu preciso passar a conexão
            // que ja será atribuida à variavel _connection.
            => _connection = connection;
        

        //Classe static não precisa ser instanciada, ela sobe junto com a aplicação, porém neste caso não é preciso.
        //Diferença do IEnurable ele é mais puro, so podemos percorrer ele, diferente do List que podemos remover e add.
        public IEnumerable<User> GetAll()
        {
            /*
            //Tesse jeito é o "antigo" podemos otimizar fazendo da forma abaixo:
            using (var connection = new SqlConnection(""))
            {
                //Como iremos chamar essa classe em diversos lugares, retornamos a lista de usuarios e
                // o método que chamou resolve o que vai fazer com a lista.
                return connection.GetAll<User>();
            }
            */

            //Forma otimizada.
            return _connection.GetAll<User>();
        }

        // Como só tem uma linha, podemos utilizar o Expression-bodied, retirando as {} e o return
        // e acrescentando =>
        public User Get(int id)      
            => _connection.Get<User>(id);
        
        public void  Create(User user)
            => _connection.Insert<User>(user);      
    }
}