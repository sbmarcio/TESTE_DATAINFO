using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure
{
    public static class DbConnectionFactory
    {
        public static IDbConnection CreateConnection(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
