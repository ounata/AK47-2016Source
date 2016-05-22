using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Common.Controllers;
using PPTS.WebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Common.Test
{
    [TestClass]
    public class MetadataControllerTest
    {
        [TestMethod]
        public void GetProvinceDataTest()
        {
            MetadataController controller = new MetadataController();
            QueryMetadataParams queryParams = new QueryMetadataParams()
            {
                Category = "C_CODE_ABBR_LOCATION",
                ParentKey = "0"
            };
            var items = controller.GetData(queryParams);
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            items.Output();
        }

        [TestMethod]
        public void GetCityDataTest()
        {
            MetadataController controller = new MetadataController();
            QueryMetadataParams queryParams = new QueryMetadataParams()
            {
                Category = "C_CODE_ABBR_LOCATION",
                ParentKey = "510000" //四川省
            };
            var items = controller.GetData(queryParams);
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            items.Output();
        }
    }
}
