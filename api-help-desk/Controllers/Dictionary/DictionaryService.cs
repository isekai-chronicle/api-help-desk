using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Dictionary.DictionaryModel;
using System.Text.Json;

namespace api_help_desk.Controllers.Dictionary
{
    public class DictionaryService : DictionaryInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Dictionary", "Script");
        public DictionaryService(DapperContext context) => _context = context;

        public async Task<List<DictionaryListOut>> PostById(DictionaryListIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PostById.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                parameters = new DynamicParameters();
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@area_parameter", data.area);
                parameters.Add("@language_id_parameter", data.language_id);

                var lists = await connection.QueryAsync<DictionaryListOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<Dictionary<string, DictionaryTraductorDetails>> PostComponentById(DictionaryTraductorIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PostComponentById.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                parameters = new DynamicParameters();
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@area_parameter", data.area);
                parameters.Add("@language_parameter", data.language);

                var result = new Dictionary<string, DictionaryTraductorDetails>();

                var lists = await connection.QueryAsync<DictionaryTraductorOut, DictionaryTraductorDetails, DictionaryTraductorOut>(
                    sql,
                    (key, keyValue) =>
                    {
                        if (!result.ContainsKey(key.key))
                        {
                            result[key.key] = keyValue;
                        }
                        return key;
                    },
                    parameters,
                    splitOn: "word", // Asegúrate de que coincida con el nombre de la columna en la consulta SQL
                    commandTimeout: 0
                );

                return result;
            }
        }

        public async Task<object> Post(DictionaryDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "Post.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            try
            {
                var json = JsonSerializer.Serialize(data.words);

                parameters = new DynamicParameters();
                parameters.Add("@name_parameter", data.name);
                parameters.Add("@area_parameter", data.area);
                parameters.Add("@word_parameter", json);
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

        public async Task<List<DictionaryDataWordOut>> Put(List<DictionaryDataWordIn> data)
        {
            var sqlFilePath = Path.Combine(_path, "Put.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var json = JsonSerializer.Serialize(data);

                parameters = new DynamicParameters();
                parameters.Add("@word_parameter", json);
                parameters.Add("@task_id_parameter", data[0].task_id);

                var lists = await connection.QueryAsync<DictionaryDataWordOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<object> Delete(DictionaryDataIdIn data)
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