CREATE TABLE [OM].[AccompanyAssigns]
(
    [CampusID] NVARCHAR(36) NOT NULL, 
    [CampusName] NVARCHAR(128) NULL, 
	[AssignID] NVARCHAR(36) NOT NULL, 
    [AssignTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [AssignStatus] NVARCHAR(32) NULL, 
    [Amount] DECIMAL(18, 2) NULL, 
    [TeacherID] NVARCHAR(36) NULL, 
    [TeacherName] NVARCHAR(64) NULL, 
    [TeacherJobID] NVARCHAR(36) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [PK_AccompanyAssigns] PRIMARY KEY NONCLUSTERED ([AssignID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定 ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'AssignID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'AssignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定状态',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'AssignStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数量',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师陪读记录',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AccompanyAssigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'