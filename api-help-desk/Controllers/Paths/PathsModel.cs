namespace api_help_desk.Controllers.Paths
{
    public class PathsModel
    {
        public class PathsListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string user { get; set; }
            public string password { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class PathsComboOut
        {
            public Guid path_id { get; set; }
            public string path_name { get; set; }
        }

        public class PathsDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string user { get; set; }
            public string password { get; set; }
            public Guid task_id { get; set; }
        }
        public class PathsSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class PathsDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string user { get; set; }
            public string password { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class PathsDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class PathsDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}