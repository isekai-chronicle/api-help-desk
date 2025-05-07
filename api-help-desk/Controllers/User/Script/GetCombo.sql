SELECT 
	CD.id AS [user_id],
	CONCAT(CD.name,' - ', CD.lastname) AS [user_name]
FROM [security].[user] CD
ORDER bY CD.name