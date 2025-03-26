namespace api_help_desk.Controllers.User
{
    public class UserModel
    {
        public class UserListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string lastName { get; set; }
            public string nickname { get; set; }
            public string account { get; set; }
            public string email { get; set; }
            public bool isActive { get; set; }
            public Guid? area_id { get; set; }
            public string? area_name { get; set; }
            public Guid? domain_id { get; set; }
            public string? domain_name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class UserComboOut
        {
            public Guid user_id { get; set; }
            public string user_name { get; set; }
        }

        public class UserDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public string lastName { get; set; }
            public string nickname { get; set; }
            public string account { get; set; }
            public string email { get; set; }
            public bool isActive { get; set; }
            public Guid area_id { get; set; }
            public Guid? domain_id { get; set; }
            public Guid task_id { get; set; }
        }

        public class UserDataInPassword
        {
            public Guid? id { get; set; }
            public string password { get; set; }
            public Guid task_id { get; set; }
        }

        public class UserSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class UserDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string lastName { get; set; }
            public string nickname { get; set; }
            public string account { get; set; }
            public string email { get; set; }
            public string password { get; set; } = "";
            public bool isActive { get; set; }
            public Guid? area_id { get; set; }
            public string? area_name { get; set; }
            public Guid? domain_id { get; set; }
            public string? domain_name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class UserDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class UserDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}