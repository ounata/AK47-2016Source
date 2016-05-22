using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    public class ConfirmDoorCreateModel
    {
        [ObjectValidator]
        public CustomerVerify ConfirmDoor
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        /// <summary>
        /// 实际上门人数
        /// </summary>
        [NoMapping]
        [ConstantCategory("c_codE_ABBR_Customer_CRM_RealCallPersonNum")]
        public int VerifyPeople
        {
            get;
            set;
        }

        /// <summary>
        /// 上门人员关系
        /// </summary>
        [NoMapping]
        [ConstantCategory("c_codE_ABBR_RealCallPersonRelation")]
        public int VerifyRelation
        {
            get;
            set;
        }

        public static ConfirmDoorCreateModel CreateConfirmDoor()
        {
            ConfirmDoorCreateModel model = new ConfirmDoorCreateModel();

            model.ConfirmDoor = new CustomerVerify { VerifyID = UuidHelper.NewUuidString(), CreateTime = DateTime.Now, VerifyTime = DateTime.Now };
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVerify), typeof(ConfirmDoorCreateModel));
            return model;
        }
    }
}
