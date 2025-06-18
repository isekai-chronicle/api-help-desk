DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,                  
    @route NVARCHAR(100) = @route_parameter,                  
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    [route] NVARCHAR(100)    
);

MERGE INTO [security].area AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name,   
        target.[route] = @route,
        target.task_id = @task_id
    OUTPUT inserted.id, inserted.name, inserted.[route] INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,         
    O.[route]    
FROM @outputTable AS O;    