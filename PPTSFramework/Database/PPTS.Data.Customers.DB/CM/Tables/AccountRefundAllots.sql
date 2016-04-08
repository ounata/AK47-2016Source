CREATE TABLE [CM].[AccountRefundAllots]
(
    [ApplyID] NVARCHAR(36) NULL, 
	[AllotID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [TeacherID] NVARCHAR(50) NULL, 
    [TeacherCode] NVARCHAR(36) NULL, 
    [TeacherName] NCHAR(10) NULL, 
    [TeacherType] NVARCHAR(32) NULL, 
    [Subject] NVARCHAR(36) NULL, 
    [CategoryType] NVARCHAR(32) NULL, 
    [AllotAmount] DECIMAL(18, 2) NULL DEFAULT 0, 
    [AllotMoney] DECIMAL(18, 4) NULL DEFAULT 0, 
    CONSTRAINT [PK_AccountRefundAllots] PRIMARY KEY NONCLUSTERED ([AllotID]) 
)

GO

CREATE INDEX [IX_AccountRefundAllots_1] ON [CM].[AccountRefundAllots] ([ApplyID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'申请ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'ApplyID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'责任分配ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师类型（全职，兼职）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型（一对一，班组...）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'金额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费责任分配表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundAllots',
    @level2type = NULL,
    @level2name = NULL