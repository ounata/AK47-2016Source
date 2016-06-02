using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Products.Adapters;
using PPTS.WebAPI.Products.Controllers;
using PPTS.WebAPI.Products.ViewModels.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Controllers.Tests
{
    [TestClass()]
    public class PresentControllerTests
    {
        [TestMethod()]
        public void GetPresentDetialTest()
        {
            string PresentID = "fb89d932-c4dc-b2e0-4eec-3752d47945ec";
            PresentDetialModel model = new PresentDetialModel()
            {
                Present = GenericDiscountAdapter<PresentViewModel, PresentCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).FirstOrDefault(),
                PresentItemCollection = GenericDiscountAdapter<PresentItemViewModel, PresentItemCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)),
                PresentPermissionCollection = GenericDiscountAdapter<PresentPermissionViewModel, PresentPermissionCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).AddCampusName().ConvertEndDate(PresentID),
                PresentPermissionsApplieCollection = GenericDiscountAdapter<PresentPermissionsApplyViewModel, PresentPermissionsApplieCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).AddCampusName()
            };
        }
    }
}