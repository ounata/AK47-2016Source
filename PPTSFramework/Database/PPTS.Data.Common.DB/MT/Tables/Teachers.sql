
CREATE TABLE [MT].[Teachers](
	[TeacherID] [nvarchar](36) NOT NULL,
	[TeacherCode] [nvarchar](64) NULL,
	[TeacherName] [nvarchar](64) NULL,
    [TeacherOACode] NVARCHAR(128) NULL, 
    [Gender] NVARCHAR(32) NULL, 
    [Birthday] DATETIME NULL, 
	[GradeMemo] [nvarchar](MAX) NULL,
	[SubjectMemo] [nvarchar](MAX) NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY NONCLUSTERED 
([TeacherID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
) ON [PRIMARY]

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师ID' , @level0type=N'SCHEMA',@level0name=N'MT', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师编码' , @level0type=N'SCHEMA',@level0name=N'MT', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师姓名' , @level0type=N'SCHEMA',@level0name=N'MT', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherName'
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授年级（用逗号分割名称）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'GradeMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授科目（用逗号分割名称）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'SubjectMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'出生日期',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'Birthday'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师OA编码',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'TeacherOACode'