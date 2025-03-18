DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @area NVARCHAR(50) = @area_parameter,
	@language NVARCHAR(50) = @language_parameter;

DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)
DECLARE @language_id UNIQUEIDENTIFIER = (SELECT TOP 1 COM.id FROM setting.language AS COM WHERE UPPER(COM.code) = UPPER(@language))
 
 SELECT 			
	COO.id AS componentObject_id,
	COO.name AS [key], 	
	COL.value AS word,
	CAST(0 AS BIT) AS [disabled],
	CAST(1 AS BIT) AS [visible]
FROM frontend.componentObject AS COO 
	LEFT JOIN frontend.componentObject_language AS COL ON COL.componentObject_id = COO.id 
WHERE COO.component_id = @component_id AND COL.language_id = @language_id


