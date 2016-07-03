using MCS.Library.OGUPermission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PPTS.Data.Orders.Test
{
    [TestClass]
    public class OrderScopeAuthorizationTest
    {
        [TestMethod]
        public void UpdateCourseAuthTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCourseInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Assign>.GetInstance(ConnectionDefine.PPTSOrderConnectionName).UpdateAuth(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                CourseRelationAuthorizationCollection ras = CourseRelationAuthorizationAdpter.GetInstance(ConnectionDefine.PPTSOrderConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                CourseOrgAuthorizationCollection oas = CourseOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateRecordAuthTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordAssetInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Assign>.GetInstance(ConnectionDefine.PPTSOrderConnectionName).UpdateAuth(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                OwnerRelationAuthorizationCollection ras = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                RecordOrgAuthorizationCollection oas = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }


        private IUser GetUserInfo()
        {
            return OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
        }

        private RecordInfo GetRecordAssetInfo()
        {
            return new RecordInfo()
            {
                RecordID = "4",
                RecordType = RecordType.Asset
            };
        }

        private RecordInfo GetRecordCourseInfo()
        {
            return new RecordInfo()
            {
                RecordID = "4",
                
                RecordType = RecordType.Assign
            };
        }
    }

    public class RecordInfo
    {
        public string RecordID { get; set; }

        public RecordType RecordType { get; set; }
    }
}
