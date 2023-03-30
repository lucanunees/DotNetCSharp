using Dapper.Contrib.Extensions;

namespace DesafioFinal.Models
{
    [Table("[User]")]
    public class User
    {
        public User()  
        => Roles = new List<Role>();
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash {get; set;}
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }

        [Write(false)] // com isso ele não deixa realizar/escrever o insert de Roles
        public List<Role> Roles {get; set;}
    }
}