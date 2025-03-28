DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @menu_id UNIQUEIDENTIFIER = @menu_id_parameter,
    @name NVARCHAR(100) = @name_parameter,    
    @displayName NVARCHAR(100) = @displayName_parameter,                                        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    displayName NVARCHAR(100),
    menu_id UNIQUEIDENTIFIER
);

MERGE INTO [security].menuData AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name, 
        target.displayName = @displayName,          
        target.task_id = @task_id,
        target.menu_id = @menu_id
    OUTPUT inserted.id, inserted.name, inserted.displayName, inserted.menu_id INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,        
    O.displayName,    
    O.menu_id    
FROM @outputTable AS O   