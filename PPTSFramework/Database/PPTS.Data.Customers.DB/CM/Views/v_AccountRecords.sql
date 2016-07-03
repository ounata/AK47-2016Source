CREATE VIEW [CM].[v_AccountRecords]
	AS 
	SELECT  CampusID, CustomerID, AccountID, RecordID, RecordTime, RecordType, RecordFlag, BillID, BillNo, BillTime, BillType, 
                   BillTypeName, BillMoney, BillMemo, BillerID, BillerName, BillerJobID, BillerJobName
	FROM      CM.AccountRecords --订购  退订
	union all
	select [CampusID],[CustomerID],AccountID,[ApplyID],[ApplyTime],'1',1,[ApplyID],[ApplyNo],[PayTime],[ChargeType],
			'',[PaidMoney],[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[AccountChargeApplies]
	where [PayStatus] = 1 --充值
	union all
	select [CampusID],[CustomerID],[AccountID],[ApplyID],[ApplyTime],'2',-1,[ApplyID],[ApplyNo],VerifyTime,[RefundType],
			'',RealRefundMoney,[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[AccountRefundApplies]
	where VerifyStatus = 3 --退费
	union all
	select [CampusID],[CustomerID],[AccountID],[ApplyID],[ApplyTime],'3',1,[ApplyID],[ApplyNo],[ApproveTime],[TransferType],
			'',[TransferMoney],[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[v_AccountTransferApplies]
	where [TransferType] = 2 and  [ApplyStatus] =2 --转入
	union all
	select [CampusID],[CustomerID],[AccountID],[ApplyID],[ApplyTime],'4',-1,[ApplyID],[ApplyNo],[ApproveTime],[TransferType],
			'',[TransferMoney],[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[v_AccountTransferApplies]
	where [TransferType] = 1 and  [ApplyStatus] =2 --转出
	union all
	select [CampusID],[CustomerID],[AccountID],[ApplyID],[ApplyTime],'7',-1,[ApplyID],[ApplyNo],[ApproveTime], '7',
	 '',ExpenseMoney,[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[AccountDeductApplies] 
	where [ApplyStatus] = 2 --综合服务费扣除
	union all 
	select [CampusID],[CustomerID],[AccountID],[ApplyID],[ApplyTime],'8',1,[ApplyID],[ApplyNo],[ApproveTime], '8',
	 '',ExpenseMoney,[ApplyMemo],[ApplierID],[ApplierName],[ApplierJobID],[ApplierJobName]
	from [CM].[AccountReturnApplies] 
	where [ApplyStatus] = 2 --综合服务费返回

