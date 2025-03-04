using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using api_help_desk.Context;

namespace api_help_desk.Controllers.Task
{
    public class TaskRepository : TaskInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        public TaskRepository(DapperContext Context) => _context = Context;

        public Task<List<dynamic>> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<List<dynamic>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<dynamic>> Post()
        {
            throw new NotImplementedException();
        }

        public Task<List<dynamic>> Put()
        {
            throw new NotImplementedException();
        }
    }
}
