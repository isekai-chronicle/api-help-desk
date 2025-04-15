DECLARE @json NVARCHAR(MAX) = @json_parameter;

-- Declaramos tabla temporal para almacenar la estructura
DECLARE @UserAccess TABLE (
    user_id UNIQUEIDENTIFIER,
    component_id UNIQUEIDENTIFIER,
    task_id UNIQUEIDENTIFIER
);

-- Cargamos datos desde el JSON
INSERT INTO @UserAccess (user_id, component_id, task_id)
SELECT 
    JSON_VALUE(value, '$.user_id'),
    JSON_VALUE(value, '$.component_id'),
    JSON_VALUE(value, '$.task_id')
FROM OPENJSON(@json);

-- Verificamos el resultado
DECLARE @user_id UNIQUEIDENTIFIER = (SELECT TOP 1 UA.[user_id] FROM @UserAccess AS UA)

DELETE UC
FROM [security].user_component AS UC
	LEFT JOIN @UserAccess AS UA ON UC.component_id = UA.component_id AND UC.[user_id] = UA.[user_id]
WHERE UC.[user_id] = @user_id AND UA.component_id IS NULL


INSERT INTO [security].user_component ([user_id], component_id, task_id)
SELECT UA.[user_id], UA.component_id, UA.task_id
FROM @UserAccess AS UA
	LEFT JOIN [security].user_component AS UC ON UC.component_id = UA.component_id AND UC.[user_id] = UA.[user_id]
WHERE UC.component_id IS NULL