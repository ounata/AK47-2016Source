
CREATE TABLE [CM].[Customers](
	[CampusID] [nvarchar](36) NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NOT NULL,
	[CustomerName] [nvarchar](128) NULL,
    [CustomerLevel] NVARCHAR(32) NULL, 
	[CustomerStatus] [nvarchar](32) NULL DEFAULT '10',
    [StudentStatus] NVARCHAR(32) NULL DEFAULT '11', 
	[Locked] [int] NULL DEFAULT 0,
	[LockMemo] NVARCHAR(MAX) NULL,
    [Graduated] INT NOT NULL DEFAULT 0, 
    [GraduateYear] NVARCHAR(32) NULL , 
    [IsOneParent] INT NULL DEFAULT 1, 
	[Character] [nvarchar](MAX) NULL,
	[Birthday] [datetime] NULL,
	[Gender] [nvarchar](32) NULL,
    [Email] NVARCHAR(255) NULL, 
	[IDType] [nvarchar](32) NULL,
	[IDNumber] [nvarchar](64) NULL,
	[VipType] [nvarchar](32) NULL,
	[VipLevel] [nvarchar](32) NULL,
	[EntranceGrade] [nvarchar](32) NULL,
	[Grade] [nvarchar](32) NULL,
	[SchoolYear] [nvarchar](32) NULL,
	[SubjectType] [nvarchar](32) NULL,
	[StudentType] [nvarchar](32) NULL,
	[ContactType] [nvarchar](32) NULL,
	[SourceMainType] [nvarchar](32) NULL,
	[SourceSubType] [nvarchar](32) NULL,
	[SourceSystem] [nvarchar](32) NULL,
	[ReferralStaffID] [nvarchar](36) NULL,
	[ReferralStaffName] [nvarchar](64) NULL,
	[ReferralStaffJobID] [nvarchar](36) NULL,
	[ReferralStaffJobName] [nvarchar](64) NULL,
    [ReferralStaffOACode] NVARCHAR(128) NULL, 
    [ReferralCustomerID] NVARCHAR(36) NULL, 
    [ReferralCustomerCode] NVARCHAR(32) NULL, 
    [ReferralCustomerName] NVARCHAR(64) NULL, 
    [ReferralCount] INT NULL DEFAULT 0, 
    [PurchaseIntention] NVARCHAR(32) NULL , 
	[SchoolID] [nvarchar](32) NULL,
    [IsStudyAgain] INT NULL DEFAULT 0, 
    [FirstSignTime] DATETIME NULL, 
    [FirstSignerID] NVARCHAR(36) NULL, 
    [FirstSignerName] NVARCHAR(64) NULL, 
    [FollowTime] DATETIME NULL , 
    [FollowStage] NVARCHAR(32) NULL, 
    [FollowedCount] INT NULL DEFAULT 0, 
    [NextFollowTime] DATETIME NULL , 
    [VisitTime] DATETIME NULL , 
    [VisitedCount] INT NULL DEFAULT 0, 
    [NextVisitTime] DATETIME NULL , 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
    [CreatorJobType] NVARCHAR(32) NULL, 
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
    [VersionStartTime] DATETIME NOT NULL, 
    [VersionEndTime] DATETIME NULL, 
    CONSTRAINT [PK_Customers] PRIMARY KEY NONCLUSTERED 
([CustomerID], [VersionStartTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
) ON [PRIMARY]

GO

/****** Object:  Index [IX_Customers_1]    Script Date: 2016/3/23 15:16:58 ******/
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员编码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CustomerCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员描述' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Character'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生年月' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件类型' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'IDType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件号码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'IDNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入学年级代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='EntranceGrade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前年级代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学年制代码（五年制，六年制等）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='SchoolYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学科类型代码（文科，理科，不分科）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='SubjectType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员类型代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='StudentType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接触方式代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='ContactType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息来源一级分类代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='SourceMainType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息来源二级分类代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='SourceSubType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源系统代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='SourceSystem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转介绍员工ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='ReferralStaffID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转介绍员工姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='ReferralStaffName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转介绍员工岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='ReferralStaffJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转介绍员工岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name='ReferralStaffJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在读学校ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'SchoolID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否锁定' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Locked'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定描述（高三毕业冻结）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'LockMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员信息表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Customers'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工OA账号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学生ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学生编号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学生姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'VIP类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'VipType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'VIP级别',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'VipLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否仅登记一个家长',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'IsOneParent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户等级（A1,A2,A3...)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约人',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前跟进阶段',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'FollowStage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已跟进次数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'FollowedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已回访次数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'VisitedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'FollowTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下一次跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'NextFollowTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前回访时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'VisitTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下次回访时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = 'NextVisitTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否复读',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'IsStudyAgain'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电子邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否高三毕业',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'Graduated'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'高三毕业年份',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'GraduateYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'购买意向',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'PurchaseIntention'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员状态（枚举-  正常-11，冻结-14（高三毕业9月后），解冻中-141）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'StudentStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'潜客状态（默认10）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员数量',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'ReferralCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'建档人岗位',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Customers',
    @level2type = N'COLUMN',
    @level2name = N'CreatorJobType'