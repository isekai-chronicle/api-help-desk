DECLARE @id UNIQUEIDENTIFIER = @id_parameter,
        @task_id UNIQUEIDENTIFIER = @task_id_parameter;


DELETE COL
FROM
    [frontend].componentObject_language COL
    INNER JOIN frontend.componentObject cO on COL.componentObject_id = cO.id
WHERE CO.component_id = @id

DELETE COL
FROM
    [frontend].componentObject_event COL
    INNER JOIN frontend.componentObject cO on COL.componentObject_id = cO.id
WHERE CO.component_id = @id

DELETE FROM [frontend].component_component
WHERE component_id_child = @id;

DELETE FROM [frontend].component_component
WHERE component_id_root = @id;

DELETE COM
FROM security.user_componentObject AS COM
    INNER JOIN [frontend].componentObject AS U ON COM.componentObject_id = U.id
WHERE U.component_id = @id;

DELETE COM
FROM security.user_component AS COM
WHERE COM.component_id = @id;

DELETE FROM [frontend].componentObject
WHERE component_id = @id;

DELETE FROM security.menuData
WHERE component_id = @id;

DELETE FROM [frontend].component
WHERE id = @id;


