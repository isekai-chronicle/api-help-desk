DECLARE @user_id UNIQUEIDENTIFIER = @user_id_parameter
DECLARE @project_id UNIQUEIDENTIFIER = @project_id_parameter

;WITH TASK AS (
	SELECT 
		TSK.id AS task_id,
		TSK.name AS task_name,
		LST.id AS list_id,
		LST.name AS list_name,
		PFT_status.value AS status_name,
		PFT_priority.value AS priority_name,
		PFT_step.value AS step_name,		
		FORMAT(CAST(PFT_createdDate.value AS DATETIME), FTP_createdDate.[format]) AS createdDate,
		FORMAT(CAST(PFT_terminateDate.value AS DATETIME), FTP_terminateDate.[format]) AS terminateDate,
		FORMAT(CAST(PFT_closeDate.value AS DATETIME), FTP_closeDate.[format]) AS closeDate,
		ISNULL(CCC.counterTask, 0) AS counterTask,
		USS.account AS [user_name]
	FROM support.task AS TSK
		OUTER APPLY (
			SELECT 
				CAST(SUM(DATEDIFF(MINUTE, TRR.startDate, ISNULL(TRR.endDate, CURRENT_TIMESTAMP)))  AS INT) AS counterTask
			FROM support.taskReview AS TRR 
			WHERE TRR.task_id = TSK.id
		) AS CCC
		INNER JOIN support.list AS LST ON LST.id = TSK.list_id
		INNER JOIN support.project_field_task AS PFT_user ON TSK.id = PFT_user.task_id AND PFT_user.field_id = 'A8A8B25F-671A-4FEE-89A3-432EBFFA2AB9' 	
		LEFT JOIN [security].[user] AS USS ON CAST(USS.id AS NVARCHAR(50)) = PFT_user.[value]

		INNER JOIN support.project_field_task AS PFT_status ON TSK.id = PFT_status.task_id AND PFT_status.field_id = 'D0D25E98-B66A-4EAC-B570-223C5402C3A8'
		INNER JOIN support.project_field_task AS PFT_priority ON TSK.id = PFT_priority.task_id AND PFT_priority.field_id = 'EC81820F-B57F-408B-B935-342220B464CC' 
		INNER JOIN support.project_field_task AS PFT_step ON TSK.id = PFT_step.task_id AND PFT_step.field_id = '0CCB4786-3BD4-4287-A993-76529931F402' 
	
		INNER JOIN support.project_field_task AS PFT_createdDate ON TSK.id = PFT_createdDate.task_id AND PFT_createdDate.field_id = '1B8BAE6D-6660-495D-A11B-A133DF63581B' 
		LEFT JOIN support.field AS FLD_createdDate ON PFT_createdDate.field_id = FLD_createdDate.id
		LEFT JOIN support.fieldType AS FTP_createdDate ON FTP_createdDate.id = FLD_createdDate.fieldType_id

		INNER JOIN support.project_field_task AS PFT_terminateDate ON TSK.id = PFT_terminateDate.task_id AND PFT_terminateDate.field_id = 'A880B93F-803C-4576-BB0A-EEF5E8832B3F' 
		LEFT JOIN support.field AS FLD_terminateDate ON PFT_terminateDate.field_id = FLD_terminateDate.id
		LEFT JOIN support.fieldType AS FTP_terminateDate ON FTP_terminateDate.id = FLD_terminateDate.fieldType_id

		INNER JOIN support.project_field_task AS PFT_closeDate ON TSK.id = PFT_closeDate.task_id AND PFT_closeDate.field_id = '5DA86CC7-3636-4DEC-A330-CEF79015A77E' 
		LEFT JOIN support.field AS FLD_closeDate ON PFT_closeDate.field_id = FLD_closeDate.id
		LEFT JOIN support.fieldType AS FTP_closeDate ON FTP_closeDate.id = FLD_closeDate.fieldType_id
	WHERE TSK.isActive = 1 /*AND (PFT_user.[value] = @user_id OR PFT_user.[value] = '')*/
		AND LST.project_id = @project_id
		/*AND TSK.isCompleted = 0*/
),
STEP AS (
	SELECT 
		FPP.sort,
		FPP.id AS step_id,
		FPP.name AS step_group,
		TSS.*
	FROM support.fieldParameter AS FPP
		LEFT JOIN TASK AS TSS ON FPP.name = TSS.step_name
	WHERE FPP.field_id = '0CCB4786-3BD4-4287-A993-76529931F402'
)

SELECT *
FROM STEP
ORDER BY sort