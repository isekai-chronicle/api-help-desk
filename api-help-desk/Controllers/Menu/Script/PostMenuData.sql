DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @menu_id UNIQUEIDENTIFIER = @menu_id_parameter,
    @name NVARCHAR(100) = @name_parameter,        
    @menuData_id_root UNIQUEIDENTIFIER = @menuData_id_root_parameter,
    @component_id UNIQUEIDENTIFIER = @component_id_parameter, 
    @displayName NVARCHAR(100) = @displayName_parameter,                      
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [security].menuData), 0) + 1;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),    
    displayName NVARCHAR(100),
    menu_id UNIQUEIDENTIFIER,
    menuData_id_root UNIQUEIDENTIFIER
);

MERGE INTO [security].menuData AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT ([name], displayName, menu_id, menuData_id_root, task_id)
    VALUES (@name, @displayName, @menu_id, @menuData_id_root, @task_id)
OUTPUT inserted.id, inserted.name, inserted.displayName, inserted.menu_id, inserted.menuData_id_root INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id AS menuData_id, 
    O.name AS menuData_name,   
    O.displayName AS menuData_displayName,
    O.menuData_id_root,    
    O.menu_id,   
    @sort AS hashtag
FROM @outputTable AS O    