namespace api_help_desk.Controllers.Task
{
    public interface TaskInterface
    {
        public Task<List<dynamic>> Get();

        public Task<List<dynamic>> Post();

        public Task<List<dynamic>> Put();

        public Task<List<dynamic>> Delete();

    }
}
