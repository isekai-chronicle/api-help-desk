SELECT 	
	CT.id, 
	CT.name, 
	CT.lastName, 
	CT.nickname, 
	CT.account, 
	CT.email, 
	CT.isActive,
	A.id AS area_id,
	A.name AS area_name,
	D.id AS domain_id,
	D.name AS domain_name,
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [security].[user] CT	
	LEFT JOIN [security].area AS A ON A.id = CT.area_id
	LEFT JOIN [security].domain AS D ON D.id = CT.domain_id
ORDER BY hashtag