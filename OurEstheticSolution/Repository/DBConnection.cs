using Microsoft.Data.SqlClient;
using OurEstheticSolution.Interface;
using System.Data;

namespace OurEstheticSolution.Repository
{
    public class DBConnection : IDBConnection
    {
        private readonly IConfiguration _config;
        public DBConnection(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connect()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

    }
}
