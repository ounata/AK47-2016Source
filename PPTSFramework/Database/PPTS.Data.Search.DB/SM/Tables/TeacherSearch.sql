CREATE TABLE [SM].[TeacherSearch]
(
    [CustomerID] NVARCHAR(36) NOT NULL,
	[TeacherID] NVARCHAR(36) NOT NULL, 
    [TeacherName] NVARCHAR(64) NULL, 
    [TeacherJobID] NVARCHAR(36) NOT NULL, 
    [TeacherJobOrgID] NVARCHAR(36) NOT NULL, 
    [TeacherJobOrgName] NVARCHAR(64) NULL, 
    [Subject] NVARCHAR(36) NOT NULL, 
    [SubjectName] NVARCHAR(64) NULL, 
    CONSTRAINT [PK_TeacherSearch] PRIMARY KEY NONCLUSTERED ([CustomerID], [TeacherJobID], [TeacherJobOrgID], [Subject])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'TeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位所属学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = 'TeacherJobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位所属学科组名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = 'TeacherJobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授科目代码',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeacherSearch',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'