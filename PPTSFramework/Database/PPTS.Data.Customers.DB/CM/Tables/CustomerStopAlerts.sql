CREATE TABLE [CM].[CustomerStopAlerts]
(
	[AlertID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [AlertType] NVARCHAR(32) NULL,
    [AlertTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [AlertReason] NVARCHAR(32) NULL, 
    [AlertReasonName] NVARCHAR(255) NULL, 
    [OperatorID] NVARCHAR(36) NULL, 
    [OperatorName] NVARCHAR(64) NULL, 
    [OperatorJobID] NVARCHAR(36) NULL, 
    [OperatorJobName] NVARCHAR(64) NULL, 
    [CustomerID] NVARCHAR(36) NULL, 
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerStopAlerts] PRIMARY KEY NONCLUSTERED ([AlertID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预警ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'AlertID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预警类型（1-停课，2-休学）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = 'AlertType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预警原因代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'AlertReason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预警原因名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'AlertReasonName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'OperatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'OperatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'OperatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'OperatorJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预警时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'AlertTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员停课休学表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStopAlerts',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO

CREATE INDEX [IX_CustomerStopAlerts_1] ON [CM].[CustomerStopAlerts] ([CustomerID])
