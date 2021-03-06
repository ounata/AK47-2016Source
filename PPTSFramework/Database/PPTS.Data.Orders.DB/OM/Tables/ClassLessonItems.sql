
CREATE TABLE [OM].[ClassLessonItems](
	[LessonID] [nvarchar](36) NOT NULL,
	[SortNo] [int] NOT NULL DEFAULT 1,
	[AssignID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[AssignTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[AssignStatus] [nvarchar](32) NOT NULL,
    [AssignPrice] DECIMAL(18, 4) NULL DEFAULT 0, 
	[ConfirmTime] [datetime] NULL,
	[ConfirmStatus] [nvarchar](32) NOT NULL,
    [ConfirmPrice] DECIMAL(18, 4) NULL, 
    [AssetID] NVARCHAR(36) NULL, 
    [AssetCode] NVARCHAR(64) NULL, 
    [CustomerID] NVARCHAR(36) NULL, 
    [CustomerCode] NVARCHAR(64) NULL, 
    [CustomerName] NVARCHAR(64) NULL, 
    [CustomerGrade] NVARCHAR(36) NULL, 
    [CustomerGradeName] NVARCHAR(64) NULL, 
    [CustomerCampusID] NVARCHAR(36) NULL, 
    [CustomerCampusName] NVARCHAR(64) NULL, 
	[IsJoinClass] [int] NOT NULL DEFAULT 0,
    [IsOuterCampus] INT NOT NULL DEFAULT 0, 
    [ConsultantID] NVARCHAR(36) NULL, 
    [ConsultantName] NVARCHAR(64) NULL, 
    [ConsultantJobID] NVARCHAR(36) NULL, 
    [EducatorID] NVARCHAR(36) NULL, 
    [EducatorName] NVARCHAR(64) NULL, 
    [EducatorJobID] NVARCHAR(36) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_ClassLessonItems] PRIMARY KEY NONCLUSTERED 
([AssignID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'LessonID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排课ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'AssignID'
GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否插班' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'IsJoinClass'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessonItems', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO

CREATE INDEX [IX_ClassLessonItems] ON [OM].[ClassLessonItems] ([LessonID])

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组班级课次明细表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = NULL,
    @level2name = NULL
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'AssetID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员所在校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员所在校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否外校',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'IsOuterCampus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'AssignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定状态（参考排课表）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'AssignStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认状态（参考排课表）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'EducatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员当前年级代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员当前年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'CustomerGradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定时价格',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'AssignPrice'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认时价格',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessonItems',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmPrice'