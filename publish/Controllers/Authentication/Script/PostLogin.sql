DECLARE
@userName NVARCHAR(50) = @userName_parameter,
@password NVARCHAR(50) = @password_parameter

 
SELECT 
	CAST(CASE WHEN COUNT(1) OVER() > 0 THEN 1 ELSE 0 END AS BIT) AS isValidate,
    U.id AS access,
    'DOM' AS country,
	'ING' AS [language],
	TS.*
FROM help_desk_00.[security].[user] AS U
	INNER JOIN help_desk_00.[security].userAccess AS US ON U.id =US.[user_id]
	OUTER APPLY (
		SELECT 
			L.name AS list,
			T.id AS [keyList]
		FROM help_desk_00.support.task AS T
			INNER JOIN help_desk_00.support.project_field_task AS PFT ON T.id = PFT.task_id AND PFT.field_id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9'
			INNER JOIN help_desk_00.support.list AS L ON L.id = T.list_id
			INNER JOIN help_desk_00.support.taskReview AS TW ON TW.task_id = T.id AND TW.startDate IS NOT NULL AND TW.endDate IS NULL
		WHERE CAST(U.id AS NVARCHAR(100)) = PFT.[value] AND T.isActive = 1 AND T.isCompleted = 0 AND T.isDaily = 1
	) AS TS
WHERE U.account = @userName AND US.[password] = HASHBYTES('SHA2_256', @password)
	AND U.isActive = 1