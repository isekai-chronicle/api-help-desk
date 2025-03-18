DECLARE @user_id UNIQUEIDENTIFIER = @user_id_paramater

SELECT 
	PJC.id AS project_id,
	PJC.[name] AS project_name,
LST.list_id,
	LST.list_name
FROM support.project AS PJC
INNER JOIN support.project_user AS PJU ON PJC.id = PJU.project_id
	OUTER APPLY (
SELECT 
			LST.id AS list_id,
		LST.[name] AS list_name	FROM support.list AS LST 
	INNER JOIN support.list_user AS LSU ON LST.id = LSU.list_id
		WHERE LST.project_id = PJC.id AND LSU.[user_id] = @user_id 
	) AS LST
WHERE PJU.[user_id] = @user_id

