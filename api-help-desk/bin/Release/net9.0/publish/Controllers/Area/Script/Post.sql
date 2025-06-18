DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,        
    @route NVARCHAR(100) = @route_parameter,        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [security].area), 0) + 1;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    route NVARCHAR(100)    
);

MERGE INTO [security].area AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT ([name], [route], task_id)
    VALUES (@name, @route, @task_id)
OUTPUT inserted.id, inserted.name, inserted.[route] INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    id, 
    name,        
    [route] ,
    @sort AS hashtag
FROM @outputTable;    