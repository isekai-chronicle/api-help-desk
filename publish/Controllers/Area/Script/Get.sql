SELECT 
	DISTINCT
	CT.id, 
	CT.name, 
	CT.route,		
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [security].area CT	
ORDER BY hashtag