DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,        
    @path NVARCHAR(100) = @path_parameter,        
    @user NVARCHAR(100) = @user_parameter,        
    @password NVARCHAR(100) = @password_parameter,        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [global].[path]), 0) + 1;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    path NVARCHAR(100),
    [user] NVARCHAR(100),
    password NVARCHAR(100)
);

MERGE INTO [global].[path] AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT ([name], [path], [user], password, task_id)
    VALUES (@name, @path, @user, @password, @task_id)
OUTPUT inserted.id, inserted.name, inserted.[path], inserted.[user], inserted.password INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,         
    O.[path],
    O.[user],
    O.password,
    @sort AS hashtag
FROM @outputTable AS O    