using MCS.Library.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Executors.Tests
{
    [TestClass()]
    public class CreatePresentExecutorTests
    {
        [TestMethod()]
        public void CreatePresentExecutorTest()
        {
            MCS.Library.Principal.DeluxeIdentity di = new MCS.Library.Principal.DeluxeIdentity(OGUExtensions.GetUserByOAName("nizhi"));
            DeluxePrincipal dp = new DeluxePrincipal(di);
            Thread.CurrentPrincipal = dp;

            CreatePresentModel model = new CreatePresentModel();

            model.Present = new Data.Products.Entities.Present() { StartDate = new DateTime(2016,5,30) };

            model.PresentItemCollection = new Data.Products.Entities.PresentItemCollection();
            model.PresentItemCollection.Add(new Data.Products.Entities.PresentItem() { PresentStandard=1, PresentValue=0.9M });
            model.PresentItemCollection.Add(new Data.Products.Entities.PresentItem() { PresentStandard = 2, PresentValue = 0.8M });
            model.PresentItemCollection.Add(new Data.Products.Entities.PresentItem() { PresentStandard = 3, PresentValue = 0.7M });

            model.PresentPermissionsApplieCollection = new Data.Products.Entities.PresentPermissionsApplieCollection();
            model.PresentPermissionsApplieCollection.Add(new Data.Products.Entities.PresentPermissionsApply() { CampusID= "18-Org" });
            model.PresentPermissionsApplieCollection.Add(new Data.Products.Entities.PresentPermissionsApply() { CampusID = "2107-Org" });
            model.PresentPermissionsApplieCollection.Add(new Data.Products.Entities.PresentPermissionsApply() { CampusID = "2108-Org" });

            CreatePresentExecutor executor = new CreatePresentExecutor(model);
            executor.Execute();
        }
    }
}