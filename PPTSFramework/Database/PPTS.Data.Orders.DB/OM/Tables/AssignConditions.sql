
CREATE TABLE [OM].[AssignConditions](
	[ConditionID] [nvarchar](36) NOT NULL DEFAULT newid(),
    [ConditionName4Customer] NVARCHAR(128) NULL, 
    [ConditionName4Teacher] NVARCHAR(128) NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,	
    [CustomerCode] NVARCHAR(64) NULL, 
    [CustomerName] NVARCHAR(64) NULL, 
    [AccountID] NVARCHAR(36) NULL, 
	[AssetID] [nvarchar](36) NOT NULL,
    [AssetCode] NVARCHAR(64) NULL, 
    [ProductID] NVARCHAR(36) NULL, 
    [ProductCode] NVARCHAR(64) NULL, 
    [ProductName] NVARCHAR(128) NULL, 
    [CategoryType] NVARCHAR(32) NULL, 
    [CategoryTypeName] NVARCHAR(64) NULL, 
	[Grade] [nvarchar](32) NULL,
	[GradeName] [nvarchar](32) NULL,
	[Subject] [nvarchar](32) NULL,
	[SubjectName] [nvarchar](32) NULL,
	[RoomID] [nvarchar](36) NULL,
	[RoomCode] [nvarchar](64) NULL,
	[RoomName] [nvarchar](128) NULL,
	[TeacherID] [nvarchar](36) NULL,
	[TeacherName] [nvarchar](64) NULL,
	[TeacherJobID] [nvarchar](36) NULL,
    [TeacherJobOrgID] NVARCHAR(36) NULL, 
    [TeacherJobOrgName] NVARCHAR(128) NULL, 
    [IsFullTimeTeacher] INT NULL, 
    [CourseLevel] NVARCHAR(32) NULL, 
    [CourseLevelName] NVARCHAR(64) NULL, 
    [LessonDuration] NVARCHAR(32) NULL, 
    [LessonDurationValue] DECIMAL(18, 2) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL DEFAULT GETUTCDATE(), 
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_AssignConditions] PRIMARY KEY NONCLUSTERED 
(
	[ConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排课条件ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'ConditionID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'AssetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年级代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教室ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'RoomID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'TeacherID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'TeacherName'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssignConditions', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教室编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'RoomCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教室名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'RoomName'
GO

CREATE INDEX [IX_AssignConditions_1] ON [OM].[AssignConditions] ([CustomerID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'GradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排课条件表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员视图排课条件名称（资产编码+科目+老师+年级）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = 'ConditionName4Customer'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ProductID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ProductCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ProductName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevelName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'LessonDuration'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长值',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'LessonDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'
GO


GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师视图排课条件名称（资产编码+科目+学员+年级）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'ConditionName4Teacher'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学科组名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否全职教师',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = 'IsFullTimeTeacher'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型（也叫课时类型）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssignConditions',
    @level2type = N'COLUMN',
    @level2name = N'CategoryTypeName'