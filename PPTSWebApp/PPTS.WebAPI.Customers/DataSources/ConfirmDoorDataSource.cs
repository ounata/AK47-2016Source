using MCS.Library.Data;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerVerifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class ConfirmDoorDataSource : GenericCustomerDataSource<ConfirmDoorQueryModel, ConfirmDoorsQueryCollection>
    {
        public static readonly new ConfirmDoorDataSource Instance = new ConfirmDoorDataSource();

        public ConfirmDoorDataSource()
        {

        }

        protected override void OnAfterQuery(ConfirmDoorsQueryCollection result)
        {
            List<string> customerIDs = new List<string>();
            result.ForEach((model) => customerIDs.Add(model.CustomerID));
            CustomerParentPhoneCollection loaded = CustomerInfoQueryAdapter.Instance.LoadCustomerParentPhoneByIDs(customerIDs.ToArray());
            result.ForEach((model) => { MappingModel(loaded, model); });
        }

        private void MappingModel(CustomerParentPhoneCollection loaded, ConfirmDoorQueryModel model)
        {
            CustomerParentPhone customerParentPhone = loaded.Find(render => render.CustomerID == model.CustomerID);
            model.CustomerName = customerParentPhone.CustomerName;
            model.CustomerCode = customerParentPhone.CustomerCode;
            model.Grade = customerParentPhone.Grade;
            School schoolModel = SchoolAdapter.Instance.Load(customerParentPhone.SchoolID);
            if(schoolModel!=null)
            model.CampusName = schoolModel.SchoolName;  //  学员所在学校
            model.ParentName = customerParentPhone.ParentName;
        }

        public PagedQueryResult<ConfirmDoorQueryModel, ConfirmDoorsQueryCollection> LoadConfirmDoor(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " a.CustomerID,a.CreatorName,a.CampusName,a.CampusID,a.CreateTime,a.VerifyPeoples,a.VerifyRelations,c.CustomerSearchContent,c.ParentSearchContent,";
            select += "(select top 1 q.PlanVerifyTime from CustomerFollows q where a.CustomerID = q.CustomerID order by q.CreateTime desc)planTime,";
            select += "(select top 1 StaffName from CustomerStaffRelations_Current aa where aa.CustomerID = a.CustomerID and aa.[RelationType] = 1 order by aa.CreateTime desc)StaffName";
            string from = "";
            from += " CM.[CustomerVerifies] a inner join CM.[PotentialCustomersFulltext] c on a.CustomerID = c.OwnerID ";
            PagedQueryResult<ConfirmDoorQueryModel, ConfirmDoorsQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }


    }
}
