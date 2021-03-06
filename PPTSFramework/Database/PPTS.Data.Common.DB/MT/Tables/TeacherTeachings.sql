
CREATE TABLE [MT].[TeacherTeachings](
	[TeacherID] [nvarchar](36) NOT NULL,
	[TeachingID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[Subject] [nvarchar](32) NOT NULL,
	[Grade] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_TeacherTeachings] PRIMARY KEY ([TeachingID]) 
) ON [PRIMARY]

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年级代码',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherTeachings',
    @level2type = N'COLUMN',
    @level2name = N'Grade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目代码',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherTeachings',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherTeachings',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教学教学表（老师教授-科目-年级）',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'TeacherTeachings',
    @level2type = NULL,
    @level2name = NULL