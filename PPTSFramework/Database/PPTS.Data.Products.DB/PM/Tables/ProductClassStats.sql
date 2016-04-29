CREATE TABLE [PM].[ProductClassStats]
(
	[CampusID] NVARCHAR(36) NOT NULL, 
    [ProductID] NVARCHAR(36) NOT NULL, 
    [ClassCount] INT NULL DEFAULT 0, 
    [ValidClasses] INT NULL DEFAULT 0, 
    CONSTRAINT [PK_ProductClassStats] PRIMARY KEY NONCLUSTERED ([CampusID], [ProductID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductClassStats',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductClassStats',
    @level2type = N'COLUMN',
    @level2name = N'ProductID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建的班级数量',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductClassStats',
    @level2type = N'COLUMN',
    @level2name = N'ClassCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'有效的班级数量，未结课',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductClassStats',
    @level2type = N'COLUMN',
    @level2name = N'ValidClasses'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品对应的班级统计表（班组产品用）',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductClassStats',
    @level2type = NULL,
    @level2name = NULL