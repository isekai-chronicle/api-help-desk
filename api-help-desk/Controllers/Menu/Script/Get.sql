DECLARE @user_id UNIQUEIDENTIFIER = @user_id_parameter
SELECT 	
	MM.id AS menu_id,
	MM.[name] AS menu_name,

	RT.id AS menuData_id, 
	CONCAT(SB.[name], ' => ', RT.[name]) AS menuData_name, 		
	RT.displayName AS menuData_displayName,
	RT.[route] AS menuData_route,

	ROW_NUMBER() OVER(ORDER BY RT.name) AS hashtag,
	CAST(1 AS BIT) AS isDelete,	

	SB.id AS menuData_id_root,
	SB.[name] AS menuData_name_root,

	CO.id AS menuData_id_component,
	CO.component_id,
	CO.[name] AS menuData_name_component,
	CAST(CASE WHEN UC.component_id Is NULL THEN 0 ELSE 1 END AS BIT) AS isCheck,

	COB.id AS componentObject_id,
	COB.[name] AS componentObject_name
FROM [security].menu As MM
	LEFT JOIN [security].menuData AS RT ON RT.menu_id = MM.id
	LEFT JOIN [security].menuData AS SB ON RT.menuData_id_root = SB.id AND RT.menu_id = MM.id
	LEFT JOIN [security].menuData AS CO ON CO.component_id IS NOT NULL AND CO.menu_id = MM.id AND CO.menuData_id_root = RT.id
	LEFT JOIN frontend.componentObject AS COB ON COB.component_id = CO.component_id AND SUBSTRING(COB.[name], 1, 2) IN ('btn')
	LEFT JOIN [security].user_component AS UC ON UC.[user_id] = @user_id AND UC.component_id = CO.component_id
WHERE RT.component_id IS NULL 
ORDER BY hashtag