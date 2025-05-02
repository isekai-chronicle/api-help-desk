DECLARE @json NVARCHAR(MAX) = @json_parameter;

-- Declaramos tabla temporal para almacenar la estructura
DECLARE @UserAccess TABLE (
    user_id UNIQUEIDENTIFIER,
    component_id UNIQUEIDENTIFIER,
	componentObject_id UNIQUEIDENTIFIER,
    task_id UNIQUEIDENTIFIER
);

-- Cargamos datos desde el JSON
INSERT INTO @UserAccess (user_id, component_id, componentObject_id, task_id)
SELECT 
    JSON_VALUE(A.value, '$.user_id') AS user_id,
    JSON_VALUE(A.value, '$.component_id') AS component_id,
    B.value,
    JSON_VALUE(A.value, '$.task_id') AS task_id
FROM OPENJSON(@json) AS A
OUTER APPLY OPENJSON(A.value, '$.componentObjects_id') AS B;

-- Verificamos el resultado
DECLARE @user_id UNIQUEIDENTIFIER = (SELECT TOP 1 UA.[user_id] FROM @UserAccess AS UA)
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 UA.component_id FROM @UserAccess AS UA)

DELETE UC
FROM [security].user_component AS UC
	LEFT JOIN @UserAccess AS UA ON UC.component_id = UA.component_id AND UC.[user_id] = UA.[user_id]
WHERE UC.[user_id] = @user_id AND UA.component_id IS NULL

INSERT INTO [security].user_component ([user_id], component_id, task_id)
SELECT
    DISTINCT
     UA.[user_id], UA.component_id, UA.task_id
FROM @UserAccess AS UA
	LEFT JOIN [security].user_component AS UC ON UC.component_id = UA.component_id AND UC.[user_id] = UA.[user_id]
WHERE UC.component_id IS NULL

DELETE UO
FROM [security].user_componentObject AS UO
	LEFT JOIN @UserAccess AS UA ON UO.componentObject_id = UA.componentObject_id AND UO.[user_id] = UA.[user_id]
WHERE UO.[user_id] = @user_id AND UA.[user_id] IS NULL

INSERT INTO [security].user_componentObject (componentObject_id, [user_id], isEnable, isVisible, isReadOnly, task_id)
SELECT 
Distinct
	UA.componentObject_id, 
	UA.[user_id],
	1 AS isEnable,
	1 AS isVisible,
	0 AS isReadOnly,
	UA.task_id
FROM @UserAccess AS UA
	LEFT JOIN [security].user_componentObject AS UO ON UO.componentObject_id = UA.componentObject_id AND UO.[user_id] = UA.[user_id]
WHERE UO.[user_id] Is NULL