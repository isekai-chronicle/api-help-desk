using api_help_desk.Context;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace api_help_desk.Controllers.Security.Authentication
{
    public class AuthenticationService : AuthenticationInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Authentication", "Script");

        public AuthenticationService(DapperContext Context) => _context = Context;

        public async void GenerateTaskUserName(User user)
        {
            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                try
                {
                    var sqlFilePath = Path.Combine(_path, "UserSession.sql");
                    var sql = await File.ReadAllTextAsync(sqlFilePath);
                    parameters = new DynamicParameters();
                    parameters.Add("@userName_parameter", user.userName);
                    parameters.Add("@user_id_parameter", "");
                    parameters.Add("@password_parameter", user.password);
                    parameters.Add("@logIn_parameter", 1);
                    parameters.Add("@logOut_parameter", 0);

                    var isAuth = await connection.ExecuteAsync(
                        sql,
                        parameters,
                    commandTimeout: 0);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public async void GenerateTaskUserId(Guid user_id)
        {
            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                try
                {
                    var sqlFilePath = Path.Combine(_path, "UserSession.sql");
                    var sql = await File.ReadAllTextAsync(sqlFilePath);
                    parameters = new DynamicParameters();
                    parameters.Add("@userName_parameter", "");
                    parameters.Add("@user_id_parameter", user_id);
                    parameters.Add("@password_parameter", "");
                    parameters.Add("@logIn_parameter", 0);
                    parameters.Add("@logOut_parameter", 1);

                    var isAuth = await connection.ExecuteAsync(
                        sql,
                        parameters,
                    commandTimeout: 0);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<object> PostLogOut(Guid user_id)
        {
            GenerateTaskUserId(user_id);
            return null;
        }
        public async Task<List<Config>> PostToken(User user)
        {
            GenerateTaskUserName(user);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                try
                {
                    var sqlFilePath = Path.Combine(_path, "PostLogin.sql");
                    var sql = await File.ReadAllTextAsync(sqlFilePath);
                    parameters = new DynamicParameters();
                    parameters.Add("@userName_parameter", user.userName);
                    parameters.Add("@password_parameter", user.password);

                    var configs = new List<Config>();

                    var isAuth = await connection.QueryAsync<Config, TaskList, Config>(
                        sql,
                    (config, task) =>
                    {
                        var existingStep = configs.FirstOrDefault(p => p.access == config.access);
                        if (existingStep == null)
                        {
                            config.tasks = new List<TaskList>();
                            configs.Add(config);
                            existingStep = config;
                        }
                        if (task != null) existingStep.tasks.Add(task);
                        return existingStep;
                    },
                    parameters,
                    splitOn: "list",
                    commandTimeout: 0);

                    if (configs[0].isValidate)
                    {
                        string token = await GetJWT(user, configs[0].access, configs[0].country);
                        foreach (var config in configs)
                        {
                            config.key = token;
                        }

                        return configs.ToList();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return null;
        }

        public async Task<string> GetJWT(User user, Guid id, string country)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.userName),
            });
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_context.GetValue("Jwt:Key")));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(960),
                SigningCredentials = new SigningCredentials(tokenkey, SecurityAlgorithms.HmacSha256),
                Issuer = _context.GetValue("Jwt:Issuer"),
                Audience = _context.GetValue("Jwt:Audience")
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<List<MenuModel>> GetMenu(Guid user)
        {
            var sqlFilePath = Path.Combine(_path, "GetMenu.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                parameters.Add("@user_id_parameter", user);
                var list = await connection.QueryAsync<MenuModel>(sql, parameters);
                return list.ToList();

            }
        }

        public Task<List<MenuModel>> PostMenu(UserAccess menu)
        {
            var sqlFilePath = Path.Combine(_path, "PostMenu.sql");
            var sql = File.ReadAllText(sqlFilePath);
            parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                parameters.Add("@user_id_parameter", menu.user_id);
                parameters.Add("@component_id_parameter", menu.component_id);
                parameters.Add("@task_id_parameter", menu.task_id);

                var list = connection.Query<MenuModel>(sql, parameters);
                return System.Threading.Tasks.Task.FromResult(list.ToList());
            }
        }

    }
}
