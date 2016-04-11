CREATE TABLE [MT].[TeacherJobs]
(
	[JobID] NVARCHAR(36) NOT NULL , 
    [TeacherID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [CampusName] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_TeacherJobs] PRIMARY KEY ([JobID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'JobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO

CREATE INDEX [IX_TeacherJobs_1] ON [MT].[TeacherJobs] ([CampusID], [TeacherID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位表',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = NULL,
    @level2name = NULL