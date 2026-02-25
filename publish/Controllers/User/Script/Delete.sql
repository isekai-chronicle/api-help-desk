DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].[user] SET task_id = @task_id
WHERE id = @id;

UPDATE [security].[userAccess] SET task_id = @task_id
WHERE user_id = @id;

DELETE FROM [security].[userAccess]
WHERE user_id = @id;

DELETE FROM [security].[user]
WHERE id = @id;