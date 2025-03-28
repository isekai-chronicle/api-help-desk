DECLARE    
    @name NVARCHAR(100) = @name_parameter,    
    @word NVARCHAR(MAX) = @word_parameter,          
    @area NVARCHAR(50) = @area_parameter,          
    @task_id UNIQUEIDENTIFIER = @task_id_parameter;

/*se busca el area id previo a guardar el componente*/
DECLARE @area_id UNIQUEIDENTIFIER = (SELECT A.id FROM [security].area AS A WHERE UPPER(A.name) = UPPER(@area));
DECLARE @component_id UNIQUEIDENTIFIER = NULL;
/*el tag_id est temporal luego se acctaulizaran los tipo de tag*/
DECLARE @tag_id UNIQUEIDENTIFIER = '090C18A7-C768-49AA-8762-610138AC2341' /*TEMPORALMENTE CAMBIAR POR REAL*/
 
 /*convertir la variable @word a tabla json*/
DECLARE @objects AS TABLE(fieldName NVARCHAR(50), fieldValue NVARCHAR(50))
/*
INSERT INTO @objects
SELECT 
    [Key] AS fieldName,
    Value AS fieldValue
FROM OPENJSON(@word);
*/
INSERT INTO @objects
SELECT 
    j.[Key] AS KeyName,
    j2.[word]/*,
    j2.[disabled],
    j2.[visible]*/
FROM OPENJSON(@word) AS j
CROSS APPLY OPENJSON(j.[Value]) 
WITH (
    Word NVARCHAR(100) '$.word',
    Disable BIT '$.disabled',
    Visible BIT '$.visible'
) AS j2;

DECLARE @output_id AS TABLE (id UNIQUEIDENTIFIER);

/*si no existe el area no crear*/
IF (@area_id IS NOT NULL)
BEGIN

/*buscar el componenete en base al nombre y area*/
SET @component_id = (SELECT TOP 1 COM.id FROM frontend.component AS COM WHERE COM.name = @name AND COM.area_id = @area_id)

/*en caso de no existir crear componente*/
IF @component_id IS NULL
BEGIN
	INSERT INTO frontend.component (area_id, name, displayName, isOffline, isService, isShared, task_id)
	OUTPUT inserted.id INTO @output_id 
	VALUES(@area_id, @name, @name, 0, 1, 0, @task_id)

	SET @component_id = (SELECT TOP 1 id FROM @output_id)
END


INSERT INTO [security].menuData(menu_id, menuData_id_root, component_id, file_id_icon, name, displayName, route, task_id)
SELECT 	
	(SELECT TOP 1 M.id FROM [security].menu AS M /*WHERE M.name like 'main%'*/) AS menu_id,
	null AS menuData_id_root,
	CC.id AS component_id,
	null AS file_id_icon,
	CC.[name],
	CC.displayName,
	null AS [route],
	@task_id
FROM [frontend].component AS CC
WHERE CC.id = @component_id

/*eliminar registros que no existen en la lista*/
/*registros de eventor*/
DELETE COE
FROM frontend.componentObject AS COO 
	INNER JOIN frontend.componentObject_event AS COE ON COE.componentObject_id = COO.id
	LEFT JOIN @objects AS OBJ ON COO.name = OBJ.fieldName 
WHERE COO.component_id = @component_id AND OBJ.fieldName IS NULL
/*registros de idiomas*/
DELETE COE
FROM frontend.componentObject AS COO 
	INNER JOIN frontend.componentObject_language AS COE ON COE.componentObject_id = COO.id
	LEFT JOIN @objects AS OBJ ON COO.name = OBJ.fieldName 
WHERE COO.component_id = @component_id AND OBJ.fieldName IS NULL
/*registro de objeto*/
DELETE COO
FROM frontend.componentObject AS COO 	
	LEFT JOIN @objects AS OBJ ON COO.name = OBJ.fieldName 
WHERE COO.component_id = @component_id AND OBJ.fieldName IS NULL

/*crear primero los objetos en base a la lista*/
INSERT INTO frontend.componentObject (component_id, tag_id, name, description, task_id)
SELECT 
	@component_id, 
	@tag_id,
	OBJ.fieldName, 
	OBJ.fieldValue,
	@task_id
FROM @objects AS OBJ
	LEFT JOIN frontend.componentObject AS COO ON COO.name = OBJ.fieldName AND COO.component_id = @component_id
WHERE COO.id IS NULL
/*crear registro de traducciones por cada idioma*/
INSERT INTO frontend.componentObject_language (language_id, componentObject_id, value, task_id)
SELECT 
	LL.language_id,
	COO.id AS componentObject_id, 
	COO.description,
	@task_id
FROM @objects AS OBJ
	INNER JOIN frontend.componentObject AS COO ON COO.name = OBJ.fieldName AND COO.component_id = @component_id
	OUTER APPLY (
		SELECT 
			LL.id AS language_id
		FROM setting.[language] AS LL		
	) AS LL
	LEFT JOIN frontend.componentObject_language AS COL ON COL.componentObject_id = COO.id AND COL.language_id = LL.language_id
WHERE COL.componentObject_id IS NULL

END

