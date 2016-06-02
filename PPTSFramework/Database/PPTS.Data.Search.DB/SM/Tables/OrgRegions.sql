CREATE TABLE [SM].[OrgRegions]
(
	[RegionID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [RegionName] NVARCHAR(128) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgRegions',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgRegions',
    @level2type = N'COLUMN',
    @level2name = N'RegionName'