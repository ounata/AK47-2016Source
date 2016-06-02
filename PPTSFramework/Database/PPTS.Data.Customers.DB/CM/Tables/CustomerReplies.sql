CREATE TABLE [CM].[CustomerReplies]
(
	[ReplyID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [BranchID] NVARCHAR(36) NULL, 
    [BranchName] NVARCHAR(64) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [CampusName] NVARCHAR(64) NULL, 
    [CustomerID] NVARCHAR(36) NULL, 
	[CustomerName] NVARCHAR(64) NULL,
	[ParentID] NVARCHAR(36) NULL, 
    [ParentPassportID] NVARCHAR(36) NULL, 
	[ParentName] NVARCHAR(64) NULL, 
	[PhoneNumber] NVARCHAR(64) NULL, 
    [ReplyTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [ReplyObject] NVARCHAR(32) NULL, 
    [ReplyContent] NVARCHAR(MAX) NULL, 
    [ReplyFrom] NVARCHAR(32) NULL, 
    [ReplierID] NVARCHAR(36) NULL, 
	[ReplierJobID] NVARCHAR(36) NULL, 
    [ReplierName] NVARCHAR(64) NULL, 
    [Poster] NVARCHAR(32) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [FromSystemID] NVARCHAR(36) NULL, 
    [Status] NVARCHAR(32) NULL DEFAULT '2', 
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerReplies] PRIMARY KEY NONCLUSTERED ([ReplyID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长手机号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'发言人（Xueda--1,Customer--2)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = 'Poster'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学大响应人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学大响应人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复来源(iOS--1,Andriod--2,PPTSWEB--3)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyFrom'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户反馈表-存放客户与学大的交互反馈信息,含周反馈',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerReplies_2] ON [CM].[CustomerReplies] ([CustomerID], [ReplyTime])

GO

CREATE INDEX [IX_CustomerReplies_3] ON [CM].[CustomerReplies] ([ReplyTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学大沟通对象代码Inquiry--咨询师反馈(1);MngTeacher--学管师反馈(2);Teacher--教师反馈(3);School--校区反馈(4);WMng--周反馈(6)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyObject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'BranchName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学大响应人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplierJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长PassportID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ParentPassportID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'映射来源系统ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'FromSystemID'
GO

CREATE INDEX [IX_CustomerReplies_4] ON [CM].[CustomerReplies] ([FromSystemID], [ReplyObject], [ReplyFrom])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'delete--0;new--1;normal--2;',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'Status'