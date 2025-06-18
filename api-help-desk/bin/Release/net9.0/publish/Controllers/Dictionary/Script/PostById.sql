DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @area NVARCHAR(50) = @area_parameter,
	@language_id UNIQUEIDENTIFIER = @language_id_parameter;

DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)
 
 SELECT 	
	ROW_NUMBER() OVER(ORDER BY COO.name) AS hashtag,
	COO.id AS componentObject_id,
	COO.name, 
	COO.description,
	COL1.value AS translate1,
	COL2.value AS translate2
FROM frontend.componentObject AS COO 
	LEFT JOIN frontend.componentObject_language AS COL1 ON COL1.componentObject_id = COO.id AND COL1.language_id = 'ECB5CBEF-F82A-462F-B9FE-25D52AD83138'
	LEFT JOIN frontend.componentObject_language AS COL2 ON COL2.componentObject_id = COO.id AND COL2.language_id = '35AEEDDB-72F1-49A6-8EE7-D5E9D37DBE94'
WHERE COO.component_id = @component_id 
ORDER BY hashtag

