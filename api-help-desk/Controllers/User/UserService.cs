using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.User.UserModel;

namespace api_help_desk.Controllers.User
{
    public class UserService : UserInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "User", "Script");
        public UserService(DapperContext context) => _context = context;

        public async Task<List<UserListOut>> Get()
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<UserListOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<UserComboOut>> GetCombo()
        {
            var sqlFilePath = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<UserComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<UserDataOut> Post(UserDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@area_id_parameter", data.area_id);
                parameters.Add("@domain_id_parameter", data.domain_id);
                parameters.Add("@lastName_parameter", data.lastName);
                parameters.Add("@nickname_parameter", data.nickname);
                parameters.Add("@account_parameter", data.account);
                parameters.Add("@email_parameter", data.email);
                parameters.Add("@isActive_parameter", data.isActive);
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.QueryAsync<UserDataOut>(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                    return lists.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new UserDataOut();
            }

        }

        public async Task<UserDataOut> Put(UserDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@area_id_parameter", data.area_id);
            parameters.Add("@lastName_parameter", data.lastName);
            parameters.Add("@nickname_parameter", data.nickname);
            parameters.Add("@account_parameter", data.account);
            parameters.Add("@email_parameter", data.email);
            parameters.Add("@isActive_parameter", data.isActive);
            parameters.Add("@domain_id_parameter", data.domain_id);
            parameters.Add("@name_parameter", data.name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<UserDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<IActionResult> PutPassword(UserDataInPassword data)
        {
            var sqlFilePath = Path.Combine(_path, "PutPassword.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@password_parameter", data.password);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.ExecuteAsync(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists > 0 ? new OkResult() : new BadRequestResult();
            }
        }

        public async Task<IActionResult> Delete(UserDataIdIn data)
        {
            try
            {
                var sqlFilePath = Path.Combine(_path, "Delete.sql");
                var sql = await File.ReadAllTextAsync(sqlFilePath);
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.ExecuteAsync(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }



    }
}