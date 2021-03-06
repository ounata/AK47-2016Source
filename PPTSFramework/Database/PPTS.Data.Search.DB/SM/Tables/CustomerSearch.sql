
CREATE TABLE [SM].[CustomerSearch](
	[CampusID] [nvarchar](36) NULL,
	[CampusName] [nvarchar](128) NULL,
    [ParentID] NVARCHAR(36) NULL, 
    [ParentCode] NVARCHAR(64) NULL, 
    [ParentName] NVARCHAR(64) NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
    [CustomerLevel] NVARCHAR(32) NULL, 
	[CustomerStatus] [nvarchar](32) NULL,
	[StudentStatus] [nvarchar](32) NULL,
    [IsOneParent] INT NULL DEFAULT 1, 
	[Character] [nvarchar](MAX) NULL,
	[Birthday] [datetime] NULL,
	[Gender] [nvarchar](32) NULL,
    [Email] NVARCHAR(255) NULL, 
	[IDType] [nvarchar](32) NULL,
	[IDNumber] [nvarchar](64) NULL,
	[VipType] NUMERIC NULL,
	[VipLevel] [nvarchar](32) NULL,
	[EntranceGrade] [nvarchar](32) NULL,
	[Grade] [nvarchar](32) NULL,
	[SubjectType] [nvarchar](32) NULL,
	[StudentType] [nvarchar](32) NULL,
	[ContactType] [nvarchar](32) NULL,
	[SourceMainType] [nvarchar](32) NULL,
	[SourceSubType] [nvarchar](32) NULL,
	[SourceSystem] [nvarchar](32) NULL, 
	[SchoolID] [nvarchar](32) NULL,
    [SchoolName] NVARCHAR(128) NULL, 
    [IsStudyAgain] INT NULL DEFAULT 0, 
    [FirstSignTime] DATETIME NULL, 
    [FirstSignerID] NVARCHAR(36) NULL, 
    [FirstSignerName] NVARCHAR(64) NULL, 
    [FollowTime] DATETIME NULL , 
    [FollowStage] NVARCHAR(32) NULL, 
    [FollowedCount] INT NULL DEFAULT 0, 
    [NextFollowTime] DATETIME NULL , 
    [VisitTime] Datetime NULL,
	[VisitedCount] INT NULL DEFAULT 0, 
	[NextVisitTime] Datetime NULL,
    [ConsultantID] NVARCHAR(36) NULL, 
    [ConsultantName] NVARCHAR(64) NULL, 
    [EducatorID] NVARCHAR(36) NULL, 
    [EducatorName] NVARCHAR(64) NULL, 
    [LastOneToOneOrderTime] DATETIME NULL, 
    [InvalidReason] NVARCHAR(32) NULL, 
    [SchoolYear] NVARCHAR(32) NULL, 
    [ReferralStaffID] NVARCHAR(36) NULL, 
    [ReferralStaffName] NVARCHAR(64) NULL, 
    [ReferralStaffJobID] NVARCHAR(36) NULL, 
    [ReferralStaffJobName] NVARCHAR(64) NULL, 
    [ReferralStaffOACode] NVARCHAR(128) NULL, 
    [ReferralCustomerID] NVARCHAR(36) NULL, 
    [ReferralCustomerCode] NVARCHAR(32) NULL, 
    [ReferralCustomerName] NVARCHAR(64) NULL, 
    [PurchaseIntention] NVARCHAR(32) NULL, 
	[Locked] INT NULL DEFAULT 0, 
	[LockMemo] NVARCHAR(max) NULL,
	[Graduated] INT NULL DEFAULT 0, 
	[GraduateYear] NVARCHAR(32) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(36) NULL, 
    [CreatorJobID] NVARCHAR(36) NULL, 
    [CreatorJobType] NVARCHAR(32) NULL, 
    [CreateTime] DATETIME NULL, 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL, 
    [ConsultantJobID] NVARCHAR(36) NULL, 
    [EducatorJobID] NVARCHAR(36) NULL, 
    [CallCenterJobID] NVARCHAR(36) NULL, 
    [CallCenterID] NVARCHAR(36) NULL, 
    [CallCenterName] NVARCHAR(64) NULL, 
    [MarketingJobID] NVARCHAR(36) NULL, 
    [MarketingID] NVARCHAR(36) NULL, 
    [MarketingName] NVARCHAR(64) NULL, 
    [AssignTeacherNum] INT NULL, 
    [ParentIDType] NVARCHAR(32) NULL, 
    [ParentIDNumber] NVARCHAR(64) NULL, 
    [ParentRoleName] NVARCHAR(128) NULL, 
    [CustomerRoleName] NVARCHAR(128) NULL, 
    [CustomerPrimaryPhoneType] NVARCHAR(32) NULL, 
    [CustomerPrimaryPhoneNumber] NVARCHAR(255) NULL, 
    [CustomerSecondaryPhoneType] NVARCHAR(32) NULL, 
    [CustomerSecondaryPhoneNumber] NVARCHAR(255) NULL, 
    [ParentPrimaryPhoneType] NVARCHAR(32) NULL, 
    [ParentPrimaryPhoneNumber] NVARCHAR(255) NULL, 
    [StudentSecondaryPhoneType] NVARCHAR(32) NULL, 
    [StudentSecondaryPhoneNumber] NVARCHAR(255) NULL, 
	[CustomerSchoolID] NVARCHAR(36) NULL,
    [CustomerSchoolName] NVARCHAR(255) NULL, 
    [VerifyTime] DATETIME NULL, 
    [VerifiedCount] INT NULL, 
	[NextVerifyTime] DATETIME NULL,
    [ScoreCount] INT NULL, 
    [FirstAccountChargeApplyTime] DATETIME NULL, 
    [FirstAccountChargeApplyMoney] DECIMAL(18, 2) NULL, 
    [LastAccountChargeApplyTime] DATETIME NULL, 
    [LastAccountChargeApplyMoney] DECIMAL(18, 2) NULL, 
    [LastAccountRefundTime] DATETIME NULL, 
    [LastAccountRefundMoney] DECIMAL(18, 2) NULL, 
    [AccountMoney] DECIMAL(18, 2) NULL, 
    [AccountDiscountRate] DECIMAL(18, 2) NULL, 
    [AccountDiscountBase] DECIMAL(18, 2) NULL, 
    [IsHasExpense] BIT NULL, 
    [ExpenseTime] DATETIME NULL, 
    [ExpenseFee] DECIMAL(18, 2) NULL, 
    [AccountTransferInMoney] DECIMAL(18, 2) NULL, 
    [LastAccountTransferInTime] DATETIME NULL, 
    [AccountTransferOutMoney] DECIMAL(18, 2) NULL, 
    [LastAccountTransferOutTime] DATETIME NULL, 
    [AssetOneToOneAmount] DECIMAL(18, 2) NULL, 
    [AssetClassAmount] DECIMAL(18, 2) NULL, 
    [AssetOtherAmount] DECIMAL(18, 2) NULL, 
    [AssetOneToOneMoney] DECIMAL(18, 2) NULL, 
    [AssetClassMoney] DECIMAL(18, 2) NULL, 
    [AssetOtherMoney] DECIMAL(18, 2) NULL, 
    [AssetOneToOneAssignedAmount] DECIMAL(18, 2) NULL, 
    [AssetClassAssignedAmount] DECIMAL(18, 2) NULL, 
    [AssetOneToOneAssignedMoney] DECIMAL(18, 2) NULL, 
    [AssetClassAssignedMoney] DECIMAL(18, 2) NULL, 
    [AssetOneToOneConfirmedAmount] DECIMAL(18, 2) NULL, 
    [AssetClassConfirmedAmount] DECIMAL(18, 2) NULL, 
    [AssetOtherConfirmedAmount] DECIMAL(18, 2) NULL, 
    [AssetOneToOneConfirmedMoney] DECIMAL(18, 2) NULL, 
    [AssetClassConfirmedMoney] DECIMAL(18, 2) NULL, 
    [AssetOtherConfirmedMoney] DECIMAL(18, 2) NULL, 
    [AssetPresentAmount] DECIMAL(18, 2) NULL, 
    [LastOrderTime] DATETIME NULL, 
    [LastDebookOrderTime] DATETIME NULL, 
    [AssignTime] DATETIME NULL, 
    [LastAccountContractRefundTime] DATETIME NULL, 
    [LastAccountContractRefundMoney] DECIMAL(18, 2) NULL, 
    [AccountContractMoney] DECIMAL(18, 2) NULL, 
    [AccountContractDiscountRate] DECIMAL(18, 2) NULL, 
    [AccountContractDiscountBase] DECIMAL(18, 2) NULL, 
    [AccountContractAmount] DECIMAL(18, 2) NULL, 
    [AccountContractAssignedAmount] DECIMAL(18, 2) NULL, 
    [AccountContractAssignedMoney] DECIMAL(18, 2) NULL, 
    [AccountContractConfirmedAmount] DECIMAL(18, 2) NULL, 
    [AccountContractConfirmedMoney] DECIMAL(18, 2) NULL, 
    [ServiceModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    [OneToOneLastClassTime] DATETIME NULL, 
    [GroupLastClassTime] DATETIME NULL, 
    [CompleteType] NVARCHAR(32) NULL, 
    [CompleteTime] DATETIME NULL, 
    [ReferralCount] INT NULL, 
    [TenantCode] NVARCHAR(36) NULL, 
    [SearchContent] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_CustomerSearch] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
) ON [PRIMARY]

GO

/****** Object:  Index [IX_CustomerSearch_1]    Script Date: 2016/3/23 15:16:58 ******/
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员编码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'CustomerCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员姓名' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员描述' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'Character'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生年月' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件类型' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'IDType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件号码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'IDNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入学年级代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='EntranceGrade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前年级代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='Grade'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学科类型代码（文科，理科，不分科）' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='SubjectType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员类型代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='StudentType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接触方式代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='ContactType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息来源一级分类代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='SourceMainType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息来源二级分类代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='SourceSubType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源系统代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='SourceSystem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别代码' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='Gender'
GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在读学校ID' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name=N'SchoolID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员状态（枚举-  正常，休学，冻结（高三毕业9月后），结业）' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch', @level2type=N'COLUMN',@level2name='CustomerStatus'
GO

GO

GO

GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员信息表' , @level0type=N'SCHEMA',@level0name=N'SM', @level1type=N'TABLE',@level1name=N'CustomerSearch'
GO


GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'VIP类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'VipType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'VIP级别',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'VipLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否仅登记一个家长',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'IsOneParent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户等级（A1,A2,A3...)',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约人ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次签约人',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'FirstSignerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前跟进阶段',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'FollowStage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已跟进次数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'FollowedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已回访次数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'VisitedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'FollowTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下一次跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'NextFollowTime'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否复读',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'IsStudyAgain'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电子邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否高三毕业',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'Graduated'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'高三毕业年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'GraduateYear'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'SchoolName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长编号',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentCode'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'EducatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'EducatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'一对一最后订购日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastOneToOneOrderTime'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无效客户理由代码（参考跟进）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'InvalidReason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学年制(C_CODE_ABBR_ACDEMICYEAR)',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'SchoolYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralStaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralStaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralStaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralStaffJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍员工OA编号',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralStaffOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralCustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralCustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralCustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'PurchaseIntention'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'建档人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'CreatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'建档人ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'建档人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'EducatorJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电销人员岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CallCenterJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电销人员员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CallCenterID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电销人员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CallCenterName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'市场人员岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'MarketingJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'市场人员员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'MarketingID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'市场人员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'MarketingName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分配教师数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssignTeacherNum'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主要监护人证件类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentIDType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主要监护人证件号码',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentIDNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主要监护人称谓',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentRoleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员称谓',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerRoleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员主要联系电话类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerPrimaryPhoneType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员主要联系电话',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerPrimaryPhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员次要联系电话类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerSecondaryPhoneType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员次要联系电话',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerSecondaryPhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长主要联系电话类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentPrimaryPhoneType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长主要联系电话',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ParentPrimaryPhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长次要联系电话类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'StudentSecondaryPhoneType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长次要联系电话',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'StudentSecondaryPhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前在读学校名称',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerSchoolName'
GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成绩单数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ScoreCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次缴费时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'FirstAccountChargeApplyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次缴费金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'FirstAccountChargeApplyMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次缴费时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountChargeApplyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次缴费金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountChargeApplyMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次退费时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountRefundTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次退费金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountRefundMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否扣减服务费',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'IsHasExpense'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务费扣减日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'ExpenseTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减服务费金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'ExpenseFee'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转入金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountTransferInMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次转入日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountTransferInTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转出金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountTransferOutMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次转出日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountTransferOutTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(1对1)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(班组)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(其他)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOtherAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(1对1)金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(班组)金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产(其他)金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOtherMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定资产(1对1)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneAssignedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定资产(班组)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassAssignedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定资产(1对1)价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneAssignedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排定资产(班组)价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassAssignedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(1对1)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneConfirmedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(班组)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassConfirmedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(其他)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOtherConfirmedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(1对1)价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOneToOneConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(班组)价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetClassConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗资产(其他)价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetOtherConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗赠送资产(1对1)数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssetPresentAmount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次订购日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastOrderTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次退订日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastDebookOrderTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次上课日期',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AssignTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同最后一次退费时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountContractRefundTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同最后一次退费金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'LastAccountContractRefundMoney'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同账户折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同账户折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同剩余资产数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractAmount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同排定资产数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractAssignedAmount'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同排定资产价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractAssignedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同消耗资产数量',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractConfirmedAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同消耗资产价值',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'老合同账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'AccountContractMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员状态（枚举-  正常-11，停课-12，休学-13，冻结-14（高三毕业9月后），结业-15）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'StudentStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否锁定',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'Locked'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'锁定描述（转学，毕业）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'LockMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上次上门时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'VerifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上门次数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'VerifiedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下次上门时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'NextVerifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上次回访时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'VisitTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'下次回访时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'NextVisitTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务更新时间，每做一次更新更新一次时间。',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ServiceModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前在读学校ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CustomerSchoolID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1最后上课时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneLastClassTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组最后上课时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'GroupLastClassTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'结课类型（ 1 无 2 上课结课 3 转让结课 4 退费结课）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CompleteType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'结课时间',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'CompleteTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员个数 ',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'ReferralCount'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'建档人岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = 'CreatorJobType'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'全文检索内容',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSearch',
    @level2type = N'COLUMN',
    @level2name = N'SearchContent'