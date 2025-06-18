SELECT 
	DISTINCT
	CT.id, 
	CT.name, 	
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [security].[domain] CT		
ORDER BY hashtag