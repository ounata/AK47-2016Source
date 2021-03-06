
CREATE TABLE [OM].[Assigns](
	[AssignID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[AssignTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[AssignStatus] [nvarchar](32) NOT NULL,
	[CampusID] [nvarchar](36) NULL,
	[CampusName] [nvarchar](128) NULL,
	[AssignPrice] [decimal](18, 4) NOT NULL DEFAULT 0,
	[AssignSource] [nvarchar](32) NOT NULL,
	[AssignMemo] [nvarchar](255) NULL,
    [CopyAllowed] INT NOT NULL DEFAULT 1, 
    [ConfirmID] NVARCHAR(36) NULL, 
	[ConfirmTime] [datetime] NULL,
	[ConfirmStatus] [nvarchar](32) NOT NULL,
    [ConfirmPrice] DECIMAL(18, 4) NULL, 
	[AssetID] [nvarchar](36) NOT NULL,
    [AssetCode] NVARCHAR(64) NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,
	[AccountID] [nvarchar](36) NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NULL,
	[ProductName] [nvarchar](128) NULL,
    [CategoryType] NVARCHAR(32) NULL, 
    [CategoryTypeName] NVARCHAR(64) NULL, 
	[RoomID] [nvarchar](36) NULL,
	[RoomCode] [nvarchar](64) NULL,
	[RoomName] [nvarchar](128) NULL,
	[TeacherID] [nvarchar](36) NULL,
	[TeacherName] [nvarchar](64) NULL,
    [TeacherJobID] NVARCHAR(36) NULL, 
    [TeacherJobOrgID] NVARCHAR(36) NULL, 
    [TeacherJobOrgName] NVARCHAR(128) NULL, 
    [IsFullTimeTeacher] INT NULL, 
    [ConsultantID] NVARCHAR(36) NULL, 
    [ConsultantName] NVARCHAR(64) NULL, 
    [ConsultantJobID] NVARCHAR(36) NULL, 
    [EducatorID] NVARCHAR(36) NULL, 
    [EducatorName] NVARCHAR(64) NULL, 
    [EducatorJobID] NVARCHAR(36) NULL, 
	[Grade] [nvarchar](32) NULL,
	[GradeName] [nvarchar](64) NULL,
	[Subject] [nvarchar](32) NULL,
	[SubjectName] [nvarchar](64) NULL,
	[DurationValue] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Amount] [decimal](18, 2) NOT NULL DEFAULT 0,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Assigns] PRIMARY KEY CLUSTERED 
(
	[AssignID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排课ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'AssignID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排课状态（1-排定，3-已上，8-异常，10-无效）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'AssignStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'异常原因' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'AssignMemo'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'AssetID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教室ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'RoomID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'TeacherID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'TeacherName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年级代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟）【课次时长】' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name='DurationValue'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排定时候价格' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name='AssignPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'EndTime'
GO

GO

GO

GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assigns', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ProductCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ProductName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教室编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'RoomCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教室名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'RoomName'
GO

GO

GO

GO

GO

CREATE INDEX [IX_Assigns_1] ON [OM].[Assigns] ([CustomerID], [AssignTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'AssignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排课来源（0-自动【班组】，1-手工【一对一】，2-补录）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'AssignSource'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认状态（0-未确认，1-已确认，3-已删除，4-部分确认-针对班组班级课程确认状态时有效）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'GradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排课表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_Assigns_2] ON [OM].[Assigns] ([AssignTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否允许复制',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CopyAllowed'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数量（一对一是实际时间除以时长向下取0.5，班组是1）与资产保持一致',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'EducatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认时价格',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmPrice'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位所属学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位所属学科组名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否全职教师',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = 'IsFullTimeTeacher'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型（也叫课时类型）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assigns',
    @level2type = N'COLUMN',
    @level2name = N'CategoryTypeName'