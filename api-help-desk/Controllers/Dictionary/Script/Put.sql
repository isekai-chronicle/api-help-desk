DECLARE        
    @word NVARCHAR(MAX) = @word_parameter,    
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

 /*convertir la variable @word a tabla json*/
DECLARE @objects AS TABLE(componentObject_id UNIQUEIDENTIFIER, translate NVARCHAR(100), language_id UNIQUEIDENTIFIER)
INSERT INTO @objects
SELECT 
    componentObject_id,
    translate,
    language_id
FROM OPENJSON(@word)
WITH (
    componentObject_id UNIQUEIDENTIFIER '$.componentObject_id',
    translate NVARCHAR(100) '$.translate',
    language_id UNIQUEIDENTIFIER '$.language_id'
);

DECLARE @outputTable TABLE (
    componentObject_id UNIQUEIDENTIFIER,
    language_id UNIQUEIDENTIFIER,
    translate NVARCHAR(50)    
);

MERGE INTO help_desk_00.frontend.componentObject_language AS target
USING (SELECT componentObject_id, translate, language_id FROM @objects) AS source
ON target.componentObject_id = source.componentObject_id
    AND target.language_id = source.language_id
WHEN MATCHED THEN
    UPDATE SET 
        target.value = source.translate,         
        target.task_id = @task_id
    OUTPUT inserted.componentObject_id, inserted.language_id, inserted.value INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    componentObject_id, 
    language_id, 
    translate    
FROM @outputTable;    
