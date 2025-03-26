using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Role.RoleModel;

namespace api_help_desk.Controllers.Role
{
    public class RoleService : RoleInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Role", "Script");
        public RoleService(DapperContext context) => _context = context;

        public async Task<List<RoleListOut>> Get()
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<RoleListOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<RoleComboOut>> GetCombo()
        {
            var sqlFilePath = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<RoleComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<RoleDataOut> Post(RoleDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@area_id_parameter", data.area_id);
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.QueryAsync<RoleDataOut>(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                    return lists.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new RoleDataOut();
            }

        }

        public async Task<RoleDataOut> Put(RoleDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@area_id_parameter", data.area_id);
            parameters.Add("@name_parameter", data.name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<RoleDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<IActionResult> Delete(RoleDataIdIn data)
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