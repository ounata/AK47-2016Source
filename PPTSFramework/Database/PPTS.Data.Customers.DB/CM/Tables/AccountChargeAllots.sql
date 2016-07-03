CREATE TABLE [CM].[AccountChargeAllots]
(
    [ApplyID] NVARCHAR(36) NULL, 
    [SortNo] INT NULL DEFAULT 0, 
	[AllotID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [TeacherID] NVARCHAR(50) NULL, 
    [TeacherName] NCHAR(10) NULL, 
    [TeacherJobID] NVARCHAR(36) NULL, 
    [TeacherJobName] NVARCHAR(64) NULL, 
    [TeacherType] NVARCHAR(32) NULL, 
    [TeacherOACode] NVARCHAR(36) NULL, 
    [Subject] NVARCHAR(36) NULL, 
    [CategoryType] NVARCHAR(32) NULL, 
    [AllotAmount] DECIMAL(18, 2) NULL DEFAULT 0, 
    [AllotMoney] DECIMAL(18, 4) NULL DEFAULT 0, 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountChargeAllots] PRIMARY KEY NONCLUSTERED ([AllotID]) 
)

GO

CREATE INDEX [IX_AccountChargeAllots_1] ON [CM].[AccountChargeAllots] ([ApplyID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'申请ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'ApplyID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'责任分配ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师OA编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = 'TeacherOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师类型（全职，兼职）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型（一对一，班组...）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'金额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'AllotMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'收费责任分配表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargeAllots',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobName'