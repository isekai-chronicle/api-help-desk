DECLARE
    @list NVARCHAR(MAX) = @list_parameter,                                      
    @menuData_id UNIQUEIDENTIFIER = @menuData_id_parameter,
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @input AS TABLE (
    menuData_id_component UNIQUEIDENTIFIER
)

INSERT INTO @input (menuData_id_component)
SELECT 
    menuData_id_component
FROM OPENJSON(@list)
WITH (
    menuData_id_component UNIQUEIDENTIFIER
);

UPDATE MD
SET MD.menuData_id_root = null,
    MD.task_id = @task_id
FROM [security].menuData AS MD
    LEFT JOIN @input AS I ON MD.id = I.menuData_id_component
WHERE MD.menuData_id_root = @menuData_id AND I.menuData_id_component Is nULL

UPDATE MD
SET MD.menuData_id_root = @menuData_id,
    MD.task_id = @task_id
FROM [security].menuData AS MD
    INNER JOIN @input AS I ON MD.id = I.menuData_id_component;