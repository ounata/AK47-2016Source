CREATE TABLE [SM].[OrgCampuses]
(
    [RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
	[CampusID] NVARCHAR(36) NOT NULL, 
    [CampusName] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_OrgCampuses] PRIMARY KEY ([CampusID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgCampuses',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgCampuses',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgCampuses',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgCampuses',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'