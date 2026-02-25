DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,        
    @path NVARCHAR(100) = @path_parameter,        
    @user NVARCHAR(100) = @user_parameter,        
    @password NVARCHAR(100) = @password_parameter,        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;    

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
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name,   
        target.[path] = @path,
        target.[user] = @user,
        target.[password] = @password,
        target.task_id = @task_id
    OUTPUT inserted.id, inserted.name, inserted.[path], inserted.[user], inserted.password INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,         
    O.[path],
    O.[user],
    O.password,
    0 AS hashtag
FROM @outputTable AS O    