SELECT 
	CD.id AS menuData_id_root,
	CD.name AS menuData_name_root
FROM [security].menuData CD
WHERE CD.component_id IS NULL AND CD.menu_id = @menu_id_parameter
ORDER bY CD.name