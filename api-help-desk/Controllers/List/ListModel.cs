namespace api_help_desk.Controllers.List
{
    public class ListModel
    {
        public class TaskList
        {
            public Guid task_id { get; set; }
            public string task_name { get; set; }
            public Guid list_id { get; set; }
            public string list_name { get; set; }
            public string status_name { get; set; }
            public string priority_name { get; set; }
            public string step_name { get; set; }
            public string createdDate { get; set; }
            public string terminateDate { get; set; }
            public string closeDate { get; set; }
            public string user_name { get; set; }
            public string counterTask { get; set; }
        }

        public class Step
        {
            public int sort { get; set; }
            public Guid step_id { get; set; }
            public string step_group { get; set; }
            public List<TaskList> Tasks { get; set; }
        }
    }
}