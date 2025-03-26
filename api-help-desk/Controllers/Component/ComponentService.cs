using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Component.ComponentModel;

namespace api_help_desk.Controllers.Component
{
    public class ComponentService : ComponentInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Component", "Script");
        public ComponentService(DapperContext context) => _context = context;

        public async Task<List<ComponentListOut>> Get()
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<ComponentListOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<ComponentComboOut>> GetCombo()
        {
            var sqlFilePath = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<ComponentComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<ComponentDataOut> Post(ComponentDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@displayName_parameter", data.displayName);
                parameters.Add("@area_id_parameter", data.area_id);
                parameters.Add("@path_id_parameter", data.path_id);
                parameters.Add("@isOffline_parameter", data.isOffline);
                parameters.Add("@isService_parameter", data.isService);
                parameters.Add("@isShared_parameter", data.isShared);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.QueryAsync<ComponentDataOut>(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                    return lists.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new ComponentDataOut();
            }

        }

        public async Task<ComponentDataOut> Put(ComponentDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@name_parameter", data.name);
            parameters.Add("@displayName_parameter", data.displayName);
            parameters.Add("@area_id_parameter", data.area_id);
            parameters.Add("@path_id_parameter", data.path_id);
            parameters.Add("@isOffline_parameter", data.isOffline);
            parameters.Add("@isService_parameter", data.isService);
            parameters.Add("@isShared_parameter", data.isShared);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<ComponentDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<IActionResult> Delete(ComponentDataIdIn data)
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