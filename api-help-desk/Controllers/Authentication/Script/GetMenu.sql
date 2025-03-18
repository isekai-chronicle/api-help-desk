DECLARE 
@user_id UNIQUEIDENTIFIER = @user_id_parameter,
@menu UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000'

DECLARE @language_id UNIQUEIDENTIFIER = (SELECT id FROM [setting].[language] AS L WHERE UPPER(L.code) = 'ING')

SELECT 
	@menu AS id, 
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
	CAST(@menu AS UNIQUEIDENTIFIER) AS menuData_id_root,
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
	LEFT JOIN [frontend].componentObject AS CO ON CO.name = 'title' AND CO.component_id = C.id
	LEFT JOIN frontend.componentObject_language AS COL ON CO.id = COL.componentObject_id AND COL.language_id = @language_id
