namespace api_help_desk.Models
{
    public class ProjectModel
    {
        public class Project
        {
            public Guid project_id { get; set; }
            public string project_name { get; set; }
            public List<List> Lists { get; set; }
            public bool isExpanded { get; set; } = false;
        }

        public class List
        {
            public Guid list_id { get; set; }
            public string list_name { get; set; }
        }
    }


}