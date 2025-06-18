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
    @task_id UNIQUEIDENTIFIER = @task_id_parameter,
    @sort INT = ISNULL((SELECT COUNt(1) FROM [security].[user]), 0) + 1;

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

DECLARE @random INT = ABS(CHECKSUM(NEWID())) % 10000;
SET @account =
    LEFT(@name, 2)
    + '_'
    + @lastName
    + RIGHT('0000' + CAST(@random AS VARCHAR(4)), 4);

DECLARE @Longitud INT = 8;
DECLARE @PasswordRandom NVARCHAR(100) = N'';
DECLARE @AllowedChars VARCHAR(60) = 'ABCDEFGHIJKLMNPQRSTUVWXYZabcdef@#*!-_+ghijklmnopqrstuvwxyz123456789';

WITH CTE_Aleatorio AS
(
    SELECT TOP (@Longitud)
        SUBSTRING(@AllowedChars, 1 + (ABS(CHECKSUM(NEWID())) % LEN(@AllowedChars)), 1) AS Caracter
    FROM sys.objects 
)
SELECT @PasswordRandom = STRING_AGG(Caracter, '')
FROM CTE_Aleatorio;

DECLARE @HashedPassword VARBINARY(32) = HASHBYTES('SHA2_256', @PasswordRandom);


MERGE INTO [security].[user] AS target
USING (SELECT @id AS id) AS source
ON target.id = source.id
WHEN NOT MATCHED THEN
    INSERT (name, lastName, nickname, account, email, isActive, area_id, domain_id, task_id)
    VALUES (@name, @lastName, @nickname, @account, @email, @isActive, @area_id, @domain_id, @task_id)
OUTPUT inserted.id, inserted.name, inserted.lastName, inserted.nickname, inserted.account, inserted.email, inserted.isActive, inserted.area_id, inserted.domain_id INTO @outputTable;

SET @id = (SELECT TOP 1 O.id FROM @outputTable AS O)

MERGE INTO [security].[userAccess] AS target
USING (SELECT @id AS user_id) AS source
ON target.user_id = source.user_id
WHEN NOT MATCHED THEN
    INSERT (user_id, password, passwordLastChangedDate, 
        expireDate, isLockedOut, lockOutUntilDate, 
        lastFailedLoginDate, failedLoginAttempt, task_id)
    VALUES (@id, @HashedPassword, CURRENT_TIMESTAMP,
        DATEADD(DAY, 0, CURRENT_TIMESTAMP), 0, null,
        null, 0, @task_id);


/* Return the updated or inserted record */
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
    D.name AS domain_name,
    @sort AS hashtag,
    @PasswordRandom AS [password]
FROM @outputTable AS O
    INNER JOIN [security].area AS A ON A.id = O.area_id
    LEFT JOIN [security].domain AS D ON D.id = O.domain_id
