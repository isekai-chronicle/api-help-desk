DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,            
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [security].menu), 0) + 1;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100)    
);

MERGE INTO [security].menu AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT ([name], task_id)
    VALUES (@name, @task_id)
OUTPUT inserted.id, inserted.name INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id AS menu_id, 
    O.name AS menu_name,             
    @sort AS hashtag
FROM @outputTable AS O    