DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].menu SET task_id = @task_id
WHERE id = @id;

DELETE FROM [security].menu
WHERE id = @id;