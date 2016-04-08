
CREATE TABLE [OM].[AssetConsumes](
	[ConsumeID] [nvarchar](36) NOT NULL,
    [ConsumeType] NVARCHAR(32) NULL, 
	[ConsumeFlag] [int] NOT NULL DEFAULT 1,
	[ConsumeTime] [datetime] NOT NULL,
	[ConsumeAmount] [decimal](18, 2) NOT NULL DEFAULT 0,
	[ConsumeMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
    [OrderID] [nvarchar](36) NULL,
	[AssetID] [nvarchar](36) NULL,
	[AssignID] [nvarchar](36) NULL,
	[AccountID] [nvarchar](36) NULL,
	[CustomerID] [nvarchar](36) NULL,
	[ProductID] [nvarchar](36) NULL,
	[TeacherID] [nvarchar](36) NULL,
	[ConsultantID] [nvarchar](36) NULL,
	[ConsultantJobID] [nvarchar](36) NULL,
	[EducatorID] [nvarchar](36) NULL,
	[EducatorJobID] [nvarchar](36) NULL,
	[OpenCampusID] [nvarchar](36) NULL,
	[CustomerCampusID] [nvarchar](36) NULL,
	[CreateTime] [datetime] NULL,
	[TenantCode] [nvarchar](36) NULL, 
    CONSTRAINT [PK_AssetConsumes] PRIMARY KEY NONCLUSTERED ([ConsumeID])
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排课ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'AssignID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='AssetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'TeacherID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗标志（1-确认，-1是删除）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗时间（确认时间/删除时间）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗金额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订单ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'OrderID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'开课校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'OpenCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCampusID'
GO

CREATE INDEX [IX_AssetConsumes_1] ON [OM].[AssetConsumes] ([ConsumeTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排课消费记录表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗类型（同确认类型）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'ConsumeType'