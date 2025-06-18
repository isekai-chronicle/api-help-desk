DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].area SET task_id = @task_id
WHERE id = @id;

DELETE FROM [security].area
WHERE id = @id;