

CREATE TABLE [CM].[CustomerScoreItems](
	[ScoreID] [nvarchar](36) NOT NULL,
	[ItemID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[SortNo] [int] NOT NULL DEFAULT 99,
	[Subject] [nvarchar](32) NULL,
	[TeacherID] [nvarchar](36) NULL,
	[TeacherName] [nvarchar](64) NULL,
    [TeacherOrgID] NVARCHAR(36) NULL, 
    [TeacherOrgName] NVARCHAR(128) NULL, 
	[ScoreChangeType] [nvarchar](32) NULL DEFAULT 0,
	[PaperScore] [decimal](18, 2) NULL,
	[RealScore] [decimal](18, 2) NULL,
	[GradeRank] [int] NULL,
	[ClassRank] [int] NULL,
	[Satisficing] [nvarchar](32) NULL,
	[IsStudyHere] [bit] NOT NULL,
 [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerScoreItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
) ON [PRIMARY]

GO
ALTER TABLE [CM].[CustomerScoreItems] ADD  CONSTRAINT [DF_CustomerScoringItems_IsStudyInXueda]  DEFAULT 1 FOR [IsStudyHere]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成绩ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'ScoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成绩明细ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示顺序' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name='Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'辅导老师ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'TeacherID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'辅导老师姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'TeacherName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卷面分' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'PaperScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'得分' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'RealScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年纪名词' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'GradeRank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级名次' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'ClassRank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家长满意度' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name='Satisficing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否在该学校辅导' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems', @level2type=N'COLUMN',@level2name=N'IsStudyHere'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员成绩明细表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerScoreItems'
GO


CREATE INDEX [IX_CustomerScoreItems_1] ON [CM].[CustomerScoreItems] ([ScoreID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScoreItems',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成绩升降（持平、上升、下降）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScoreItems',
    @level2type = N'COLUMN',
    @level2name = N'ScoreChangeType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScoreItems',
    @level2type = N'COLUMN',
    @level2name = N'TeacherOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学科组名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerScoreItems',
    @level2type = N'COLUMN',
    @level2name = N'TeacherOrgName'