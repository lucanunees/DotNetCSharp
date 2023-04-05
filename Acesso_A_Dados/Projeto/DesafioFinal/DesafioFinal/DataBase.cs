using Microsoft.Data.SqlClient;

namespace Blog
{
    //Classe static sempre fica na memoria.
    public static class Database
    {
        public static SqlConnection Connection;
    }
}