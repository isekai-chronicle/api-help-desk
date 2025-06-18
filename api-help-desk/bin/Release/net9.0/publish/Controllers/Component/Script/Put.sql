DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,
    @displayName NVARCHAR(100) = @displayName_parameter,
    @area_id UNIQUEIDENTIFIER = @area_id_parameter,    
    @path_id UNIQUEIDENTIFIER = @path_id_parameter,    
    @isOffline BIT = @isOffline_parameter,
    @isService BIT = @isService_parameter,
    @isShared BIT = @isShared_parameter,  
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;    

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    displayName NVARCHAR(100),
    area_id UNIQUEIDENTIFIER,    
    path_id UNIQUEIDENTIFIER,    
    isOffline BIT,
    isService BIT,
    isShared BIT
);

MERGE INTO [frontend].component AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        name = @name,
        displayName = @displayName,
        area_id = @area_id,
        path_id = @path_id,
        isOffline = @isOffline,
        isService = @isService,
        isShared = @isShared,
        task_id = @task_id
OUTPUT inserted.id, inserted.name, inserted.displayName, inserted.area_id, inserted.path_id, inserted.isOffline, inserted.isService, inserted.isShared INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 	
	CT.id, 
	CT.name, 
	CT.displayName,
	CT.isOffline,
	CT.isService,
	CT.isShared,
	A.id AS area_id,
	A.name AS area_name,
	P.id AS path_id,
	P.name AS path_name		
FROM @outputTable CT	
	INNER JOIN [security].area AS A ON A.id = CT.area_id
	LEFT JOIN [global].[path] AS P ON P.id = CT.path_id

