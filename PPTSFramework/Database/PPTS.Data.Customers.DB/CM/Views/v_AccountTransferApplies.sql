
CREATE VIEW [CM].[v_AccountTransferApplies]
AS
SELECT   ApplyID, ApplyNo, ApplyStatus, ApplyMemo, ApplyTime, ApplierID, ApplierName, ApplierJobID, ApplierJobName, 
                ProcessStatus, ProcessTime, ProcessMemo, TransferType = 1, TransferMoney, SubmitterID, SubmitterName, 
                SubmitterJobID, SubmitterJobName, SubmitTime, ApproverID, ApproverName, ApproverJobID, ApproverJobName, 
                ApproveTime, CampusID, CampusName, CustomerID, CustomerCode, CustomerName, AccountID, AccountCode, 
                ThatDiscountID, ThatDiscountCode, ThatDiscountBase, ThatDiscountRate, ThatAccountValue, ThatAccountMoney, 
                ThisDiscountID, ThisDiscountCode, ThisDiscountBase, ThisDiscountRate, ThisAccountValue, ThisAccountMoney, 
                BizCampusID, BizCampusName, BizCustomerID, BizCustomerCode, BizCustomerName, BizAccountID, BizAccountCode, 
                BizThatDiscountID, BizThatDiscountCode, BizThatDiscountBase, BizThatDiscountRate, BizThatAccountValue, 
                BizThatAccountMoney, BizThisDiscountID, BizThisDiscountCode, BizThisDiscountBase, BizThisDiscountRate, 
                BizThisAccountValue, BizThisAccountMoney, CreatorID, CreatorName, CreateTime, ModifierID, ModifierName, 
                ModifyTime, TenantCode
FROM      CM.AccountTransferApplies
UNION ALL
SELECT   ApplyID, ApplyNo, ApplyStatus, ApplyMemo, ApplyTime, ApplierID, ApplierName, ApplierJobID, ApplierJobName, 
                ProcessStatus, ProcessTime, ProcessMemo, TransferType = 2, TransferMoney, SubmitterID, SubmitterName, 
                SubmitterJobID, SubmitterJobName, SubmitTime, ApproverID, ApproverName, ApproverJobID, ApproverJobName, 
                ApproveTime, BizCampusID, BizCampusName, BizCustomerID, BizCustomerCode, BizCustomerName, BizAccountID, 
                BizAccountCode, BizThatDiscountID, BizThatDiscountCode, BizThatDiscountBase, BizThatDiscountRate, 
                BizThatAccountValue, BizThatAccountMoney, BizThisDiscountID, BizThisDiscountCode, BizThisDiscountBase, 
                BizThisDiscountRate, BizThisAccountValue, BizThisAccountMoney, CampusID, CampusName, CustomerID, 
                CustomerCode, CustomerName, AccountID, AccountCode, ThatDiscountID, ThatDiscountCode, ThatDiscountBase, 
                ThatDiscountRate, ThatAccountValue, ThatAccountMoney, ThisDiscountID, ThisDiscountCode, ThisDiscountBase, 
                ThisDiscountRate, ThisAccountValue, ThisAccountMoney, CreatorID, CreatorName, CreateTime, ModifierID, 
                ModifierName, ModifyTime, TenantCode
FROM      CM.AccountTransferApplies

GO
