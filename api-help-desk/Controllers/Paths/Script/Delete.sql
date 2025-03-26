DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [global].[path] SET task_id = @task_id
WHERE id = @id;

DELETE FROM [global].[path]
WHERE id = @id;