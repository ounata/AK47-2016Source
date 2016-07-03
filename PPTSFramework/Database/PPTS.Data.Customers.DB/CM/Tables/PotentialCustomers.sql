CREATE TABLE [CM].[PotentialCustomers]
(
	[CustomerID] NVARCHAR(36) NOT NULL DEFAULT newid(),
	[OrgID] NVARCHAR(36) NULL,
    [OrgName] NVARCHAR(128) NULL, 
	[OrgType] NVARCHAR(32) NULL,
    [CampusID] NVARCHAR(36) NULL, 
    [CampusName] NVARCHAR(64) NULL, 
	[CustomerCode] NVARCHAR(64) NULL,
	[CustomerName] NVARCHAR(128) NULL,
    [CustomerLevel] NVARCHAR(32) NULL, 
    [InvalidReason] NVARCHAR(32) NULL, 
	[CustomerStatus] NVARCHAR(32) NULL DEFAULT '0',
    [IsOneParent] INT NULL DEFAULT 1, 
    [Character] NVARCHAR(MAX) NULL, 
    [Birthday] DATETIME NULL, 
	[Gender] NVARCHAR(32) NULL,
    [Email] NVARCHAR(255) NULL, 
	[IDType] NVARCHAR(32) NULL,
	[IDNumber] NVARCHAR(64) NULL,
	[VipType] NVARCHAR(32) NULL,
	[VipLevel] NVARCHAR(32) NULL,
    [EntranceGrade] NVARCHAR(32) NULL, 
    [Grade] NVARCHAR(32) NULL, 
    [SchoolYear] NVARCHAR(32) NULL, 
    [SubjectType] NVARCHAR(32) NULL, 
    [StudentType] NVARCHAR(32) NULL DEFAULT 51, 
	[ContactType] NVARCHAR(32) NULL,
	[SourceMainType] NVARCHAR(32) NULL,
	[SourceSubType] NVARCHAR(32) NULL,
	[SourceSystem] NVARCHAR(32) NULL,
	[ReferralStaffID] [nvarchar](36) NULL,
	[ReferralStaffName] [nvarchar](64) NULL,
	[ReferralStaffJobID] [nvarchar](36) NULL,
	[ReferralStaffJobName] [nvarchar](64) NULL,
    [ReferralStaffOACode] NVARCHAR(128) NULL, 
    [ReferralCustomerID] NVARCHAR(36) NULL, 
    [ReferralCustomerCode] NVARCHAR(32) NULL, 
    [ReferralCustomerName] NVARCHAR(64) NULL, 
    [PurchaseIntention] NVARCHAR(32) NULL , 
	[SchoolID] NVARCHAR(32) NULL,
    [IsStudyAgain] INT NULL DEFAULT 0, 
    [FollowTime] DATETIME NULL , 
    [FollowStage] NVARCHAR(32) NULL, 
    [FollowedCount] INT NULL DEFAULT 0, 
    [NextFollowTime] DATETIME NULL , 
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
    [CreatorJobType] NVARCHAR(32) NULL, 
	[CreateTime] DATETIME NULL DEFAULT GETUTCDATE(),
	[ModifierID] NVARCHAR(36) NULL,
	[ModifierName] NVARCHAR(64) NULL,
	[ModifyTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [TenantCode] NVARCHAR(36) NULL,
	[VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00' ,
    CONSTRAINT [PK_PotentialCustomers] PRIMARY KEY NONCLUSTERED ([CustomerID], [VersionStartTime]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入学大年级。对应类别C_CODE_ABBR_CUSTOMER_GRADE（年级）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'EntranceGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文理科(C_CODE_ABBR_STUDENTBRANCH)。1:文科，2:理科，3:不分科',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SubjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学年制(C_CODE_ABBR_ACDEMICYEAR)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SchoolYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生描述',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Character'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生生日',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Birthday'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前年级。年级(C_CODE_ABBR_CUSTOMER_GRADE)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'Grade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生类型(C_CODE_ABBR_CUSTOMER_STUDENTTYPE)，默认51',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'StudentType'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'PurchaseIntention'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'潜在客户表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)0=未确认客户信息, 1 = 确认客户信息, 9=无效用户（逻辑删除），10=正式学员',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别(C_CODE_ABBR_GENDER)。1--男，2--女',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'SchoolID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'vip客户(C_CODE_ABBR_CUSTOMER_VipType)。1:关系vip客户 2:大单vip客户',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VipType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'vip客户等级（C_CODE_ABBR_CUSTOMER_VipLevel）。1:A级 2:B级 3:C级',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VipLevel'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'FollowTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计下次跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'NextFollowTime'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'接触方式(C_CODE_ABBR_Customer_CRM_NewContactType)。"1：呼入 2：呼出 3：直访 4：在线咨询-乐语 5：在线咨询-其他"',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'ContactType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件类型(C_CODE_ABBR_BO_Customer_CertificateType)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IDType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'信息来源一级分类(C_Code_Abbr_BO_Customer_Source)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SourceMainType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'信息来源二级分类(C_Code_Abbr_BO_Customer_Source)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SourceSubType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'归属组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'归属组织机构类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IDNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'来源系统',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SourceSystem'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工OA编号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralStaffJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ReferralCustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否仅登记一个家长',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'IsOneParent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户级别（A1,A2,A3...）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进阶段（销售阶段）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'FollowStage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已跟进次数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'FollowedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否复读',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IsStudyAgain'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电子邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无效客户理由代码（参考跟进）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'InvalidReason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'