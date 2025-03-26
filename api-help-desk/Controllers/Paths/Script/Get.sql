SELECT 	
	CT.id, 
	CT.name, 
	CT.[path],
	CT.[user],
	CT.password,
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [global].[path] CT		
ORDER BY hashtag