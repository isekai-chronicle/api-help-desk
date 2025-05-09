DECLARE @menu_id UNIQUEIDENTIFIER = (SELECT TOP 1 D.menu_id FROM [security].menuData AS D WHERE D.id = @menuData_id_parameter)

SELECT 	
	MM.id AS menu_id,
	MM.[name] AS menu_name,
	RT.id AS menuData_id_component, 
	RT.displayName AS menuData_name_component, 			
	ROW_NUMBER() OVER(ORDER BY RT.name) AS hashtag	
FROM [security].menu As MM
	INNER JOIN [security].menuData AS RT ON RT.menu_id = MM.id	
WHERE RT.component_id IS NOT NULL 
	AND RT.menu_id = @menu_id
	AND RT.menuData_id_root = @menuData_id_parameter
ORDER BY hashtag

SELECT 	
	MM.id AS menu_id,
	MM.[name] AS menu_name,
	RT.id AS menuData_id_component, 
	RT.displayName AS menuData_name_component, 			
	ROW_NUMBER() OVER(ORDER BY RT.name) AS hashtag	
FROM [security].menu As MM
	INNER JOIN [security].menuData AS RT ON RT.menu_id = MM.id	
WHERE RT.component_id IS NOT NULL 
	AND RT.menu_id = @menu_id
	AND RT.menuData_id_root IS NULL
ORDER BY hashtag
