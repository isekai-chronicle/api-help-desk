namespace api_help_desk.Controllers.Security.Authentication
{
    public class AuthenticationModel
    {
    }

    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class UserId
    {
        public Guid user_id { get; set; }
    }

    public class Config
    {
        public Guid access { get; set; }
        public bool isValidate { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public List<TaskList> tasks { get; set; }
        public string? key { get; set; }
    }

    public class UserAccess
    {
        public Guid user_id { get; set; }
        public Guid component_id { get; set; }
        public Guid task_id { get; set; }

    }

    public class TaskList
    {
        public string list { get; set; }
        public Guid keyList { get; set; }
    }

    public class UserNameResponse
    {
        public virtual string userName { get; set; }
    }

    public class MenuModel
    {
        public virtual Guid id { get; set; }
        public virtual Guid? menuData_id_root { get; set; }
        public virtual Guid? component_id { get; set; }
        public virtual string menu_name { get; set; }
        public virtual string icon { get; set; }
        public virtual string image { get; set; }
        public virtual string menu_displayName { get; set; }
        public virtual string path { get; set; }
        public virtual bool isDisabled { get; set; }
        public virtual bool isActive { get; set; }
        public virtual int sort { get; set; }
        public virtual bool isCheck { get; set; }
    }
}
