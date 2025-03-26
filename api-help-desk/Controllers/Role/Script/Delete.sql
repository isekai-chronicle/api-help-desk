DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].role SET task_id = @task_id
WHERE id = @id;

DELETE FROM [security].role
WHERE id = @id;