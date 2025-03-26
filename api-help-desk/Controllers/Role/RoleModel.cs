namespace api_help_desk.Controllers.Role
{
    public class RoleModel
    {
        public class RoleListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public Guid area_id { get; set; }
            public string area_name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class RoleComboOut
        {
            public Guid role_id { get; set; }
            public string role_name { get; set; }
        }

        public class RoleDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public Guid area_id { get; set; }
            public Guid task_id { get; set; }
        }
        public class RoleSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class RoleDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public Guid area_id { get; set; }
            public string area_name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class RoleDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class RoleDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}