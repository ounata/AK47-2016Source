CREATE TABLE [CM].[SpecialCampuses]
(
	[CampusID] NVARCHAR(36) NOT NULL , 
    [CampusName] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [PK_SpecialCampuses] PRIMARY KEY NONCLUSTERED ([CampusID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特殊校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'SpecialCampuses',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特殊校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'SpecialCampuses',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'