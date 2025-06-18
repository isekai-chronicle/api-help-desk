DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @area NVARCHAR(50) = @area_parameter,
	@language NVARCHAR(50) = @language_parameter,
	@task UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)
DECLARE @language_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM setting.language AS COM WHERE UPPER(COM.code) = UPPER(@language))

DECLARE @user_id UNIQUEIDENTIFIER = (
SELECT 
	U.id
FROM support.task AS T
	INNER JOIN support.project_field_task AS PFT ON T.id = PFT.task_id
	INNER JOIN [security].[user] AS U ON U.id = PFT.value
WHERE T.isDaily = 1 AND T.id = @task AND PFT.field_id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9')

SELECT 			
	COO.id AS componentObject_id,
	COO.name AS [key], 	
	COL.value AS word,
	ISNULL(~UCO.isEnable, 1) AS [disabled],
	CAST(1 AS BIT) AS [visible]
FROM frontend.componentObject AS COO 
	INNER JOIN frontend.component AS CC ON CC.id = COO.component_id 
	LEFT JOIN frontend.componentObject_language AS COL ON COL.componentObject_id = COO.id 
	LEFT JOIN [security].user_componentObject AS UCO ON COO.id = UCO.componentObject_id AND UCO.[user_id] = @user_id
WHERE COO.component_id = @component_id AND COL.language_id = @language_id 



