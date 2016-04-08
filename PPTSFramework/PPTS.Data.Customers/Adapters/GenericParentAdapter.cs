using MCS.Library.Core;
using MCS.Library.Data;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericParentAdapter<T, TCollection> : CustomerAdapterBase<T, TCollection>
        where T : Parent
        where TCollection : IList<T>, new()
    {
        public static readonly GenericParentAdapter<T, TCollection> Instance = new GenericParentAdapter<T, TCollection>();

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public Parent Load(string parentID)
        {
            return this.Load(builder => builder.AppendItem("ParentID", parentID)).SingleOrDefault();
        }

        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.ParentCode.IsNullOrEmpty())
                data.ParentCode = Helper.GetCustomerCode("P");
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
            if (data.ParentCode.IsNullOrEmpty())
                data.ParentCode = Helper.GetCustomerCode("P");
        }
    }
}
