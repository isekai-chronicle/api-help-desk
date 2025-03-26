using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Paths.PathsModel;

namespace api_help_desk.Controllers.Paths
{
    public class PathsService : PathsInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Paths", "Script");
        public PathsService(DapperContext context) => _context = context;

        public async Task<List<PathsListOut>> Get()
        {
            var sqlFilePaths = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePaths);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<PathsListOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<PathsComboOut>> GetCombo()
        {
            var sqlFilePaths = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePaths);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<PathsComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<PathsDataOut> Post(PathsDataIn data)
        {
            var sqlFilePaths = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePaths);
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@path_parameter", data.path);
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@user_parameter", data.user);
                parameters.Add("@password_parameter", data.password);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.QueryAsync<PathsDataOut>(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                    return lists.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new PathsDataOut();
            }

        }

        public async Task<PathsDataOut> Put(PathsDataIn data)
        {
            var sqlFilePaths = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePaths);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@path_parameter", data.path);
            parameters.Add("@name_parameter", data.name);
            parameters.Add("@user_parameter", data.user);
            parameters.Add("@password_parameter", data.password);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<PathsDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<IActionResult> Delete(PathsDataIdIn data)
        {
            try
            {
                var sqlFilePaths = Path.Combine(_path, "Delete.sql");
                var sql = await File.ReadAllTextAsync(sqlFilePaths);
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