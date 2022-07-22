using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace homework2_NET.Context
{
    public class AppDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            return this.configuration.GetConnectionString("postgreSqlCon");
        }

        public NpgsqlConnection CreateConnection()
        {

            return new NpgsqlConnection(connectionString);

        }
    }
}
