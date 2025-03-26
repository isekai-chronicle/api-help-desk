using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_help_desk.Controllers.List
{
    public class ListService : ListInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "List", "Script");

        public ListService(DapperContext context) => _context = context;

        public Task<object> Delete()
        {
            throw new NotImplementedException();
        }


        public async Task<object> Get(string user_id, string project_id)
        {

            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@user_id_parameter", new Guid(user_id));
            parameters.Add("@project_id_parameter", new Guid(project_id));

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var steps = new List<ListModel.Step>();

                var lists = await connection.QueryAsync<ListModel.Step, ListModel.TaskList, ListModel.Step>(
                    sql,
                    (step, task) =>
                    {
                        var existingStep = steps.FirstOrDefault(p => p.step_id == step.step_id);
                        if (existingStep == null)
                        {
                            step.Tasks = new List<ListModel.TaskList>();
                            steps.Add(step);
                            existingStep = step;
                        }
                        if (task != null) existingStep.Tasks.Add(task);
                        return existingStep;
                    },
                    parameters,
                    splitOn: "task_id",
                    commandTimeout: 0
                );
                return steps.ToList();
            }
        }

        public Task<object> Post()
        {
            throw new NotImplementedException();
        }

        public Task<object> Put()
        {
            throw new NotImplementedException();
        }
    }
}