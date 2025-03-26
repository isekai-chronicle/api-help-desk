namespace api_help_desk.Controllers.Area
{
    public class AreaModel
    {
        public class AreaListOut
        {
            public Guid id { get; set; }
            public string route { get; set; }
            public string name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class AreaComboOut
        {
            public Guid area_id { get; set; }
            public string area_name { get; set; }
        }

        public class AreaDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public string route { get; set; }
            public Guid task_id { get; set; }
        }
        public class AreaSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class AreaDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string route { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class AreaDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class AreaDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}