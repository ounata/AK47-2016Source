
CREATE TABLE [OM].[ClassLessons](
	[ClassID] [nvarchar](36) NOT NULL,
	[SortNo] [int] NOT NULL DEFAULT 1,
	[LessonID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[LessonCode] [nvarchar](64) NOT NULL,
	[LessonStatus] [nvarchar](32) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[ConfirmStatus] [nvarchar](32) NOT NULL,
	[ConfirmTime] [datetime] NULL,
	[ConfirmedPeoples] [int] NULL DEFAULT 0,
    [ConfirmedMoney] DECIMAL(18, 4) NULL DEFAULT 0, 
	[LessonPeoples] [int] NULL DEFAULT 0,
	[RoomID] [nvarchar](36) NULL,
	[RoomCode] [nvarchar](64) NULL,
	[RoomName] [nvarchar](128) NULL,
	[TeacherID] [nvarchar](36) NULL,
	[TeacherName] [nvarchar](64) NULL,
    [TeacherJobID] NVARCHAR(36) NULL, 
    [TeacherJobOrgID] NVARCHAR(36) NULL, 
    [TeacherJobOrgName] NVARCHAR(128) NULL, 
    [IsFullTimeTeacher] INT NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_ClassLessons] PRIMARY KEY NONCLUSTERED 
(
	[LessonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ClassID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'LessonID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次状态（1-排定，3-已上，9-已删除）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'LessonStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认状态（参考排课确认状态）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ConfirmStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后确认时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ConfirmTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已确认的人数（已上课）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name='ConfirmedPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次课包含人数' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name='LessonPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次课教室ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'RoomID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次课教师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'TeacherID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次课教师名称' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'TeacherName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ClassLessons', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次课教室编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'RoomCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次课教室名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'RoomName'
GO

CREATE INDEX [IX_ClassLessons_1] ON [OM].[ClassLessons] ([ClassID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组班级课次表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次编号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'LessonCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已确认的课时费（已上课）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次教师岗位所属学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次教师岗位所属学科组名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本次是否全职教师',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ClassLessons',
    @level2type = N'COLUMN',
    @level2name = N'IsFullTimeTeacher'