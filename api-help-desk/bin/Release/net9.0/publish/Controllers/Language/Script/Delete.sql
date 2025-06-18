DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;

UPDATE help_desk_00.setting.language SET task_id = @task_id
WHERE id = @id;

DELETE FROM help_desk_00.setting.language
WHERE id = @id;