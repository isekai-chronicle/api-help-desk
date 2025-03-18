DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @area NVARCHAR(50) = @area_parameter,
	@language_id UNIQUEIDENTIFIER = @language_id_parameter;

DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)
 
 SELECT 	
	COL.language_id,
	ROW_NUMBER() OVER(ORDER BY COO.name) AS hashtag,
	COO.id AS componentObject_id,
	COO.name, 
	COO.description,
	COL.value AS translate
FROM frontend.componentObject AS COO 
	LEFT JOIN frontend.componentObject_language AS COL ON COL.componentObject_id = COO.id 
WHERE COO.component_id = @component_id AND COL.language_id = @language_id
ORDER BY hashtag

