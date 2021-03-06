

CREATE TABLE [CM].[CustomerScores](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[ScoreID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ScoreType] [nvarchar](32) NULL,
    [ScoreGrade] NVARCHAR(32) NULL, 
	[StudyYear] [nvarchar](32) NULL,
	[StudyTerm] [nvarchar](32) NULL,
	[StudyStage] [nvarchar](32) NULL,
    [ClassPeoples] INT NULL DEFAULT 0, 
	[Satisficing] [nvarchar](32) NULL,
	[StudentType] [nvarchar](32) NULL,
	[AdmissionType] [nvarchar](32) NULL,
	[AdmissionSchool] [nvarchar](64) NULL,
    [OtherScoreTypeName] NVARCHAR(64) NULL, 
	[IsKeyCollege] INT NULL DEFAULT 0 ,
	[IsAllAdded] INT NULL DEFAULT 0,
	[ExamineMonth] [int] NULL DEFAULT 1,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerScores] PRIMARY KEY NONCLUSTERED 
(
	[ScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成绩ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ScoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成绩类型（考试类型，部分枚举）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ScoreType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学年（例如：2015-2016）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='StudyYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学期' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='StudyTerm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学段' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='StudyStage'
GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家长满意度' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='Satisficing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生类型代码（高考用）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='StudentType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录取类型代码（升学考试）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='AdmissionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录取学校（升学考试）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name='AdmissionSchool'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否985/211院校（高考用）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'IsKeyCollege'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'月份（月考用）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ExamineMonth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员成绩表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScores'
GO

CREATE INDEX [IX_CustomerScores_2] ON [CM].[CustomerScores] ([CustomerID], [StudyYear], [StudyTerm], [StudyStage])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScores',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班级人数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScores',
    @level2type = N'COLUMN',
    @level2name = N'ClassPeoples'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'考试年级',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScores',
    @level2type = N'COLUMN',
    @level2name = N'ScoreGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'其它考试类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScores',
    @level2type = N'COLUMN',
    @level2name = N'OtherScoreTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目成绩是否已全部添加',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScores',
    @level2type = N'COLUMN',
    @level2name = N'IsAllAdded'