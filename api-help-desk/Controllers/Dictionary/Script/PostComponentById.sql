DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @area NVARCHAR(50) = @area_parameter,
	@language NVARCHAR(50) = @language_parameter,
	@task UNIQUEIDENTIFIER = @task_id_parameter;

DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)
DECLARE @language_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM setting.language AS COM WHERE UPPER(COM.code) = UPPER(@language))

DECLARE @user NVARCHAR(50) = (
SELECT 
	U.account
FROM support.task AS T
	INNER JOIN support.project_field_task AS PFT ON T.id = PFT.task_id
	INNER JOIN [security].[user] AS U ON U.id = PFT.value
WHERE T.isDaily = 1 AND T.id = @task AND PFT.field_id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9')

SELECT 			
	COO.id AS componentObject_id,
	COO.name AS [key], 	
	COL.value AS word,
	CAST(0 AS BIT) AS [disabled],
	ISNULL(CAST(
		CASE 
			WHEN COO.id = '1E30F32C-5181-4789-82E2-5AEF05364771' 
				AND @user IN ('p_ovalles4223','d_suarez1050', 'r_aburto9293', 'j_ruiz3854', 'v_aleman9081') 
			THEN 1 
			WHEN @user IS NULL THEN 0
			WHEN COO.id != '1E30F32C-5181-4789-82E2-5AEF05364771' THEN 1 END AS BIT), 0) AS [visible]
FROM frontend.componentObject AS COO 
	LEFT JOIN frontend.componentObject_language AS COL ON COL.componentObject_id = COO.id 
WHERE COO.component_id = @component_id AND COL.language_id = @language_id





