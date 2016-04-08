CREATE TABLE [CM].[CustomerServices]
(
    [CampusID] NVARCHAR(36) NULL , 
    [CampusName] NVARCHAR(128) NULL , 
    [CustomerID] NVARCHAR(36) NULL, 
	[ServiceID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [ServiceType] NVARCHAR(32) NOT NULL , 
    [ServiceStatus] NVARCHAR(32) NULL , 
    [ServiceMemo] NVARCHAR(MAX) NULL, 
    [AcceptTime] DATETIME NOT NULL DEFAULT getdate(), 
    [AcceptLimit] NVARCHAR(32) NULL, 
    [AcceptLimitValue] DECIMAL(18, 4) NULL, 
    [AcceptMemo] NVARCHAR(MAX) NULL, 
    [AccepterID] NVARCHAR(36) NOT NULL, 
    [AccepterName] NVARCHAR(64) NULL, 
    [AccepterJobID] NVARCHAR(36) NOT NULL, 
    [AccepterJobName] NVARCHAR(64) NULL, 
    [AppealMemo] NVARCHAR(MAX) NULL, 
    [ConsultantID] NVARCHAR(36) NULL, 
    [ConsultantName] NVARCHAR(64) NULL, 
    [ConsultantJobID] NVARCHAR(36) NULL, 
    [ConsultantJobName] NVARCHAR(64) NULL, 
    [EducatorID] NVARCHAR(36) NULL, 
    [EducatorName] NVARCHAR(64) NULL, 
    [EducatorJobID] NVARCHAR(36) NULL, 
    [EducatorJobName] NVARCHAR(64) NULL, 
    [ConsultType] NVARCHAR(32) NULL, 
    [ConsultMemo] NVARCHAR(255) NULL, 	
    [ComplaintTimes] NVARCHAR(32) NULL, 
    [ComplaintLevel] NVARCHAR(32) NULL, 
    [ComplaintUpgrade] NVARCHAR(32) NULL, 
    [IsUpgradeHandle] INT NOT NULL DEFAULT 0, 
    [HandleJobType] NVARCHAR(32) NULL, 
    [IsSendEmail] INT NULL DEFAULT 0, 
    [HandlerEmail] NVARCHAR(255) NULL, 
    [IsSendMessage] INT NULL DEFAULT 0, 
    [HandlerPhone] NVARCHAR(255) NULL, 
    [VoiceID] NVARCHAR(36) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT getdate(), 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NOT NULL DEFAULT getdate(), 
    CONSTRAINT [PK_CustomerServices] PRIMARY KEY NONCLUSTERED ([ServiceID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务类型（投诉，退费，咨询，其它）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ServiceType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务状态',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'ServiceStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'AcceptTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'AccepterID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'AccepterName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'AccepterJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'AccepterJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下一处理岗类型（分客服专员，分客服经理，总客服经理...）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'HandleJobType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'HandlerEmail'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否发送短信',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'IsSendMessage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人手机',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'HandlerPhone'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'EducatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'受理描述',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'AcceptMemo'
GO

CREATE INDEX [IX_CustomerServices_1] ON [CM].[CustomerServices] ([AcceptTime])

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户诉求',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'AppealMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ServiceID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'语音文件ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'VoiceID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否发送邮件',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'IsSendEmail'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备注',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ServiceMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否升级处理',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'IsUpgradeHandle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'要求分客服受理并回复时间限制代码（2小时内，6小时内，12小时内，24小时内）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = 'AcceptLimit'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询类型代码（校区相关，退费相关，加盟，其它）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'如果咨询类型是其它，则录入该内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ConsultMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'投诉次数代码（一次，二次，三次，三次以上...)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ComplaintTimes'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'严重程度代码（普通，严重，紧急）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ComplaintLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'投诉升级代码（二级，三级，特级）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'ComplaintUpgrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客服信息表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分客服受理并回复时间限制值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServices',
    @level2type = N'COLUMN',
    @level2name = N'AcceptLimitValue'