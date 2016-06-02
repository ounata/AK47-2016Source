CREATE TABLE [SM].[OrgBranches]
(
    [RegionID] NVARCHAR(36) NULL, 
	[BranchID] NVARCHAR(36) NOT NULL, 
    [BranchName] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_OrgBranches] PRIMARY KEY ([BranchID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgBranches',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgBranches',
    @level2type = N'COLUMN',
    @level2name = N'BranchName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgBranches',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'