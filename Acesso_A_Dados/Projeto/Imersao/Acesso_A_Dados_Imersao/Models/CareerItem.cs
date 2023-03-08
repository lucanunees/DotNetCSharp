namespace Acesso_A_Dados_Imersao.Models
{
    public class CareerItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Course Course { get; set; } // Estou ligando um com o outro, ele vai ter o objeto da classe Course.
    }
}