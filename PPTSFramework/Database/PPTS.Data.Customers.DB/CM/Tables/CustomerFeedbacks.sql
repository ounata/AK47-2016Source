
CREATE TABLE [CM].[CustomerFeedbacks](
	[CampusID] [nvarchar](36) NOT NULL,
    [CampusName] NVARCHAR(128) NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,
	[FeedbackID] [nvarchar](36) NOT NULL DEFAULT newid(), 
    [FeedbackTime] DATETIME NOT NULL, 
    [FeedbackContent] NVARCHAR(MAX) NULL, 
    [ParentID] NVARCHAR(36) NULL, 
    [ParentName] NVARCHAR(64) NULL, 
    [PhoneNumber] NVARCHAR(64) NULL, 
    [Grade] NVARCHAR(32) NULL, 
    [ConsultantID] NVARCHAR(36) NULL, 
    [ConsultantJobID] NVARCHAR(36) NULL, 
    [ConsultantName] NVARCHAR(64) NULL, 
    [EducatorID] NVARCHAR(36) NULL, 
    [EducatorJobID] NVARCHAR(36) NULL, 
    [EducatorName] NCHAR(10) NULL, 
    [CreatorID] NVARCHAR(36) NULL , 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT getdate(), 
    CONSTRAINT [PK_CustomerFeedbacks] PRIMARY KEY NONCLUSTERED ([FeedbackID])
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家长反馈表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFeedbacks'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'反馈ID（家长反馈）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'FeedbackID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'反馈日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'FeedbackTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'反馈内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = 'FeedbackContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前年级编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = 'Grade'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'ParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长电话',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = 'ConsultantName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'EducatorName'
GO

CREATE INDEX [IX_CustomerFeedbacks_2] ON [CM].[CustomerFeedbacks] ([CustomerID], [FeedbackTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFeedbacks',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO

CREATE INDEX [IX_CustomerFeedbacks_3] ON [CM].[CustomerFeedbacks] ([FeedbackTime])
