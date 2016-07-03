CREATE TABLE [SM].[TaskDetails]
(
	[RegionID] INT NULL, 
    [BranchID] NCHAR(10) NULL, 
    [CampusID] NCHAR(10) NULL, 
    [StaffID] NCHAR(10) NULL, 
    [StaffName] NCHAR(10) NULL, 
    [StaffJobID] NCHAR(10) NOT NULL, 
    [StaffJobName] NCHAR(10) NULL, 
    [StaffOA] NCHAR(10) NULL, 
    [StudentID] NCHAR(10) NOT NULL, 
    [StudentName] NCHAR(10) NULL, 
    [StudentState] NCHAR(10) NULL, 
    [CurrentGrade] NCHAR(10) NULL, 
    [TaskSpecies] NCHAR(10) NOT NULL, 
    [TaskType] NCHAR(10) NOT NULL, 
    [PlanTask] NCHAR(10) NULL, 
    [CompleteTask] NCHAR(10) NULL, 
    [UploadYear] INT NOT NULL, 
    [UploadMonth] INT NOT NULL, 
    [DocumentYear] INT NULL, 
    [DocumentMonth] INT NULL, 
    [UploadDay] INT NOT NULL, 
    [DocumentDay] INT NULL, 
    [IsSuccessful] INT NULL, 
    [Reason] NVARCHAR(MAX) NULL, 
    [CreateTime] DATETIME NULL, 
    CONSTRAINT [PK_TaskDetails] PRIMARY KEY ([StaffJobID], [StudentID], [TaskSpecies], [TaskType], [UploadYear], [UploadMonth], [UploadDay])  
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工OA',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StaffOA'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StudentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StudentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月初学员状态',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'StudentState'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前年级',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'CurrentGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务种类（1 课时量 2 续费金额）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = 'TaskSpecies'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务类型（1 动态任务 2 预算任务）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'TaskType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划任务量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = 'PlanTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'完成任务量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'CompleteTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上传时年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'UploadYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上传时月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'UploadMonth'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文档中年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'DocumentYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文档中月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'DocumentMonth'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上传时天数或周数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'UploadDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文档中天数或周数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'DocumentDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否成功 1 成功 0 失败',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'IsSuccessful'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原因',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'Reason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TaskDetails',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'