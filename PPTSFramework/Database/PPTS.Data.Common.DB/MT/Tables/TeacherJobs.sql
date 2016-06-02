CREATE TABLE [MT].[TeacherJobs]
(
	[JobID] NVARCHAR(36) NOT NULL , 
    [JobName] NVARCHAR(64) NULL, 
    [JobOrgID] NVARCHAR(36) NULL, 
    [JobOrgName] NVARCHAR(128) NULL, 
    [JobOrgType] NVARCHAR(32) NULL, 
    [JobStatus] NVARCHAR(32) NULL, 
    [TeacherID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [CampusName] NVARCHAR(128) NULL, 
    [IsFullTime] INT NULL DEFAULT 0, 
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
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否全职',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'IsFullTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'JobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位组织机构ID（一般指学科组ID）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = 'JobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位组织机构名称（一般指学科组名称）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = 'JobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位状态（0-在职，1-全职）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'JobStatus'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位组织机构类型（一般指学科组）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherJobs',
    @level2type = N'COLUMN',
    @level2name = N'JobOrgType'
GO
