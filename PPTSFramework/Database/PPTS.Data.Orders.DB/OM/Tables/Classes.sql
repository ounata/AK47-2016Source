
CREATE TABLE [OM].[Classes](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NULL,
	[ProductName] [nvarchar](128) NULL,
	[ClassID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ClassCode] [nvarchar](64) NULL,
    [ClassName] NVARCHAR(255) NULL, 
    [ClassStatus] NVARCHAR(32) NULL, 
	[RoomID] [nvarchar](36) NULL,
	[RoomCode] [nvarchar](64) NULL,
	[RoomName] [nvarchar](128) NULL,
	[Grade] [nvarchar](32) NULL,
	[GradeName] [nvarchar](64) NULL,
	[Subject] [nvarchar](32) NULL,
	[SubjectName] [nvarchar](64) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[LessonCount] [int] NOT NULL DEFAULT 0,
    [InvalidLessons] INT NOT NULL DEFAULT 0, 
	[FinishedLessons] [int] NOT NULL DEFAULT 0,
    [LessonDurationValue] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
	[PeriodDurationValue] [decimal](18, 2) NULL DEFAULT 0,
	[PeriodsOfLesson] [decimal](18, 2) NULL DEFAULT 0,
	[ClassPeoples] [int] NOT NULL DEFAULT 0,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY NONCLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'ClassID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教室ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'RoomID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年级代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次总数' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'LessonCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已上课次（过了上课时间）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name='FinishedLessons'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级人数' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name='ClassPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Classes', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'ProductCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'ProductName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教室名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'RoomName'
GO

CREATE INDEX [IX_Classes_1] ON [OM].[Classes] ([CampusID], [ProductID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'RoomCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'GradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组班级',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班级编号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'ClassCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班级状态（0-新建，1-上部分，2-已上完，9-已删除）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'ClassStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = 'LessonDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'ClassName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时时长',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'PeriodDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'每课次课时数',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = N'PeriodsOfLesson'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无效课次（过了结账日）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Classes',
    @level2type = N'COLUMN',
    @level2name = 'InvalidLessons'