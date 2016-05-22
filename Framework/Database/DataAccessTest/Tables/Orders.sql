CREATE TABLE [dbo].[Orders]
(
	[OrderID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [OrderName] NVARCHAR(64) NULL, 
    [ProductID] NVARCHAR(36) NULL, 
    [Quantity] INT NULL DEFAULT 0, 
	[Status] NVARCHAR(32) NULL,
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE()
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订单ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = N'COLUMN',
    @level2name = N'OrderID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订单名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = N'COLUMN',
    @level2name = N'OrderName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = N'COLUMN',
    @level2name = N'ProductID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订单对应的产品数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = N'COLUMN',
    @level2name = N'Quantity'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'