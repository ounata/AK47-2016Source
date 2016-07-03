CREATE TABLE [WF].[WFJobConfig]
(
	[JobConfigID] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT newid(), 
    [ProcessKey] NVARCHAR(64) NOT NULL, 
    [OrgID] NVARCHAR(36) NOT NULL, 
	[OrgName] NVARCHAR(255) NOT NULL,
    [JobName] NVARCHAR(255) NOT NULL, 
    [JobID] NVARCHAR(36) NOT NULL, 
    [StartTime] DATETIME NOT NULL DEFAULT getutcdate(), 
    [EndTime] DATETIME NOT NULL DEFAULT '99990909 00:00:00'
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'JobConfigID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'工作流Key-优先级1',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'ProcessKey'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构ID--记录了生效的机构范围--优先级2',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人岗位名称--优先级3',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'JobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'JobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生效时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'StartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'失效时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'EndTime'
GO

CREATE INDEX [IX_WFJobConfig_01] ON [WF].[WFJobConfig] ([ProcessKey], [OrgID], [JobName])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFJobConfig',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'