namespace api_help_desk.Controllers.Domain
{
    public class DomainModel
    {
        public class DomainListOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class DomainComboOut
        {
            public Guid domain_id { get; set; }
            public string domain_name { get; set; }
        }

        public class DomainDataIn
        {
            public Guid? id { get; set; }
            public string name { get; set; }
            public Guid task_id { get; set; }
        }
        public class DomainSortIn
        {
            public Guid? id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class DomainDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class DomainDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class DomainDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}