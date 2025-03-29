DECLARE 
@user_id UNIQUEIDENTIFIER = null,
@menu_id UNIQUEIDENTIFIER = '94b6dfd6-4415-4df8-8c7d-1562beab6cb4'

DECLARE @language_id UNIQUEIDENTIFIER = (SELECT id FROM [setting].[language] AS L WHERE UPPER(L.code) = 'ING')

SELECT 
	@menu_id AS id, 
	CAST(NULL AS UNIQUEIDENTIFIER) AS menuData_id_root, 
	CAST(NULL AS UNIQUEIDENTIFIER) AS component_id, 
	'undefined' AS menu_name,
	'production' AS icon,
	'production' AS [image],
	'undefined' AS menu_displayName,
	'/under-construction' AS [path],
	0 AS isDisabled,
	1 AS isActive,
	2 AS sort
UNION ALL
SELECT      	
	NEWID() AS id,
	CAST(@menu_id AS UNIQUEIDENTIFIER) AS menuData_id_root,
	C.id AS component_id,
	C.[name] AS [menu_name],
	'production'  AS icon,
	'production' AS [image],
	ISNULL(COL.[value], C.displayName) AS menu_displayName,
	ISNULL(CONCAT(A.route, '/', C.[name]),'/under-construction')  AS [path],
	CAST(ISNULL(0,0) AS BIT) AS isDisabled,
	CAST(1 AS BIT) AS isActive,
	2 AS sort	
FROM [frontend].component AS C
	INNER JOIN [security].area AS A ON C.area_id = A.id
	INNER JOIN [security].menuData AS MD ON MD.component_id = C.id
	LEFT JOIN [frontend].componentObject AS CO ON CO.name = 'title' AND CO.component_id = C.id
	LEFT JOIN frontend.componentObject_language AS COL ON CO.id = COL.componentObject_id AND COL.language_id = @language_id
WHERE MD.menuData_id_root Is NULL
UNION ALL
SELECT 
	MD.id,
	MD.menuData_id_root,
	MD.component_id,
	MD.name AS menu_name,
	'production' AS icon,
	'production' AS [image],
	ISNULL(COL.[value], MD.displayName) AS menu_displayName,
	ISNULL(CONCAT(A.route, '/', CC.[name]),'/under-construction')  AS [path],
	CAST(ISNULL(0,0) AS BIT) AS isDisabled,
	CAST(1 AS BIT) AS isActive,
	DENSE_RANK() OVER(ORDER BY MD.menuData_id_root, MD.name) AS sort	
FROM [security].menu AS MM 
	INNER JOIN [security].menuData AS MD ON MD.menu_id = MM.id
	LEFT JOIN [frontend].component AS CC ON CC.id = MD.component_id
	LEFT JOIN [security].area AS A ON CC.area_id = A.id	
	LEFT JOIN [frontend].componentObject AS CO ON CO.name = 'title' AND CO.component_id = CC.id
	LEFT JOIN frontend.componentObject_language AS COL ON CO.id = COL.componentObject_id AND COL.language_id = @language_id
WHERE MM.id = @menu_id AND ((MD.component_id IS NOT NULL AND MD.menuData_id_root IS NOT NULL) OR (MD.component_id Is nULL))
