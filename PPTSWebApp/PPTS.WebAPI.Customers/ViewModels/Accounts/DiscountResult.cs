using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    public class DiscountResult
    {
        /// <summary>
        /// 折扣ID
        /// </summary>
        public string DiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣编码
        /// </summary>
        public string DiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣基数
        /// </summary>
        public decimal DiscountBase
        {
            set;
            get;
        }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal DiscountRate
        {
            set;
            get;
        }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 账户价值
        /// </summary>
        public decimal AccountValue
        {
            set;
            get;
        }

        //计算折扣基数
        public static DiscountResult CalcDiscount(AccountModel account, DiscountModel discount, decimal money)
        {
            decimal thatAccountMoney = 0;
            decimal thatAccountValue = 0;
            decimal thatDiscountBase = 0;
            decimal thatDiscountRate = 1;
            if (account != null)
            {
                thatAccountMoney = account.AccountMoney;
                thatAccountValue = account.AccountValue;
                thatDiscountBase = account.DiscountBase;
                thatDiscountRate = account.DiscountRate;
            }
            //新的账户余额=充值金额+以前的账户余额
            decimal thisAccountMoney = thatAccountMoney + money;
            //新的账户价值=充值金额+以前的账户价值
            decimal thisAccountValue = thatAccountValue + money;
            decimal thisDiscountBase = 0;
            decimal thisDiscountRate = 1;

            //新的账户价值大于上次折扣基数则使用新的账户价值作为折扣基数
            if (thisAccountValue > thatDiscountBase)
                thisDiscountBase = thisAccountValue;
            else
                thisDiscountBase = thatDiscountBase;
            //如果折扣基数相等就保留原来的折扣率
            if (thisDiscountBase == thatDiscountBase)
                thisDiscountRate = thatDiscountRate;
            else if (discount != null && discount.Items.Count != 0)
            {
                //根据折扣表重新计算新的折扣率
                thisDiscountRate = discount.Items[0].DiscountValue;
                for (var i = 0; i < discount.Items.Count; i++)
                {
                    var item = discount.Items[i];
                    if (thisDiscountBase >= item.DiscountStandard * 10000)
                    {
                        thisDiscountRate = item.DiscountValue;
                        break;
                    }
                }
            }
            //如果新折扣大于之前折扣使用之前折扣
            if (thisDiscountRate > thatDiscountRate)
                thisDiscountRate = thatDiscountRate;

            DiscountResult result = new DiscountResult();
            if (discount != null)
            {
                result.DiscountID = discount.DiscountID;
                result.DiscountCode = discount.DiscountCode;
            }
            result.DiscountBase = thisDiscountBase;
            result.DiscountRate = thisDiscountRate;
            result.AccountMoney = thisAccountMoney;
            result.AccountValue = thisAccountValue;
            return result;
        }
        //计算折扣基数
        public static DiscountResult CalcDiscount(AccountModel account, string campusID, decimal money)
        {
            DiscountModel discount = DiscountModel.LoadByCampusID(campusID);
            return CalcDiscount(account, discount, money);
        }
        public static DiscountResult CalcDiscount(string campusID, decimal money)
        {
            return CalcDiscount(null, campusID, money);
        }
    }
}