CREATE TABLE [MT].[MutexSettings]
(
	[BizAction] INT NOT NULL, 
    [BizActionText] NVARCHAR(50) NULL, 
    [MutexAction] INT NOT NULL, 
    [MutexActionText] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_MutexSettings] PRIMARY KEY ([BizAction], [MutexAction]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务操作',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexSettings',
    @level2type = N'COLUMN',
    @level2name = 'BizAction'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'互斥业务操作',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexSettings',
    @level2type = N'COLUMN',
    @level2name = 'MutexAction'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务操作描述',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexSettings',
    @level2type = N'COLUMN',
    @level2name = N'BizActionText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'互斥业务操作描述',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexSettings',
    @level2type = N'COLUMN',
    @level2name = N'MutexActionText'