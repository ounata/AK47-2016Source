using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Proxies;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 退费折扣返还结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundReallowanceResult
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID { get; set; }

        /// <summary>
        /// 折扣ID
        /// </summary>
        [DataMember]
        public string DiscountID { get; set; }

        /// <summary>
        /// 折扣编码
        /// </summary>
        [DataMember]
        public string DiscountCode { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        [DataMember]
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 折扣返还金额
        /// </summary>
        [DataMember]
        public decimal ReallowanceMoney { get; set; }
        /// <summary>
        /// 获取退费折扣返还数据
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="discountID">折扣ID</param>
        /// <param name="discountBase">折扣基数</param>
        /// <param name="reallowanceStartTime">折扣返还时间计算时间点</param>
        /// <returns></returns>
        public static RefundReallowanceResult GetReallowance(string accountID, string discountID, decimal discountBase, DateTime reallowanceStartTime)
        {
            RefundReallowanceResult result = new RefundReallowanceResult();
            DiscountModel discount = DiscountModel.LoadByDiscountID(discountID);
            result.AccountID = accountID;
            result.DiscountRate = 1;
            if (discount != null && discount.Items.Count != 0)
            {
                result.DiscountID = discount.DiscountID;
                result.DiscountCode = discount.DiscountCode;
                result.DiscountRate = discount.Items[0].DiscountValue;
                for (var i = 0; i < discount.Items.Count; i++)
                {
                    var item = discount.Items[i];
                    if (discountBase >= item.DiscountStandard * 10000)
                    {
                        result.DiscountRate = item.DiscountValue;
                        break;
                    }
                }
            }
            RefundReallowanceMoneyQueryCriteriaModel criteria = new RefundReallowanceMoneyQueryCriteriaModel();
            criteria.AccountID = accountID;
            criteria.RefundDiscountRate = result.DiscountRate;
            criteria.ReallowanceStartTime = reallowanceStartTime;

            RefundReallowanceMoneyQueryResult r = PPTSAssetQueryServiceProxy.Instance.QueryReallowanceMoney(criteria);
            if (r != null)
                result.ReallowanceMoney = r.ReallowanceMoney;
            return result;
        }
    }
}