DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE [security].[domain] SET task_id = @task_id
WHERE id = @id;

DELETE FROM [security].[domain]
WHERE id = @id;