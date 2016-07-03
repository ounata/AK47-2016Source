using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.WebAPI.Customers.ViewModels.Parents;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class PotentialCustomersController : ApiController
    {
        #region api/potentialcustomers/getallcustomers

        /// <summary>
        /// 潜客查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国")]
        public PotentialCustomerQueryResult GetAllCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return new PotentialCustomerQueryResult
            {
                QueryResult = PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent))
            };
        }

        /// <summary>
        /// 潜客查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国")]
        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> GetPagedCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/GetAllMarketCustomers

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国")]
        public PotentialCustomerQueryResult GetAllMarketCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            criteria.From = "market";
            return new PotentialCustomerQueryResult
            {
                QueryResult = PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国")]
        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> GetPagedMarketCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/createcustomer

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:新增潜在客户")]
        public CreatablePortentialCustomerModel CreateCustomer()
        {
            return new CreatablePortentialCustomerModel
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:新增潜在客户")]
        public void CreateCustomer(CreatablePortentialCustomerModel model)
        {
            AddPotentialCustomerExecutor executor = new AddPotentialCustomerExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/updatecustomer

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public EditablePotentialCustomerModel UpdateCustomer(string id)
        {
            return EditablePotentialCustomerModel.Load(id);
        }

        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:编辑潜客信息,编辑潜客信息-本部门")]
        [HttpPost]
        public void UpdateCustomer(EditablePotentialCustomerModel model)
        {
            #region 写入权限验证
            PPTS.Data.Common.Authorization.ScopeAuthorization<PotentialCustomer>
               .GetInstance(Data.Customers.ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth(model.Customer.CustomerID);
            #endregion

            EditPotentialCustomerExecutor executor = new EditPotentialCustomerExecutor(model);

            executor.Execute();
        }
        #endregion

        #region api/potentialcustomers/getstaffbyOA

        [HttpGet]
        public PPTSJobCollection GetStaffByOA(string staffOA)
        {
            return PotentialCustomerModel.GetStaffJobs(staffOA);
        }

        #endregion

        #region api/potentialcustomers/getcustomerbycode

        [HttpGet]
        public Customer GetCustomerByCode(string customerCode)
        {
            return CustomerAdapter.Instance.LoadByCustomerCode(customerCode);
        }

        #endregion

        #region api/potentialcustomers/getcustomerinfo

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public EditablePotentialCustomerModel GetCustomerInfo(string id)
        {
            return this.UpdateCustomer(id);
        }

        #endregion

        #region api/potentialcustomers/getstaffrelations

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public CustomerStaffRelationQueryResult GetStaffRelations(CustomerStaffRelationQueryCriteriaModel criteria)
        {
            return new CustomerStaffRelationQueryResult
            {
                QueryResult = CustomerStaffRelationDataSource.Instance.LoadCustomerStaffRelations(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public PagedQueryResult<CustomerStaffRelation, CustomerStaffRelationCollection> GetPagedStaffRelations(CustomerStaffRelationQueryCriteriaModel criteria)
        {
            return CustomerStaffRelationDataSource.Instance.LoadCustomerStaffRelations(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/getteacherrelations

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public CustomerTeacherRelationsQueryResult GetTeacherRelations(CustomerTeacherRelationsQueryCriteriaModel criteria)
        {
            return new CustomerTeacherRelationsQueryResult
            {
                QueryResult = GenericCustomerDataSource<CustomerTeacherAssignApply, CustomerTeacherAssignApplyCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerTeacherAssignApply))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public PagedQueryResult<CustomerTeacherAssignApply, CustomerTeacherAssignApplyCollection> GetPagedTeacherRelations(CustomerTeacherRelationsQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<CustomerTeacherAssignApply, CustomerTeacherAssignApplyCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/getcustomerparents

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public CustomerParentsQueryResult GetCustomerParents(string id)
        {
            return CustomerParentsQueryResult.GetCustomerParents(id);
        }
        #endregion

        #region api/potentialcustomers/getcustomerparent
        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public EditableParentModel GetCustomerParent(string id, string customerId)
        {
            EditableParentModel result = new EditableParentModel();

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(id, parent => result.Parent = parent);

            GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(customerId, customer => result.Customer = customer);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            result.CustomerParentRelation = CustomerParentRelationAdapter.Instance.Load(customerId, id);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent));

            return result;
        }
        #endregion

        #region api/potentialcustomers/getparentinfo
        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public EditableParentModel GetParentInfo(string id)
        {
            EditableParentModel result = new EditableParentModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(id, parent => result.Parent = parent);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
        #endregion

        #region api/potentialcustomers/updateparent

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:修改家长、学员非关键信息,修改家长、学员关键信息-本分公司")]
        public void UpdateParent(EditableParentModel model)
        {
            EditableParentExecutor executor = new EditableParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/getallparents

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public ParentsSearchQueryResult GetAllParents(ParentsSearchQueryCriteriaModel criteria)
        {
            return new ParentsSearchQueryResult
            {
                QueryResult = ParentDataSource.Instance.LoadParents(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:查看客户信息（含联系方式）,查看客户信息,查看客户信息-本部门,查看客户信息-本校区,查看客户信息-本分公司,查看客户信息-全国")]
        public PagedQueryResult<ParentModel, ParentModelCollection> GetPagedParents(ParentsSearchQueryCriteriaModel criteria)
        {
            return ParentDataSource.Instance.LoadParents(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/addparent

        [HttpPost]
        [PPTSJobFunctionAuthorize("添加孩子家长")]
        public void AddParent(CreatablePortentialCustomerModel criteria)
        {
            AddParentExecutor executor = new AddParentExecutor(criteria);
            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/createparent

        [HttpGet]
        [PPTSJobFunctionAuthorize("添加孩子家长")]
        public EditableParentModel CreateParent(string id)
        {
            EditableParentModel result = new EditableParentModel();
            result.Customer = GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.Load(id);
            result.Parent = new ParentModel { ParentID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard };
            result.CustomerParentRelation = new CustomerParentRelation() { CustomerID = result.Customer.CustomerID, ParentID = result.Parent.ParentID };
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(EditableParentModel), typeof(Parent));
            return result;
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("添加孩子家长")]
        public void CreateParent(EditableParentModel model)
        {
            EditableParentExecutor executor = new EditableParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/assertaccountcharge

        [HttpGet]
        public AssertResult AssertAccountCharge(string customerID)
        {
            return ChargeApplyResult.Validate(customerID, DeluxeIdentity.CurrentUser);
        }
        #endregion

        #region api/potentialcustomers/transfercustomers

        [HttpPost]
        public CreatableCustomerTransferResourceModel GetTransferResources(string[] customerIds)
        {
            return new CreatableCustomerTransferResourceModel(customerIds);
        }

        [HttpPost]
        public void TransferCustomers(CreatableCustomerTransferResourceModel model)
        {
            AddCustomerTranferResourceExecutor executor = new AddCustomerTranferResourceExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/createCustomerStaffRelations

        /// <summary>
        /// 分配咨询师/学管师/坐席/市场专员
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:分配咨询师,分配咨询师-本校区,分配咨询师-本部门,分配坐席-本部门,分配市场专员-本校区,分配学管师-本校区")]
        public void CreateCustomerStaffRelations(EditCustomerStaffRelationsModel model)
        {
            model.CustomerStaffRelations.NullCheck("model");
            model.InitCustomerStaffRelation();
            EditCustomerStaffRelationsExecutor executor = new EditCustomerStaffRelationsExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/transferCustomerTransferResources
        /// <summary>
        /// 划转资源
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:划转资源,划转资源-本部门,划转资源-本校区,划转资源-本分公司,划转资源-全国")]
        public void TransferCustomerTransferResources(EditCustomerTransferResourcesModel model)
        {
            EditCustomerTransferResourcesExecutor executor = new EditCustomerTransferResourcesExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/importexcel

        [HttpPost]
        public void ImportCustomers(HttpRequestMessage request)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];
            CustomerImportModel.ImportCustomersHandler(context, request);
        }

        #endregion 

        #region api/potentialcustomers/getimporthistory

        [HttpPost]
        public CustomerImportHistoryQueryResult GetImportHistory(CustomerImportHistoryCriteriaModel criteria)
        {
            return new CustomerImportHistoryQueryResult
            {
                QueryResult = CustomerImportHistoryDataSource.Instance.LoadImportCustomerHistory(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }

        #endregion 

        #region api/potentialcustomers/exportallcustomers

        [HttpPost]
        public HttpResponseMessage Exportallcustomers([ModelBinder(typeof(FormBinder))] PotentialCustomerQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 1;
            criteria.PageParams.PageSize = 5000;
            PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> queryResult = PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy);

            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("潜客管理");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前年级", typeof(string))) { PropertyName = "Grade" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("信息来源", typeof(string))) { PropertyName = "SourceMainType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("归属地", typeof(string))) { PropertyName = "OrgName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("建档日期", typeof(string))) { PropertyName = "CreateTime" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("建档人", typeof(string))) { PropertyName = "CreatorName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("建档人岗位", typeof(string))) { PropertyName = "CreateJobName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("归属咨询师", typeof(string))) { PropertyName = "ConsultantStaff" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("跟进次数", typeof(string))) { PropertyName = "FollowedCount" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("最后一次跟进时间", typeof(string))) { PropertyName = "FollowTime" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("归属市场专员", typeof(string))) { PropertyName = "MarketStaff" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("购买意原", typeof(string))) { PropertyName = "PurchaseIntention" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("跟进阶段", typeof(string))) { PropertyName = "FollowStage" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("客户级别", typeof(string))) { PropertyName = "CustomerLevel" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("下次沟通时间", typeof(string))) { PropertyName = "NextFollowTime" });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer));
            sheet.LoadFromCollection(queryResult.PagedData, tableDesp, (cell, param) =>
            {
                if (param.PropertyValue != null && !string.IsNullOrEmpty(param.PropertyValue.ToString()))
                {
                    switch (param.ColumnDescription.PropertyName)
                    {
                        case "Grade":
                            var g = dictionaries["C_CODE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
                            break;
                        case "SourceMainType":
                            var sourceMainType = dictionaries["C_CODE_ABBR_BO_Customer_Source"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != sourceMainType ? (null == sourceMainType.FirstOrDefault() ? null : sourceMainType.FirstOrDefault().Value) : null;
                            break;
                        case "FollowStage":
                            var sollowStage = dictionaries["C_CODE_ABBR_Customer_CRM_SalePhase"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != sollowStage ? (null == sollowStage.FirstOrDefault() ? null : sollowStage.FirstOrDefault().Value) : null;
                            break;
                        case "CustomerLevel":
                            var customerLevel = dictionaries["C_CODE_ABBR_Customer_CRM_CustomerLevelEx"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != customerLevel ? (null == customerLevel.FirstOrDefault() ? null : customerLevel.FirstOrDefault().Value) : null;
                            break;
                        case "NextFollowTime":
                        case "FollowTime":
                            cell.Value = Convert.ToDateTime(param.PropertyValue) == DateTime.MinValue ? "" : param.PropertyValue;
                            break;
                        default:
                            cell.Value = param.PropertyValue;
                            break;
                    }
                }
            });

            return wb.ToResponseMessage(string.Format("潜客管理_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
        }

        #endregion 
    }
}