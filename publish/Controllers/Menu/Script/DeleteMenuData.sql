DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].menuData SET task_id = @task_id
WHERE id = @id;

DELETE FROM [security].menuData
WHERE id = @id;