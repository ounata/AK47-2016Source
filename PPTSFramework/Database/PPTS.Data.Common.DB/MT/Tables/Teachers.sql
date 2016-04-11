
CREATE TABLE [OM].[Teachers](
    [OrgID] NVARCHAR(36) NULL, 
    [OrgName] NVARCHAR(128) NULL, 
	[TeacherID] [nvarchar](36) NOT NULL,
    [TeacherType] NVARCHAR(32) NULL, 
	[TeacherCode] [nvarchar](64) NULL,
	[TeacherName] [nvarchar](64) NULL,
    [Gender] NVARCHAR(32) NULL, 
    [Birthday] DATETIME NULL, 
	[GradeMemo] [nvarchar](32) NOT NULL,
	[SubjectMemo] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY NONCLUSTERED 
([TeacherID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
) ON [PRIMARY]

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师编码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name='TeacherName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年级代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目代码' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Teachers', @level2type=N'COLUMN',@level2name=N'Subject'
GO

GO

GO

GO

GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师信息表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = NULL,
    @level2name = NULL
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师类型（全职，兼职）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'TeacherType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授年级（用逗号分割名称）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'GradeMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教授科目（用逗号分割名称）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'SubjectMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'出生日期',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Teachers',
    @level2type = N'COLUMN',
    @level2name = N'Birthday'