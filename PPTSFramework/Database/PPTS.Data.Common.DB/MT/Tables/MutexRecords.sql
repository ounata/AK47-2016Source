CREATE TABLE [MT].[MutexRecords]
(
	[MutexKey] NVARCHAR(64) NOT NULL, 
    [BizAction] INT NULL, 
    [BizActionText] NVARCHAR(50) NULL, 
    [ExpireTime] DATETIME NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [BizBillID] NVARCHAR(36) NOT NULL,
    [BizBillNo] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_MutexRecords] PRIMARY KEY NONCLUSTERED ([MutexKey], [BizBillID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务键，学员：C_学员ID，账户：A_账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = 'MutexKey'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务操作',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = 'BizAction'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务操作描述',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = N'BizActionText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'过期时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = 'ExpireTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务单ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = N'BizBillID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务单号',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = N'BizBillNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'描述',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'MutexRecords',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO

CREATE INDEX [IX_MutexRecords_1] ON [MT].[MutexRecords] ([BizBillID])
