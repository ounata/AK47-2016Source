using PPTS.Contracts.Products.Models;
using PPTS.Contracts.Proxies;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Executors
{
    public class StudentApproveTransferModel : ApproveModelBase
    {
        public Customer Customer
        {
            set;
            get;
        }

        public CustomerTransferApply Apply
        {
            get;
            private set;
        }

        private List<Account> _accounts = new List<Account>();
        /// <summary>
        /// 要转学的账户
        /// </summary>
        public List<Account> Accounts
        {
            get
            {
                return _accounts;
            }
        }

        /// <summary>
        /// 为审批准备数据
        /// </summary>
        public void PrepareApprove()
        {
            CustomerTransferApply apply = CustomerTransferApplyAdapter.Instance.LoadByApplyID(this.BillID);
            if (apply == null)
                throw new Exception(string.Format("转学申请单ID为[{0}]的记录不存在", this.BillID));

            Customer customer = CustomerAdapter.Instance.Load(apply.CustomerID);
            if (customer == null)
                throw new Exception(string.Format("学员ID为[{0}]的记录不存在", apply.CustomerID));

            customer.CampusID = apply.ToCampusID;
            customer.CampusName = apply.ToCampusName;

            this.Apply = apply;
            this.Customer = customer;

            //获取新校区的折扣表
            DiscountQueryResult discount = PPTSConfigRuleQueryServiceProxy.Instance.QueryDiscountByCampusID(apply.ToCampusID);
            //获取学员所有账户
            AccountCollection accounts = AccountAdapter.Instance.LoadCollectionByCustomerID(apply.CustomerID);
            //重新计算折扣率
            foreach (Account account in accounts)
            {
                decimal rate = account.DiscountRate;
                this.FillDiscountRate(account, discount.DiscountItemCollection);
                //折扣率没有发生变化的就不考虑更新了
                if (rate != account.DiscountRate)
                    this.Accounts.Add(account);
            }
        }

        //构建折扣率
        private Account FillDiscountRate(Account account, List<DiscountItem> discountItems)
        {
            if (discountItems != null)
            {
                for (var i = 0; i < discountItems.Count; i++)
                {
                    var item = discountItems[i];
                    if (account.DiscountBase >= item.DiscountStandard * 10000)
                    {
                        account.DiscountRate = item.DiscountValue;
                        break;
                    }
                }
            }
            return account;
        }
    }
}
