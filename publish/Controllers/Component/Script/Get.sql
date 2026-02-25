SELECT 	
	CT.id, 
	CT.name, 
	CT.displayName,
	CT.isOffline,
	CT.isService,
	CT.isShared,
	A.id AS area_id,
	A.name AS area_name,
	P.id AS path_id,
	P.name AS path_name,
	ROW_NUMBER() OVER(ORDER BY CT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM [frontend].component CT	
	INNER JOIN [security].area AS A ON A.id = CT.area_id
	LEFT JOIN [global].[path] AS P ON P.id = CT.path_id
ORDER BY hashtag