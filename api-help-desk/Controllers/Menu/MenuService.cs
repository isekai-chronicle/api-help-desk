using api_help_desk.Context;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using static api_help_desk.Controllers.Menu.MenuModel;
using System.Text.Json;

namespace api_help_desk.Controllers.Menu
{
    public class MenuService : MenuInterface
    {
        private readonly DapperContext _context;
        private DynamicParameters parameters;
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "Menu", "Script");
        public MenuService(DapperContext context) => _context = context;

        public async Task<List<MenuListOut>> Get(Guid user_id)
        {
            var sqlFilePath = Path.Combine(_path, "Get.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            var configs = new List<MenuListOut>();
            parameters = new DynamicParameters();
            parameters.Add("@user_id_parameter", user_id);
            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var result = await connection.QueryAsync<MenuListOut, MenuData, ComponentData, ComponentDataObject, MenuListOut>(
               sql,
               (menu, menuData, componentData, componentObject) =>
               {
                   if (menu == null)
                       return null;

                   var menuEntry = configs.FirstOrDefault(x => x.menu_id == menu.menu_id);
                   if (menuEntry == null)
                   {
                       menuEntry = new MenuListOut
                       {
                           menu_id = menu.menu_id,
                           menu_name = menu.menu_name,
                           hashtag = menu.hashtag,
                           isCancel = menu.isCancel,
                           isView = menu.isView,
                           isEdit = menu.isEdit,
                           isDelete = menu.isDelete,
                           isConfirmDelete = menu.isConfirmDelete,
                           menudatas = new List<MenuData>()
                       };
                       configs.Add(menuEntry);
                   }

                   if (menuData != null && menuData.menuData_id != Guid.Empty)
                   {
                       var menuDataEntry = menuEntry.menudatas.FirstOrDefault(md => md.menuData_id == menuData.menuData_id);
                       if (menuDataEntry == null)
                       {
                           menuDataEntry = new MenuData
                           {
                               menuData_id = menuData.menuData_id,
                               menuData_name = menuData.menuData_name,
                               menuData_displayName = menuData.menuData_displayName,
                               menuData_route = menuData.menuData_route,
                               menuData_id_root = menuData.menuData_id_root,
                               menuData_name_root = menuData.menuData_name_root,
                               componentDatas = new List<ComponentData>()
                           };
                           menuEntry.menudatas.Add(menuDataEntry);
                       }

                       if (componentData != null && componentData.menuData_id_component.HasValue)
                       {
                           var componentEntry = menuDataEntry.componentDatas.FirstOrDefault(cd => cd.menuData_id_component == componentData.menuData_id_component);
                           if (componentEntry == null)
                           {
                               componentEntry = new ComponentData
                               {
                                   menuData_id_component = componentData.menuData_id_component,
                                   component_id = componentData.component_id,
                                   isCheck = componentData.isCheck,
                                   menuData_name_component = componentData.menuData_name_component,
                                   componentDataObjects = new List<ComponentDataObject>(),
                                   componentObjects_id = new List<Guid>()
                               };
                               menuDataEntry.componentDatas.Add(componentEntry);
                           }

                           if (componentObject != null && componentObject.componentObject_id.HasValue)
                           {
                               if (componentObject.componentObject_id_user.HasValue)
                               {
                                   componentEntry.componentObjects_id.Add(new Guid(componentObject.componentObject_id_user.ToString()));
                               }

                               componentEntry.componentDataObjects.Add(new ComponentDataObject
                               {
                                   componentObject_id = componentObject.componentObject_id,
                                   componentObject_name = componentObject.componentObject_name
                               });

                           }
                       }
                   }

                   return menuEntry;
               },
               parameters,
               splitOn: "menuData_id,menuData_id_component,componentObject_id",
               commandTimeout: 0
           );
                return configs.ToList();
            }
        }


        public async Task<MenuListComponentOut> GetMenuComponent(Guid menuData_id)
        {
            var sqlFilePath = Path.Combine(_path, "GetMenuComponent.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@menuData_id_parameter", menuData_id);

            var configs = new MenuListComponentOut();

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                using (var result = await connection.QueryMultipleAsync(sql, parameters, commandTimeout: 0))
                {
                    configs.link = result.Read<MenuComponent>().ToList();
                    configs.unLink = result.Read<MenuComponent>().ToList();
                }
            }

            return configs;
        }



        public async Task<List<MenuComboOut>> GetCombo()
        {
            var sqlFilePath = Path.Combine(_path, "GetCombo.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuComboOut>(
                    sql,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<List<MenuDataComboOut>> GetComboMenuData(Guid menu_id)
        {
            var sqlFilePath = Path.Combine(_path, "GetComboMenuData.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@menu_id_parameter", menu_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataComboOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList();
            }
        }

        public async Task<MenuDataOut> PostMenu(MenuDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PostMenu.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.menu_id);
            parameters.Add("@name_parameter", data.menu_name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<MenuDataDataOut> PostMenuData(MenuDataDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PostMenuData.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.menuData_id);
            parameters.Add("@menu_id_parameter", data.menu_id);
            parameters.Add("@menuData_id_root_parameter", data.menuData_id_root);
            parameters.Add("@component_id_parameter", data.component_id);
            parameters.Add("@displayName_parameter", data.menuData_displayName);
            parameters.Add("@name_parameter", data.menuData_name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<MenuDataOut> PutMenu(MenuDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PutMenu.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.menu_id);
            parameters.Add("@name_parameter", data.menu_name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<MenuDataDataOut> PutMenuData(MenuDataDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PutMenuData.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            parameters = new DynamicParameters();
            parameters.Add("@id_parameter", data.menuData_id);
            parameters.Add("@menu_id_parameter", data.menu_id);
            parameters.Add("@menuData_id_root_parameter", data.menuData_id_root);
            parameters.Add("@component_id_parameter", data.component_id);
            parameters.Add("@displayName_parameter", data.menuData_displayName);
            parameters.Add("@name_parameter", data.menuData_name);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return lists.ToList().FirstOrDefault();
            }
        }

        public async Task<IActionResult> PutMenuDataComponent(MenuDataComponentDataIn data)
        {
            var sqlFilePath = Path.Combine(_path, "PutMenuDataComponent.sql");
            var sql = await File.ReadAllTextAsync(sqlFilePath);

            var list = JsonSerializer.Serialize(data.components);

            parameters = new DynamicParameters();
            parameters.Add("@menuData_id_parameter", data.menuData_id);
            parameters.Add("@list_parameter", list);
            parameters.Add("@task_id_parameter", data.task_id);

            using (var connection = _context.CreateConnection("", "helpdesk"))
            {
                var lists = await connection.QueryAsync<MenuDataDataOut>(
                    sql,
                    parameters,
                    commandTimeout: 0
                );
                return null;
            }
        }

        public async Task<IActionResult> Delete(MenuDataIdIn data)
        {
            try
            {
                var sqlFilePath = Path.Combine(_path, "Delete.sql");
                var sql = await File.ReadAllTextAsync(sqlFilePath);
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.menu_id);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.ExecuteAsync(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task<IActionResult> DeleteData(MenuDataDataIdIn data)
        {
            try
            {
                var sqlFilePath = Path.Combine(_path, "DeleteMenuData.sql");
                var sql = await File.ReadAllTextAsync(sqlFilePath);
                parameters = new DynamicParameters();
                parameters.Add("@id_parameter", data.menuData_id);
                parameters.Add("@task_id_parameter", data.task_id);

                using (var connection = _context.CreateConnection("", "helpdesk"))
                {
                    var lists = await connection.ExecuteAsync(
                        sql,
                        parameters,
                        commandTimeout: 0
                    );
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }


    }
}