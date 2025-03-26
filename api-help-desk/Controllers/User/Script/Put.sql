DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @name NVARCHAR(100) = @name_parameter,       
    @lastName NVARCHAR(100) = @lastName_parameter,
    @nickname NVARCHAR(100) = @nickname_parameter,
    @account NVARCHAR(100) = @account_parameter,
    @email NVARCHAR(100) = @email_parameter,
    @isActive BIT = @isActive_parameter,
    @area_id UNIQUEIDENTIFIER = @area_id_parameter,        
    @domain_id UNIQUEIDENTIFIER = @domain_id_parameter,        
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;    

DECLARE @outputTable TABLE (
    id UNIQUEIDENTIFIER,
    name NVARCHAR(100),
    lastName NVARCHAR(100),
    nickname NVARCHAR(100),
    account NVARCHAR(100),
    email NVARCHAR(100),
    isActive BIT,
    area_id UNIQUEIDENTIFIER ,
    domain_id UNIQUEIDENTIFIER
);

MERGE INTO [security].[user] AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        target.[name] = @name,   
        target.[lastName] = @lastName,
        target.[nickname] = @nickname,
        target.[account] = @account,
        target.[email] = @email,
        target.[isActive] = @isActive,
        target.[area_id] = @area_id,
        target.[domain_id] = @domain_id,
        target.[task_id] = @task_id

OUTPUT inserted.id, inserted.name, inserted.lastName, inserted.nickname, inserted.account, inserted.email, inserted.isActive, inserted.area_id, inserted.domain_id INTO @outputTable;

/*Devolver el registro actualizado o insertado*/
SELECT 
    O.id, 
    O.name,       
    O.lastName,
    O.nickname,
    O.account,
    O.email,
    O.isActive,        
    A.id AS area_id,
    A.name AS area_name,
    D.id AS domain_id,
    D.name AS domain_name    
FROM @outputTable AS O
    INNER JOIN [security].area AS A ON A.id = O.area_id
    LEFT JOIN [security].domain AS D ON D.id = O.domain_id