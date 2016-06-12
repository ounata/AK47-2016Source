using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.DataObjects;
using PPTS.WebAPI.Customers.ViewModels.Students;
using MCS.Library.SOA.DataObjects;
using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerSearchDataSource : GenericSearchDataSource<StudentQueryResultModel, StudentQueryResultModelCollection>
    {
        public static readonly new CustomerSearchDataSource Instance = new CustomerSearchDataSource();

        private CustomerSearchDataSource()
        {
        }

        public PagedQueryResult<StudentQueryResultModel, StudentQueryResultModelCollection> LoadCustomerSearch(IPageRequestParams prp, StudentQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ConnectiveSqlClauseCollection sqlCollection = new ConnectiveSqlClauseCollection();
            sqlCollection.Add(ConditionMapping.GetConnectiveClauseBuilder(condition));
            string customerSqlBuilder = sqlCollection.ToSqlString(TSqlBuilder.Instance);

            string sqlBuilder = BuildQueryCondition(condition);

            sqlBuilder = string.IsNullOrEmpty(customerSqlBuilder) ? string.Format(" 1=1 {0}", sqlBuilder) : string.Format("{0}{1}", customerSqlBuilder, sqlBuilder);

            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @"CampusID,CustomerID,CustomerName, CustomerCode, ParentName, FirstSignTime, SchoolName, Grade, EducatorName, ConsultantName, 
                                ISNULL(AccountContractConfirmedAmount+AssetOneToOneAmount,0)+ISNULL(AssetClassAmount,0)+ISNULL(AssetOneToOneConfirmedAmount,0)+ISNULL(AssetClassConfirmedAmount,0) AS AssignedAmount,
                                ISNULL(AssetOneToOneAmount,0)+ISNULL(AssetClassAmount,0)+ISNULL(AssetOtherAmount,0) AS AssetRemainAmount,
                                ISNULL(AssetOneToOneMoney,0)+ISNULL(AssetClassMoney,0)+ISNULL(AssetOtherMoney,0)+ISNULL(AccountMoney,0) AS AccountMoney,
                                ISNULL(AssetOneToOneMoney,0)+ISNULL(+AssetClassMoney,0)+ISNULL(+AssetOtherMoney,0) AS OrderMoney,
                                ISNULL(AccountMoney,0) AS AvaiableMoney,
                                DATEDIFF(DAY, AssignTime, GETUTCDATE()) AS LastAssignDays";
            qc.FromClause = @" SM.CustomerSearch ";

            switch (qc.OrderByClause)
            {
                case "assignedAmount DESC":
                    qc.OrderByClause = " AccountContractConfirmedAmount+AssetOneToOneAmount+AssetClassAmount+AssetOneToOneConfirmedAmount+AssetClassConfirmedAmount DESC";
                    break;
                default:
                    break;
            }
            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(StudentQueryResultModelCollection result)
        {
        }

        private string BuildQueryCondition(StudentQueryCriteriaModel condition)
        {
            string sqlBuilder = string.Empty;

            #region 学员状态

            string customerStatusBuilder = string.Empty;

            string accountAmount = @" ISNULL(AssetOneToOneMoney,0)+ISNULL(AssetClassMoney,0) + ISNULL(AssetOtherMoney,0) + ISNULL(AccountMoney,0) "; // 账户价值
            string avaiableAmount = @" ISNULL(AssetOneToOneAmount,0)+ISNULL(AssetClassAmount,0)+ISNULL(AssetOtherAmount,0) "; // 剩余课时

            #region 最后上课时间查询方式 与之有关的包括：有效、停课、休学、结课

            int minDays = 30, maxDays = 180;
            string dateTypeBuilder_Valid = string.Empty, dateTypeBuilder_Attend = string.Empty, dateTypeBuilder_Stop = string.Empty, dateTypeBuilder_Suspend = string.Empty;
            if (condition.LastCourseType != -1)
            {
                if (condition.LastCourseType == (byte)DateTypeDefine.LastCourse)
                {
                    dateTypeBuilder_Valid = string.Format(
                        @" (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())<={0}) ", maxDays);
                    dateTypeBuilder_Attend = string.Format(
                        @" (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())<={0}) ", minDays);
                    dateTypeBuilder_Stop = string.Format(
                        @" (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())>{0} and DATEDIFF(DD,AssignTime,GETUTCDATE())<={1}) ", minDays, maxDays);
                    dateTypeBuilder_Suspend = string.Format(
                        @" (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())>{0}) ", maxDays);
                }
                else if (condition.LastCourseType == (byte)DateTypeDefine.LastPay)
                {
                    dateTypeBuilder_Valid = string.Format(
                        @" (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={0}) ", maxDays);
                    dateTypeBuilder_Attend = string.Format(
                        @" (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={0}) ", minDays);
                    dateTypeBuilder_Stop = string.Format(
                        @" (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())>{0} and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={1}) ", minDays, maxDays);
                    dateTypeBuilder_Suspend = string.Format(
                        @" (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())>{0}) ", maxDays);
                }
            }
            else
            {
                // 默认查询方式：取最后一次上课时间，如果没有上课，取最后一次付款时间
                dateTypeBuilder_Valid = string.Format(
                    @" ( (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())<={0})
		              or (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={0}) )", maxDays);
                dateTypeBuilder_Attend = string.Format(
                    @" ( (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())<={0})
		              or (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={0}) )", minDays);
                dateTypeBuilder_Stop = string.Format(
                    @" ( (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())>{0} and DATEDIFF(DD,AssignTime,GETUTCDATE())<={1})
		              or (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())>{0} and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())<={1}) )", minDays, maxDays);
                dateTypeBuilder_Suspend = string.Format(
                    @" ( (AssignTime is not null and DATEDIFF(DD,AssignTime,GETUTCDATE())>{0} )
		              or (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,GETUTCDATE())>{0}) )", maxDays);
            }
            #endregion 

            if (condition.CustomerType != -1)
            {
                if (condition.CustomerType == (byte)CustomerTypeDefine.Valid)   // 有效学员：账户价值金额>=200，或剩余课时>=1；最后一次上课时间（如果没有上课，取付款时间）据查询时间<=180天，不含高三毕业库学生
                {
                    customerStatusBuilder = string.Format(@" and ({0}>=200 or {1}>=1) and {2} and Graduated <> 0", accountAmount, avaiableAmount, dateTypeBuilder_Valid);
                    if (condition.ValidType != -1) // 有效学员二级
                    {
                        if (condition.ValidType == (byte)ValidDefine.OneToOneValid)
                        {
                            customerStatusBuilder += " and ISNULL(AssetOneToOneAmount,0)>0";
                        }
                        else if (condition.ValidType == (byte)ValidDefine.ClassValid)
                        {
                            customerStatusBuilder += " and ISNULL(AssetClassAmount,0)>0";
                        }
                        else if (condition.ValidType == (byte)ValidDefine.OtherValid)
                        {
                            customerStatusBuilder += " and ISNULL(AssetOtherAmount,0)>0";
                        }
                    }
                }
                else if (condition.CustomerType == (byte)CustomerTypeDefine.Attend) // 上课学员：最后一次上课/付款时间据查询时间<=30天的学员
                {
                    customerStatusBuilder = " and " + dateTypeBuilder_Attend;
                    if (condition.AttendType != -1) // 上课学员二级
                    {
                        if (condition.AttendType == (byte)AttendDefine.OneToOneAttend)
                        {
                        }
                        else if (condition.AttendType == (byte)AttendDefine.ClassAttend)
                        {
                        }
                        else if (condition.AttendType == (byte)AttendDefine.OtherAttend)
                        {
                        }
                    }
                    if (condition.StatusStartTimeUTC != null && condition.StatusEndTimeUTC != null) // 本月上课
                    {
                        customerStatusBuilder += string.Format(
                            @" and ( (AssignTime is not null and AssignTime between '{0}' and '{1}') 
                                  or (AssignTime is null and LastAccountChargeApplyTime between '{0}' and '{1}') ) ", condition.StatusStartTime, condition.StatusEndTime);
                    }
                }
                else if (condition.CustomerType == (byte)CustomerTypeDefine.Stop) // 停课学员：账户价值>=200，或剩余课时>=1；最后一次上课/付款时间据查询时间>=31天，<=180天的学员，不含高三毕业库学生
                {
                    customerStatusBuilder = string.Format(@" and ({0}>=200 or {1}>=1) and {2} and Graduated <> 0", accountAmount, avaiableAmount, dateTypeBuilder_Stop);
                    if (condition.StopType != -1) // 有效学员二级
                    {
                        if (condition.StopType == (byte)StopDefine.OneToOneStop)
                        {
                            customerStatusBuilder += " and ISNULL(AssetOneToOneAmount,0)>0";
                        }
                        else if (condition.StopType == (byte)StopDefine.ClassStop)
                        {
                            customerStatusBuilder += " and ISNULL(AssetClassAmount,0)>0";
                        }
                    }
                    if (condition.StatusStartTimeUTC != null && condition.StatusEndTimeUTC != null) // 本月新增
                    {
                        customerStatusBuilder += string.Format(
                            @" and ( (AssignTime is not null and DATEDIFF(DD,AssignTime,'{0}')<=30 and DATEDIFF(DD,AssignTime,'{1}')>30)  
		                         or  (AssignTime is null and DATEDIFF(DD,LastAccountChargeApplyTime,'{0}')<=30 and DATEDIFF(DD,LastAccountChargeApplyTime,'{1}')>30))", condition.StatusStartTimeUTC, condition.StatusEndTimeUTC);
                    }
                }
                else if (condition.CustomerType == (byte)CustomerTypeDefine.Suspend) // 休学学员：账户价值>=200，或剩余课时>=1；最后一次上课/付款时间据查询时间>=31天，<=180天的学员
                {
                    customerStatusBuilder = string.Format(@" and ({0}>=200 or {1}>=1) and {2} and Graduated <> 0", accountAmount, avaiableAmount, dateTypeBuilder_Stop);
                    if (condition.SuspendType != -1) // 休学学员二级
                    {
                        if (condition.SuspendType == (byte)SuspendDefine.OneToOneSuspend)
                        {
                            customerStatusBuilder += " and ISNULL(AssetOneToOneAmount,0)>0";
                        }
                        else if (condition.SuspendType == (byte)SuspendDefine.ClassSuspend)
                        {
                            customerStatusBuilder += " and ISNULL(AssetClassAmount,0)>0";
                        }
                    }
                    if (condition.StatusStartTimeUTC != null && condition.StatusEndTimeUTC != null) // 本月新增休学
                    {
                        customerStatusBuilder += string.Format(
                            @" and( (AssignTime is not null and DATEDIFF(DD,AssignTime,'{0}')<=180 and DATEDIFF(DD,AssignTime,'{1}')>180 )
                                or  (AssignTime is null and DATEDIFF(DD,AssignTime,'{0}')<=180 and DATEDIFF(DD,AssignTime,'{1}')>180) )", condition.StatusStartTime, condition.StatusEndTime);
                    }
                }
                else if (condition.CustomerType == (byte)CustomerTypeDefine.Completed) // 结课学员：账户价值<200，且剩余课时<1的学员
                {
                    customerStatusBuilder = string.Format(@" and ({0}<200 and {1}<1)", accountAmount, avaiableAmount);
                    if (condition.CompletedType != -1)
                    {
                        if (condition.CompletedType == (byte)CompletedDefine.ConsumeCompleted) // 消耗结课：没有退费记录，有“已上”上课记录
                        {
                            customerStatusBuilder += @" and (LastAccountRefundTime is null and AssignTime is not null) ";
                        }
                        else if (condition.CompletedType == (byte)CompletedDefine.ReturnCompleted) // 退费结课：只要有退费记录，且“分区域财务经理审批通过”，就算退费结课
                        {
                            customerStatusBuilder += @" and (LastAccountRefundTime is not null) ";
                        }
                        else if (condition.CompletedType == (byte)CompletedDefine.TransferCompleted) // 转让结课：没有退费记录，没有“已上”上课记录，有转出记录
                        {
                            customerStatusBuilder += @" and (LastAccountRefundTime is null and AssignTime is null and LastAccountTransferOutTime is not null) ";
                        }
                    }
                    if (condition.StatusStartTimeUTC != null && condition.StatusEndTimeUTC != null) // 本月新增结课
                    {
                        // ...
                    }
                }
                else if (condition.CustomerType == (byte)CustomerTypeDefine.NoOrder) // 无订单学员：账户价值>=200且剩余课时=0的学员
                {
                    customerStatusBuilder = string.Format(@" and ({0}>=200 and {1}=0)", accountAmount, avaiableAmount);
                    if (condition.StatusStartTimeUTC != null && condition.StatusEndTimeUTC != null) // 本月新增无订单
                    {
                        // ...
                    }
                }
            }
            #endregion

            #region 高三毕业库：高三毕业 && (账户价值>=200 or 剩余课时>=1)
            string graduateBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.Graduated))
            {
                graduateBuilder = string.Format(@" {0}>=200 or {1}>=1 and Graduated = 0 ", accountAmount, avaiableAmount);

                if (condition.Graduated == "0")
                    graduateBuilder += string.Format(@" and GraduateYear = '{0}'", DateTime.Now.Year); // 今年新入库学员
                else if (condition.Graduated == "1")
                    graduateBuilder += string.Format(@" and GraduateYear < '{0}'", DateTime.Now.Year); // 历史入库学员
            }
            #endregion

            sqlBuilder = graduateBuilder + customerStatusBuilder;

            return sqlBuilder;
        }

    }
}