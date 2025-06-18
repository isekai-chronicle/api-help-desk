SELECT 
	CD.id AS component_id,
	CD.name AS component_name
FROM [frontend].component CD
WHERE CD.isOffline = 0
ORDER bY CD.name