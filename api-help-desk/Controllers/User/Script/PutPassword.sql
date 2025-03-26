DECLARE
    @id UNIQUEIDENTIFIER = @id_parameter,
    @password NVARCHAR(100) = @password_parameter,           
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;    

DECLARE @Longitud INT = 8;

DECLARE @HashedPassword VARBINARY(32) = HASHBYTES('SHA2_256', @password);

MERGE INTO [security].[userAccess] AS target
USING (SELECT @id AS user_id) AS source
ON target.user_id = source.user_id
WHEN MATCHED THEN
    UPDATE SET 
        target.password = @HashedPassword,
        target.passwordLastChangedDate = CURRENT_TIMESTAMP,
        target.expireDate = DATEADD(DAY, 30, CURRENT_TIMESTAMP),
        target.task_id = @task_id;
