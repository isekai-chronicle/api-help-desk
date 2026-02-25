SELECT 
	DISTINCT
	CT.id, 
	CT.name, 
	A.id AS area_id,
	A.name AS area_name,
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [security].role CT	
	INNER JOIN [security].area AS A ON A.id = CT.area_id
ORDER BY hashtag