namespace api_help_desk.Controllers.Component
{
    public class ComponentModel
    {
        public class ComponentListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public Guid area_id { get; set; }
            public string area_name { get; set; }
            public Guid? path_id { get; set; }
            public string path_name { get; set; }
            public bool isOffline { get; set; }
            public bool isService { get; set; }
            public bool isShared { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class ComponentComboOut
        {
            public Guid component_id { get; set; }
            public string component_name { get; set; }
        }

        public class ComponentDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public Guid area_id { get; set; }
            public Guid? path_id { get; set; }
            public bool isOffline { get; set; }
            public bool isService { get; set; }
            public bool isShared { get; set; }
            public Guid task_id { get; set; }
        }
        public class ComponentSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class ComponentDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public Guid area_id { get; set; }
            public string area_name { get; set; }
            public Guid? path_id { get; set; }
            public string path_name { get; set; }
            public bool isOffline { get; set; }
            public bool isService { get; set; }
            public bool isShared { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class ComponentDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class ComponentDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}