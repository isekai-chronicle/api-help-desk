using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Language.LanguageModel;

namespace api_help_desk.Controllers.Language
{
    public class LanguageService : LanguageInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Language", "Script");
        public LanguageService(DapperContext context) => _context = context;

        public async Task<List<LanguageListOut>> Get()
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<LanguageListOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<LanguageComboOut>> GetCombo()
        {
            var sqlFilePath = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<LanguageComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<LanguageDataOut> Post(LanguageDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            try
            {

                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.id);
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@code_parameter", data.code);
                parameters.Add("@isDefault_parameter", data.isDefault);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.QueryAsync<LanguageDataOut>(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                    return lists.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new LanguageDataOut();
            }

        }

        public async Task<LanguageDataOut> Put(LanguageDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.id);
            parameters.Add("@name_parameter", data.name);
            parameters.Add("@code_parameter", data.code);
            parameters.Add("@isDefault_parameter", data.isDefault);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<LanguageDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<object> Delete(LanguageDataIdIn data)
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}