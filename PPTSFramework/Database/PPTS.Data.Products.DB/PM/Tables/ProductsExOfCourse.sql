
CREATE TABLE [PM].[ProductsExOfCourse](
	[ProductID] [nvarchar](36) NOT NULL,
	[LessonCount] [int] NULL DEFAULT 0,
	[LessonDuration] [nvarchar](32) NULL DEFAULT 0,
	[LessonDurationValue] [decimal](18, 2) NULL DEFAULT 0,
	[PeriodDuration] [nvarchar](32) NULL,
	[PeriodDurationValue] [decimal](18, 2) NULL DEFAULT 0,
	[PeriodsOfLesson] [decimal](18, 2) NULL DEFAULT 0,
	[CourseLevel] [nvarchar](32) NULL,
    [CoachType] [nvarchar](32) NULL,
	[GroupType] [nvarchar](32) NULL,
	[ClassType] [nvarchar](32) NULL,
	[MinPeoples] [int] NULL DEFAULT 0,
	[MaxPeoples] [int] NULL DEFAULT 0,
	[IncomeBelonging] [nvarchar](32) NULL,
	[IsCrossCampus] [int] NULL DEFAULT 0,
	[TenantCode] [nvarchar](36) NULL, 
    CONSTRAINT [PK_ProductsExOfCourse] PRIMARY KEY NONCLUSTERED ([ProductID])
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次数量（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'LessonCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次时长代码（通用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'LessonDuration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次时长（通用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'LessonDurationValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课时时长代码 （通用）对于一对一和课次一样' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'PeriodDuration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课时时长 （通用）对于一对一和课次一样' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'PeriodDurationValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课次课时数 （通用）对于一对一值为1' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'PeriodsOfLesson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课程级别代码（普通、1A、3A）  【班组和一对一不一样，考虑拉平】（通用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name='CourseLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班组类型代码（长期，阶段性）（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'GroupType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级类型代码（大班，小班）（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'ClassType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开班人数（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name='MinPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满班人数（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name='MaxPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跨校区收入归属（C表示学员，T表示老师）（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name=N'IncomeBelonging'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否跨校区班组（班组用）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductsExOfCourse', @level2type=N'COLUMN',@level2name='IsCrossCampus'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'辅导类型代码（常规，自主招生）（班组用）',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductsExOfCourse',
    @level2type = N'COLUMN',
    @level2name = N'CoachType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品班组属性表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductsExOfCourse',
    @level2type = NULL,
    @level2name = NULL