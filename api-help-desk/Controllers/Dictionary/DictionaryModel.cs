namespace api_help_desk.Controllers.Dictionary
{
    public class DictionaryModel
    {
        public class DictionaryListOut
        {
            public Guid componentObject_id { get; set; }
            public Guid? language_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string translate1 { get; set; }
            public string translate2 { get; set; }
            public int hashtag { get; set; }
            public bool isEdit { get; set; } = false;
        }

        public class DictionaryListIn
        {
            public string name { get; set; }
            public string area { get; set; }
            public Guid? language_id { get; set; }
        }

        public class DictionaryTraductorIn
        {
            public string name { get; set; }
            public string area { get; set; }
            public string language { get; set; }
        }

        public class DictionaryDataIn
        {
            public string name { get; set; }
            public string area { get; set; }
            public Guid task_id { get; set; }
            public Dictionary<string, DictionaryWordDetails> words { get; set; }
        }

        public class DictionaryTraductorOut
        {
            public string key { get; set; }
            public DictionaryTraductorDetails keyValue { get; set; }
        }

        public class DictionaryTraductorDetails
        {
            public string word { get; set; }
            public bool? disabled { get; set; }
            public bool? visible { get; set; }
        }
        public class DictionaryWordDetails
        {
            public string word { get; set; }
            public bool? disabled { get; set; }
            public bool? visible { get; set; }
        }

        public class DictionaryDataWordIn
        {
            public Guid componentObject_id { get; set; }
            public Guid? language_id { get; set; }
            public string translate1 { get; set; }
            public string translate2 { get; set; }
            public Guid task_id { get; set; }
        }

        public class DictionaryDataWordOut
        {
            public Guid componentObject_id { get; set; }
            public string translate1 { get; set; }
            public string translate2 { get; set; }
        }

        public class DictionaryDataOut
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public bool isAcive { get; set; }
        }

        public class DictionaryDataIdIn
        {
            public Guid id { get; set; }
            public Guid task_id { get; set; }

        }

        public class DictionaryDataIdOut
        {
            public Guid id { get; set; }
        }
    }
}