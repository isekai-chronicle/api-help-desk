DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @menu_id UNIQUEIDENTIFIER = @menu_id_parameter,
    @name NVARCHAR(100) = @name_parameter,    
    @displayName NVARCHAR(100) = @displayName_parameter,      
    @menuData_id_root UNIQUEIDENTIFIER = @menuData_id_root_parameter,
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

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
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name, 
        target.displayName = @displayName,          
        target.task_id = @task_id,
        target.menu_id = @menu_id,
        target.menuData_id_root = @menuData_id_root
    OUTPUT inserted.id, inserted.name, inserted.displayName, inserted.menu_id, inserted.menuData_id_root INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id AS menuData_id,
    O.name AS menuData_name,        
    O.displayName AS menuData_displayName,  
    O.menu_id,
    O.menuData_id_root
FROM @outputTable AS O   