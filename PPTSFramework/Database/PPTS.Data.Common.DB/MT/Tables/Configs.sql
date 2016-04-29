CREATE TABLE [MT].[Configs]
(
	[ConfigKey] NVARCHAR(64) NOT NULL , 
    [ConfigValue] NVARCHAR(MAX) NULL, 
    [Description] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_Configs] PRIMARY KEY NONCLUSTERED ([ConfigKey])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配置项',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Configs',
    @level2type = N'COLUMN',
    @level2name = N'ConfigKey'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配置值',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Configs',
    @level2type = N'COLUMN',
    @level2name = N'ConfigValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'简要说明',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Configs',
    @level2type = N'COLUMN',
    @level2name = N'Description'