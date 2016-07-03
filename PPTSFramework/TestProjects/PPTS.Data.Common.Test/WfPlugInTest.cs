using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class WfPlugInTest
    {
        [TestMethod]
        public void StartProcessWithCampusRoleTest()
        {
            IWfProcessDescriptor proessDesp = ProcessHelper.CreateSimpleProcessDescriptor();

            proessDesp.Activities["NormalActivity"].Resources.Add(new WfDynamicResourceDescriptor("校咨询主任", "JobUsers(CampusID, \"校咨询主任\")"));

            IWfProcess process = proessDesp.StartupProcess(PrepareRuntimeParameters());

            process.OutputEveryActivities();

            Assert.IsTrue(process.Activities.FindActivityByDescriptorKey("NormalActivity").Candidates.Count > 0);
        }

        [TestMethod]
        public void StartProcessWithRegionRoleTest()
        {
            IWfProcessDescriptor proessDesp = ProcessHelper.CreateSimpleProcessDescriptor();

            proessDesp.Activities["NormalActivity"].Resources.Add(new WfDynamicResourceDescriptor("分总经理", "JobUsers(BranchID, \"分总经理\")"));

            IWfProcess process = proessDesp.StartupProcess(PrepareRuntimeParameters());

            process.OutputEveryActivities();

            Assert.IsTrue(process.Activities.FindActivityByDescriptorKey("NormalActivity").Candidates.Count > 0);
        }

        [TestMethod]
        public void StartProcessWithHQRoleTest()
        {
            IWfProcessDescriptor proessDesp = ProcessHelper.CreateSimpleProcessDescriptor();

            proessDesp.Activities["NormalActivity"].Resources.Add(new WfDynamicResourceDescriptor("总座席代表", "JobUsers(HQID, \"总座席代表\")"));

            IWfProcess process = proessDesp.StartupProcess(PrepareRuntimeParameters());

            process.OutputEveryActivities();

            Assert.IsTrue(process.Activities.FindActivityByDescriptorKey("NormalActivity").Candidates.Count > 0);
        }

        private static Dictionary<string, object> PrepareRuntimeParameters()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob job = GetJobByDepartmentType(user, DepartmentType.Campus);

            return job.FillRuntimeParameters(new Dictionary<string, object>());
        }

        private static PPTSJob GetJobByDepartmentType(IUser user, DepartmentType deptType)
        {
            PPTSJob result = null;

            foreach (PPTSJob job in user.Jobs())
            {
                if (job.GetParentOrganizationByType(deptType) != null)
                {
                    result = job;
                    break;
                }
            }

            return result;
        }
    }
}
