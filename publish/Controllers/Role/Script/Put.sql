DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,                  
    @area_id UNIQUEIDENTIFIER = @area_id_parameter,                  
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    area_id UNIQUEIDENTIFIER   
);

MERGE INTO [security].role AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name,   
        target.[area_id] = @area_id,
        target.task_id = @task_id
    OUTPUT inserted.id, inserted.name, inserted.[area_id] INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,         
    A.id AS area_id,
    A.name AS area_name    
FROM @outputTable AS O
    INNER JOIN [security].area AS A ON A.id = O.area_id;    