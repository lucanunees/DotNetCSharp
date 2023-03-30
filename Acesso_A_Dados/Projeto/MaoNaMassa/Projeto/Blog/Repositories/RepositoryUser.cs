using Blog.Models;
using Blog.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace blog.Repositories
{
    public class RepositoryUser : Repository<User>
    {
        private readonly SqlConnection _connection;

        public RepositoryUser(SqlConnection connection)
        :base(connection) // Aqui estou chamando o construtor da classe base, que seria a repository e passando o construtor.
        => _connection = connection;

        public List<User> GetWithRoles()
        {
            var query = @"SELECT
                            [User].*,
                            [Role].*
                        FROM 
                            [User]
                        LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                        LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

            var users = new List<User>();

            var items = _connection.Query<User, Role, User>(
                query,
                (user, role) => // Aqui são os dois tipos de dados que vou receber da tabela, separados a consulta pelo ID
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id); // AQui eu verifico se esse usuario já existe.
                    if (usr == null)
                    {
                        usr = user;
                        if (role != null)
                            usr.Roles.Add(role);

                        users.Add(usr);
                    }
                    else
                        usr.Roles.Add(role);

                    return user;
                }, splitOn: "Id");    

            return users;
        }
    }
}