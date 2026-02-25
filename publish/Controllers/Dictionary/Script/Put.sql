DECLARE        
    @word NVARCHAR(MAX) = @word_parameter,    
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

 /*convertir la variable @word a tabla json*/
DECLARE @objects AS TABLE(componentObject_id UNIQUEIDENTIFIER, translate1 NVARCHAR(100), translate2 NVARCHAR(100), language_id UNIQUEIDENTIFIER)
INSERT INTO @objects
SELECT 
    componentObject_id,
    translate1,
    translate2,
    language_id
FROM OPENJSON(@word)
WITH (
    componentObject_id UNIQUEIDENTIFIER '$.componentObject_id',
    translate1 NVARCHAR(100) '$.translate1',
    translate2 NVARCHAR(100) '$.translate2',
    language_id UNIQUEIDENTIFIER '$.language_id'
);

DECLARE @outputTable TABLE (
    componentObject_id UNIQUEIDENTIFIER,
    language_id UNIQUEIDENTIFIER,
    translate1 NVARCHAR(50),
    translate2 NVARCHAR(50)   
);

DECLARE @language_id_01 UNIQUEIDENTIFIER = 'ECB5CBEF-F82A-462F-B9FE-25D52AD83138'
DECLARE @language_id_02 UNIQUEIDENTIFIER = '35AEEDDB-72F1-49A6-8EE7-D5E9D37DBE94'

MERGE INTO help_desk_00.frontend.componentObject_language AS target
USING (SELECT componentObject_id, translate1, @language_id_01 AS language_id FROM @objects) AS source
ON target.componentObject_id = source.componentObject_id
    AND target.language_id = source.language_id
WHEN MATCHED THEN
    UPDATE SET 
        target.value = source.translate1,         
        target.task_id = @task_id
    OUTPUT inserted.componentObject_id, inserted.language_id, inserted.value, '' INTO @outputTable;

MERGE INTO help_desk_00.frontend.componentObject_language AS target
USING (SELECT componentObject_id, translate2, @language_id_02 AS language_id FROM @objects) AS source
ON target.componentObject_id = source.componentObject_id
    AND target.language_id = source.language_id
WHEN MATCHED THEN
    UPDATE SET 
        target.value = source.translate2,         
        target.task_id = @task_id
    OUTPUT inserted.componentObject_id, inserted.language_id, '', inserted.value INTO @outputTable;


/*Devolver el registro actualizado o insertado*/
SELECT 
    componentObject_id, 
    MAX(language_id) AS language_id, 
    MAX(translate1) AS translate1,
    MAX(translate2) AS translate2 
FROM @outputTable
GROUP BY componentObject_id
