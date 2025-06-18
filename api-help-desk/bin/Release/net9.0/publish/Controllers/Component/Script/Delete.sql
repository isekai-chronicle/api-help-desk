DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [frontend].component SET task_id = @task_id
WHERE id = @id;

DELETE FROM [frontend].component
WHERE id = @id;