namespace api_help_desk.Controllers.Menu
{
    public class MenuModel
    {
        public class MenuListOut
        {
            public Guid menu_id { get; set; }
            public string menu_name { get; set; }

            public List<MenuData> menudatas { get; set; }

            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class MenuData
        {
            public Guid menuData_id { get; set; }
            public string menuData_name { get; set; }
            public string menuData_displayName { get; set; }
            public string menuData_route { get; set; }
            public Guid? menuData_id_root { get; set; }
            public string? menuData_name_root { get; set; }
            public List<ComponentData> componentDatas { get; set; }
        }

        public class ComponentData
        {
            public Guid? menuData_id_component { get; set; }
            public Guid? component_id { get; set; }
            public string? menuData_name_component { get; set; }
            public List<ComponentDataObject> componentDataObjects { get; set; }
        }

        public class ComponentDataObject
        {
            public Guid? componentObject_id { get; set; }
            public string? componentObject_name { get; set; }
        }

        public class MenuListComponentOut
        {
            public List<MenuComponent> link { get; set; }
            public List<MenuComponent> unLink { get; set; }
        }

        public class MenuComponent
        {
            public Guid menu_id { get; set; }
            public string menu_name { get; set; }

            public Guid menuData_id_component { get; set; }
            public string menuData_name_component { get; set; }

            public int hashtag { get; set; }
            public bool isCheck { get; set; } = false;
        }

        public class MenuComboOut
        {
            public Guid menu_id { get; set; }
            public string menu_name { get; set; }
        }

        public class MenuDataComboOut
        {
            public Guid menuData_id { get; set; }
            public string menuData_name { get; set; }
        }

        public class MenuDataIn
        {
            public Guid? menu_id { get; set; }
            public string menu_name { get; set; }
            public Guid task_id { get; set; }
        }

        public class MenuDataComponentDataIn
        {
            public Guid? menuData_id { get; set; }
            public List<component> components { get; set; }
            public Guid task_id { get; set; }
        }

        public class component
        {
            public Guid? menuData_id_component { get; set; }

        }

        public class MenuDataDataIn
        {
            public Guid? menuData_id { get; set; }
            public string menuData_name { get; set; }
            public Guid menu_id { get; set; }
            public Guid? menuData_id_root { get; set; }
            public Guid? component_id { get; set; }
            public string menuData_displayName { get; set; }
            public Guid task_id { get; set; }
        }

        public class MenuSortIn
        {
            public Guid? menu_id { get; set; }
            public int hashtag { get; set; }
            public Guid task_id { get; set; }
        }


        public class MenuDataOut
        {
            public Guid menu_id { get; set; }
            public string menu_name { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class MenuDataDataOut
        {
            public Guid menuData_id { get; set; }
            public string menuData_name { get; set; }
            public Guid menu_id { get; set; }
            public Guid? menuData_id_root { get; set; }
            public string? menuData_name_root { get; set; }
            public Guid? component_id { get; set; }
            public string? menuData_name_component { get; set; }
            public string menuData_displayName { get; set; }
            public string menuData_route { get; set; }
            public int hashtag { get; set; }
            public bool isCancel { get; set; } = false;
            public bool isView { get; set; } = false;
            public bool isEdit { get; set; } = true;
            public bool isDelete { get; set; } = true;
            public bool isConfirmDelete { get; set; } = false;
        }

        public class MenuDataIdIn
        {
            public Guid menu_id { get; set; }
            public Guid task_id { get; set; }

        }

        public class MenuDataDataIdIn
        {
            public Guid menuData_id { get; set; }
            public Guid task_id { get; set; }

        }

        public class MenuDataIdOut
        {
            public Guid menu_id { get; set; }
        }

        public class MenuDataDataIdOut
        {
            public Guid menuData_id { get; set; }
        }
    }
}