SELECT 
	id, 
	name, 	
    code,
    isDefault,	
	ROW_NUMBER() OVER(ORDER BY name) AS hashtag,
	CAST(1 AS BIT) AS isDelete
FROM help_desk_00.setting.language
ORDER BY hashtag