using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace api_help_desk.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        private class DatabaseConfig
        {
            public string Server { get; set; }
            public string Database { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
        }

        private readonly DatabaseConfig _dbConfig;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConfig = new DatabaseConfig
            {
                Server = _configuration["Database:Server"],
                Database = _configuration["Database:Database"],
                UserId = _configuration["Database:UserId"],
                Password = _configuration["Database:Password"]
            };
        }
        
        public IDbConnection CreateConnection(string user, string context)
        {
            return new SqlConnection(
                $"Server={_dbConfig.Server};Initial Catalog={_dbConfig.Database};User Id={_dbConfig.UserId};Password={_dbConfig.Password};Persist Security Info = False; MultipleActiveResultSets = False; Encrypt = false; TrustServerCertificate = False; Connection Timeout = 30;");
        }

        public string GetValue(string value)
        {
            return _configuration.GetSection(value).Value;
        }
    }
}