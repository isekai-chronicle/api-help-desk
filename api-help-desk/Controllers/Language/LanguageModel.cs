namespace api_help_desk.Controllers.Language
{
    public class LanguageModel
    {
        public class LanguageListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public bool isDefault { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class LanguageComboOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public bool isDefault { get; set; }
        }

        public class LanguageDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public bool isDefault { get; set; }
            public Guid task_id { get; set; }
        }

        public class LanguageDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public bool isDefault { get; set; }
        }

        public class LanguageDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class LanguageDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}