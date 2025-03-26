using api_help_desk.Context;
using Dapper;
using System.Threading.Tasks;

namespace api_help_desk.Controllers.Project
{
    public class ProjectService : ProjectInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Project", "Script");

        public ProjectService(DapperContext Context) => _context = Context;

        public Task<object> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<object> Get(string user_id)
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            string sql;

            try { sql = await File.ReadAllTextAsync(sqlFilePath); }
            catch (Exception e) { throw new Exception(e.Message); }

            parameters = new DynamicParameters();
            parameters.Add("@user_id_paramater", user_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var projects = new List<ProjectModel.Project>();

                await connection.QueryAsync<ProjectModel.Project, ProjectModel.List, ProjectModel.Project>(
                  sql,
                  (project, list) =>
                  {
                      var existingProject = projects.FirstOrDefault(p => p.project_id == project.project_id);
                      if (existingProject == null)
                      {
                          project.Lists = new List<ProjectModel.List>();
                          projects.Add(project);
                          existingProject = project;
                      }
                      if (list != null) existingProject.Lists.Add(list);
                      return existingProject;
                  },
                  parameters,
                  splitOn: "list_id",
                  commandTimeout: 0
              );

                return projects.ToList();
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