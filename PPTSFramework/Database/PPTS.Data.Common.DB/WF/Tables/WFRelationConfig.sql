CREATE TABLE [WF].[WFRelationConfig]
(
	[RelationConfigID] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT newid(), 
    [ApplicationName] NVARCHAR(64) NOT NULL, 
    [ProgramName] NVARCHAR(64) NULL, 
    [ProcessKey] NVARCHAR(64) NOT NULL, 
    [OrgID] NVARCHAR(36) NULL, 
	[OrgType] NVARCHAR(32) NULL, 
	[OrgName] NVARCHAR(255) NULL, 
    [JobName] NVARCHAR(255) NULL, 
    [StartTime] DATETIME NOT NULL DEFAULT getutcdate(), 
    [EndTime] DATETIME NOT NULL DEFAULT '99990909 00:00:00'
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'RelationConfigID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'应用名称--工作流应用分类信息--优先级1',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'模块名称--工作流模块分类信息--优先级2',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'ProgramName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'工作流Key',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = 'ProcessKey'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构ID-优先级3',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人岗位名称-优先级4',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'JobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生效时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'StartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'失效时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'EndTime'
GO

CREATE INDEX [IX_WFRelationConfig_01] ON [WF].[WFRelationConfig] ([ApplicationName], [ProgramName], [OrgID], [JobName])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构类型,NULL--总部，2--分公司，3--校区，8--大区',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'WFRelationConfig',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'