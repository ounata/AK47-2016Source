using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Proxies;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    /// <summary>
    /// 转学申请模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class StudentTransferApplyModel : CustomerTransferApply
    {
        /// <summary>
        /// 学员编码
        /// </summary>
        [DataMember]
        public string CustomerCode
        {
            set;
            get;
        }

        /// <summary>
        /// 学员名称
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            set;
            get;
        }

        /// <summary>
        /// 初始化提交人信息
        /// </summary>
        public void InitApplier(IUser user)
        {
            this.ApplierID = user.ID;
            this.ApplierName = user.Name;
            this.ApplierJobID = user.GetCurrentJob().ID;
            this.ApplierJobName = user.GetCurrentJob().Name;
            this.ApplyTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = Data.Customers.ApplyStatusDefine.New;
        }

        /// <summary>
        /// 根据申请人初始化提交人信息
        /// </summary>
        public void InitSubmitter(IUser user)
        {
            this.SubmitterID = user.ID;
            this.SubmitterName = user.Name;
            this.SubmitterJobID = user.GetCurrentJob().ID;
            this.SubmitterJobName = user.GetCurrentJob().Name;
            this.SubmitTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = ApplyStatusDefine.Approving;
        }

        /// <summary>
        /// 初始化审批人
        /// </summary>
        /// <param name="user"></param>
        public void InitApprover(IUser user, ApplyStatusDefine status)
        {
            this.ApproverID = user.ID;
            this.ApproverName = user.Name;
            this.ApproverJobID = user.GetCurrentJob().ID;
            this.ApproverJobName = user.GetCurrentJob().Name;
            this.ApproveTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = status;
        }

        private List<string> _accountIDs = new List<string>();
        public List<string> AccountIDs
        {
            set
            {
                if (value == null)
                    _accountIDs.Clear();
                else
                    _accountIDs = value;
            }
            get
            {
                return _accountIDs;
            }
        }

        /// <summary>
        /// 准备数据
        /// </summary>
        public void Prepare(IUser user)
        {
            if (this.CampusID == this.ToCampusID)
            {
                throw new Exception("当前校区与转至校区不能相同");
            }
            //获取所有的账户列表进行判定
            AssetStatisticQueryResult asset = PPTSAssetQueryServiceProxy.Instance.QueryAssetStatisticByCustomerID(this.CustomerID);
#if !DEBUG
            if (asset != null && asset.AssignedAmount != 0)
            {
                throw new Exception("存在排课记录，无法转学，请首先取消排课记录");
            }
            DiscountModel discount1 = DiscountModel.LoadByCampusID(this.CampusID);
            if (discount1 == null)
            {
                throw new Exception("当前校区没有折扣表，请首先维护折扣表");
            }
            DiscountModel discount2 = DiscountModel.LoadByCampusID(this.ToCampusID);
            if (discount2 == null)
            {
                throw new Exception("转至校区没有折扣表，请首先维护折扣表");
            }
            if (discount1.DiscountID != discount2.DiscountID && asset != null && asset.AssetMoney != 0)
            {
                throw new Exception("存在未消耗的订购记录，无法转学，请首先退订");
            }
#endif
            foreach (Account account in AccountAdapter.Instance.LoadCollectionByCustomerID(this.CustomerID))
                this.AccountIDs.Add(account.AccountID);

            IOrganization campus = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, this.CampusID).SingleOrDefault();
            IOrganization branch = campus.GetUpperDataScope().GetParentOrganizationByType(DepartmentType.Branch);
            IOrganization toCampus = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, this.ToCampusID).SingleOrDefault();
            IOrganization toBranch = toCampus.GetUpperDataScope().GetParentOrganizationByType(DepartmentType.Branch);

            //保存短名称
            this.CampusName = campus.GetShortName();
            this.ToCampusName = toCampus.GetShortName();
            //判断是否跨分公司转学
            this.TransferType = branch.ID == toBranch.ID ? StudentTransferType.SameBranch : StudentTransferType.CrossBranch;
        }

        /// <summary>
        /// 根据学员ID获取缴费单模型
        /// </summary>
        /// <param name="customer">学员</param>
        /// <returns></returns>
        public static StudentTransferApplyModel LoadByCustomer(CustomerModel customer)
        {
            StudentTransferApplyModel model = new StudentTransferApplyModel();
            model.ApplyID = UuidHelper.NewUuidString();
            model.CampusID = customer.CampusID;
            model.CampusName = customer.CampusName;
            model.CustomerID = customer.CustomerID;
            model.CustomerCode = customer.CustomerCode;
            model.CustomerName = customer.CustomerName;
            return model;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static StudentTransferApplyModel LoadByApplyID(string applyID)
        {
            CustomerTransferApply apply = CustomerTransferApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                StudentTransferApplyModel model = apply.ProjectedAs<StudentTransferApplyModel>();
                CustomerModel customer = CustomerModel.Load(model.CustomerID);
                if (customer != null)
                {
                    model.CustomerCode = customer.CustomerCode;
                    model.CustomerName = customer.CustomerName;
                }
                return model;
            }
            return null;
        }
    }
}