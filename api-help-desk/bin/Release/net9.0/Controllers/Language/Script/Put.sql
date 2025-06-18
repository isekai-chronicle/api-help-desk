DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(50) = @name_parameter,    
    @code NVARCHAR(10) = @code_parameter,    
    @isDefault BIT = @isDefault_parameter,
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(50),    
    code NVARCHAR(10),
    isDefault BIT
);

MERGE INTO help_desk_00.setting.language AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name,        
        target.isDefault = @isDefault,    
        target.code = @code,   
        target.task_id = @task_id
    OUTPUT inserted.id, inserted.name, inserted.code, inserted.isDefault INTO @outputTable;

UPDATE LL
SET LL.isDefault = 0,
    LL.task_id = @task_id
FROM help_desk_00.setting.language AS LL
    LEFT JOIN @outputTable AS O ON O.id = LL.id
WHERE O.id IS NULL AND @isDefault = 1

/*Devolver el registro actualizado o insertado*/
SELECT 
    id, 
    name, 
    code, 
    isDefault,
    CAST(0 AS BIT) AS hashtag
FROM @outputTable;    