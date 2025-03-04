using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace api_help_desk.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        private readonly ThroneConnection.Services.ConnectionService dbConnection;

        private ThroneConnection.Services.User? user;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            dbConnection = new ThroneConnection.Services.ConnectionService();
        }

        public IDbConnection CreateConnection() => dbConnection.GetSqlConnection();

        public IDbConnection CreateConnection(string user, string context)
        {

            return dbConnection.GetSqlConnection(this.user, context);
        }

        public string GetValue(string value)
        {
            return _configuration.GetSection(value).Value;
        }
    }
}
