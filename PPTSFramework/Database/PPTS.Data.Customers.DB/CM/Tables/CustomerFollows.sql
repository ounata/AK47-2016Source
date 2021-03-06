

CREATE TABLE [CM].[CustomerFollows](
	[OrgID] [nvarchar](36) NOT NULL,
    [OrgName] NVARCHAR(128) NULL, 
	[OrgType] [nvarchar](32) NOT NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[FollowID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[FollowTime] [datetime] NOT NULL,
	[FollowerID] [nvarchar](36) NULL,
	[FollowerName] [nvarchar](64) NULL,
	[FollowerJobID] [nvarchar](36) NULL,
	[FollowerJobName] [nvarchar](64) NULL,
	[FollowType] [nvarchar](32) NULL,
    [FollowStage] NVARCHAR(32) NULL, 
	[FollowObject] [nvarchar](32) NULL,
    [FollowPhone] NVARCHAR(64) NULL, 
    [FollowMemo] NVARCHAR(MAX) NULL, 
	[TalkMainResult] [nvarchar](32) NULL,
	[TalkSubResult] [nvarchar](32) NULL,
	[CustomerLevel] [nvarchar](32) NULL,
    [InvalidReason] NVARCHAR(32) NULL, 
	[PurchaseIntention] [nvarchar](32) NULL,
	[IntensionSubjects] [nvarchar](MAX) NULL,
	[IsValidFiling] INT NOT NULL DEFAULT 0,
	[NextFollowTime] [datetime] NULL,
	[PlanVerifyTime] [datetime] NULL,
	[PlanSignDate] [datetime] NULL,
    [IsStudyThere] INT NOT NULL DEFAULT 0, 
	[IsPotential] INT NOT NULL DEFAULT 0,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerFollows] PRIMARY KEY NONCLUSTERED 
(
	[FollowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='OrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'FollowID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否潜客阶段' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='IsPotential'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowerJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowerJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进方式代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟进对象代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='FollowObject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'沟通一级结果代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='TalkMainResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'沟通耳机结果代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='TalkSubResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买意愿代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='PurchaseIntention'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'想补习的科目' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'IntensionSubjects'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户级别代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='CustomerLevel'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有效建档' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'IsValidFiling'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计下次跟进（沟通）时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='NextFollowTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计上门确认时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name='PlanVerifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计签约日期' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'PlanSignDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerFollows', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

CREATE INDEX [IX_CustomerFollows_2] ON [CM].[CustomerFollows] ([CustomerID], [FollowTime])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进信息表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = 'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进阶段代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = 'FollowStage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'此次通电号码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = 'FollowPhone'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否其它机构辅导',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = N'IsStudyThere'
GO

CREATE INDEX [IX_CustomerFollows_3] ON [CM].[CustomerFollows] ([FollowTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无效客户理由代码，当客户级别是D时候填写（A-空手机号，B，C，D）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = N'InvalidReason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进备注',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollows',
    @level2type = N'COLUMN',
    @level2name = N'FollowMemo'