DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,        
    @area_id UNIQUEIDENTIFIER = @area_id_parameter,        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [security].role), 0) + 1;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    area_id UNIQUEIDENTIFIER  
);

MERGE INTO [security].role AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT ([name], [area_id], task_id)
    VALUES (@name, @area_id, @task_id)
OUTPUT inserted.id, inserted.name, inserted.[area_id] INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,         
    A.id AS area_id,
    A.name AS area_name,
    @sort AS hashtag
FROM @outputTable AS O
    INNER JOIN [security].area AS A ON A.id = O.area_id;    