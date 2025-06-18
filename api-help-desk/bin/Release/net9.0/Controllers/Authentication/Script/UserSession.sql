DECLARE @userName NVARCHAR(100) = @userName_parameter,
	@logIn BIT = @logIn_parameter,
	@logOut BIT = @logout_parameter,
    @password NVARCHAR(50) = @password_parameter;
    
DECLARE 
	@user_id UNIQUEIDENTIFIER = 
        ISNULL((SELECT TOP 1 U.id FROM help_desk_00.[security].[user] AS U WHERE UPPER(U.account) = UPPER(@userName)), @user_id_parameter)

IF @user_id IS NOT NULL 
BEGIN
    IF (
    SELECT 
        TOP 1 CAST(CASE WHEN COUNT(1) OVER() > 0 THEN 1 ELSE 0 END AS BIT) AS isValidate   
    FROM help_desk_00.[security].[user] AS U	
    WHERE U.id = @user_id /*AND U.[password] = [security].authentication_fn_password(@password) */
        AND U.isActive = 1) = 1
    BEGIN

        DECLARE
            @list_id UNIQUEIDENTIFIER = 'c931a469-23df-403e-a76b-215a4fd13b0b',
            @task_name NVARCHAR(100) = CONCAT((SELECT U.account FROM help_desk_00.[security].[user] AS U WHERE U.id = @user_id), '-', FORMAT(CURRENT_TIMESTAMP, 'dd-MMM-yy HH:mm')),	
            @createDate DATETIME = CURRENT_TIMESTAMP

        DECLARE @temp AS TABLE (field_id UNIQUEIDENTIFIER, project_id UNIQUEIDENTIFIER, [value] NVARCHAR(100))
        DECLARE @output AS TABLE (task_id UNIQUEIDENTIFIER)

        DECLARE @task_id UNIQUEIDENTIFIER = NULL;

        SELECT 
            @task_id = TSS.id
        FROM help_desk_00.support.task AS TSS
            INNER JOIN help_desk_00.support.project_field_task AS PFT ON TSS.id = PFT.task_id AND PFT.field_id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9' AND CAST(@user_id AS NVARCHAR(100)) = PFT.[value]
        WHERE TSS.list_id = @list_id AND TSS.isCompleted = 0 --AND TSS.isDaily = 1

        IF @task_id IS NULL AND @logIn = 1
        BEGIN 

            INSERT INTO @temp (field_id, project_id, [value])
            SELECT 
                F.id AS field_id,
                L.project_id,					
                CASE 
                    WHEN F.id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9' THEN CAST(@user_id AS NVARCHAR(50)) /*usuario designado*/
                    WHEN F.id = '1B8BAE6D-6660-495D-A11B-A133DF63581B' THEN FORMAT(@createDate, 'yyyy-MM-dd HH:mm:ss') /*fecha de creacion*/					
                ELSE 
                    ISNULL(FP.[name], '')
                END AS field_default
            FROM help_desk_00.support.list AS L	
                INNER JOIN help_desk_00.support.project_field AS PF ON PF.project_id = L.project_id AND PF.isRoot = 1
                INNER JOIN help_desk_00.support.field AS F ON F.id = PF.field_id
                LEFT JOIN help_desk_00.support.fieldParameter AS FP ON FP.field_id = PF.field_id AND FP.sort = 0
            WHERE TRIM(@task_name) != '' AND L.id = @list_id

            INSERT INTO help_desk_00.support.task (list_id, task_id_root, [name], isActive, isCompleted, isDaily)
            OUTPUT inserted.id INTO @output (task_id)
            SELECT 
                @list_id AS list_id, 
                null AS task_id_root, 
                @task_name AS [name], 
                1 AS isActive, 
                0 AS isCompleted,
                1 AS isDaily	

            INSERT INTO help_desk_00.support.project_field_task (task_id, field_id, project_id, [value])
            SELECT 
                (SELECT TOP 1 O.task_id FROM @output AS O) AS task_id,
                T.field_id, 
                T.project_id, 
                T.[value]
            FROM @temp AS T
            
            SET @task_id = (SELECT TOP 1 O.task_id FROM @output AS O)	
        END

        DECLARE @review_id UNIQUEIDENTIFIER = NULL,
                @review_start DATE = NULL, @review_end DATE = NULL

        SELECT 
            @review_id = TW.id,
            @review_start = TW.startDate,
            @review_end = TW.endDate
        FROM help_desk_00.support.taskReview AS TW 
        WHERE TW.task_id = @task_id AND (TW.startDate IS NULL OR TW.endDate IS NULL)

        IF @logIn = 1 AND @logOut = 0
        BEGIN
            If @review_id IS NULL
            BEGIN
                INSERT INTO help_desk_00.support.taskReview (task_id, createDate, startDate)
                SELECT 
                    @task_id,
                    CURRENT_TIMESTAMP,
                    CURRENT_TIMESTAMP
            END	
            IF @review_id Is NOT NULL AND @review_start IS NULL
            BEGIN
                UPDATE T
                SET T.startDate = CURRENT_TIMESTAMP
                FROM help_desk_00.support.taskReview AS T
                WHERE T.id = @review_id
            END
        END
        /*
        If @logOut = 1 AND @logIn = 0
        BEGIN
            IF @review_id Is NOT NULL AND (@review_start IS NULL AND @review_end IS NULL)
            BEGIN
                UPDATE T
                SET T.startDate = CURRENT_TIMESTAMP,
                    T.endDate = CURRENT_TIMESTAMP
                FROM help_desk_00.support.taskReview AS T
                WHERE T.id = @review_id
            END

            IF @review_id Is NOT NULL AND (@review_start IS NOT NULL AND @review_end IS NULL)
            BEGIN
                UPDATE T
                SET
                    T.endDate = CURRENT_TIMESTAMP
                FROM help_desk_00.support.taskReview AS T
                WHERE T.id = @review_id
            END
        END
        */
    END
END
