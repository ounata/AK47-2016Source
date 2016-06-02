using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Web.API.Controllers;
using MCS.Web.API.Models;
using MCS.Web.MVC.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using APITest = MCS.Web.API.Test;

namespace MCS.Library.SOA.SysTasks.Test
{
    [TestClass]
    public class MaterialTest
    {
        [TestMethod]
        public void UploadMaterialTest()
        {
            MaterialModel material = PrepareMaterial();

            PrepareMaterialContent(material.ID);

            MaterialModelCollection materials = new MaterialModelCollection();

            materials.Add(material);

            MaterialModelHelper helper = MaterialModelHelper.GetInstance(APITest.DataHelper.BizConnectionName);

            helper.Update(materials);

            Material loaded = helper.LoadByResourceID(material.ResourceID).FirstOrDefault();

            Assert.IsNotNull(loaded);

            MaterialContent content = MaterialContentAdapter.Instance.Load(loaded.ID);

            Assert.IsNotNull(content);
        }

        [TestMethod]
        public void DownloadNewMaterialTest()
        {
            MaterialModel material = PrepareMaterial();

            PrepareMaterialContent(material.ID);

            SampleController controller = PrepareController();

            HttpResponseMessage message = controller.DownloadMaterial(material);

            Assert.AreEqual("image/jpeg", message.Content.Headers.ContentType);
        }

        private static MaterialModel PrepareMaterial()
        {
            MaterialUploadModel uploadModel = PrepareUploadModel();

            return uploadModel.GenerateMaterial();
        }

        private static MaterialUploadModel PrepareUploadModel()
        {
            MaterialUploadModel model = new MaterialUploadModel();

            model.OriginalName = "冷清秋.jpg";
            model.ResourceID = UuidHelper.NewUuidString();
            model.RootPathName = MaterialUploadModel.DefaultRootPathName;

            return model;
        }

        private static void PrepareMaterialContent(string materialID)
        {
            FileInfo fileInfo = GetTempUploadFilePath(materialID, "GenericProcess", "冷清秋.jpg");

            IMaterialContentPersistManager prerpersistManager = GetMaterialContentPersistManager(materialID, fileInfo);

            using (Stream stream = ResourceHelper.GetResourceStream(Assembly.GetExecutingAssembly(), "MCS.Web.API.Test.Attachments.冷清秋.jpg"))
            {
                prerpersistManager.SaveTempMaterialContent(materialID, stream);
            }
        }

        private static IMaterialContentPersistManager GetMaterialContentPersistManager(string contentID, FileInfo destFile)
        {
            IMaterialContentPersistManager persistManager = MaterialContentSettings.GetConfig().PersistManager;

            persistManager.DestFileInfo = destFile;

            return persistManager;
        }

        private static FileInfo GetTempUploadFilePath(string materialID, string rootPathName, string originalFileName)
        {
            string fileName = materialID + Path.GetExtension(originalFileName);

            fileName = Path.Combine(GetUploadRootPath(rootPathName), "Temp", fileName);

            return new FileInfo(fileName);
        }

        private static string GetUploadRootPath(string rootPathName)
        {
            ExceptionHelper.CheckStringIsNullOrEmpty(rootPathName, "rootPathName");

            AppPathSettingsElement elem = AppPathConfigSettings.GetConfig().Paths[rootPathName];

            ExceptionHelper.FalseThrow(elem != null, "不能在配置节appPathSettings下找到名称为\"{0}\"的路径定义", rootPathName);

            return elem.Dir;
        }

        /// <summary>
        /// 检查上传路径是否存在，如果不存在则创建该路径
        /// </summary>
        private static void AutoCreateUploadPath(string uploadRootPath)
        {
            ExceptionHelper.CheckStringIsNullOrEmpty(uploadRootPath, "uploadRootPath");

            if (Directory.Exists(uploadRootPath) == false)
                Directory.CreateDirectory(uploadRootPath);

            string uploadTempPath = uploadRootPath + @"Temp\";

            if (Directory.Exists(uploadTempPath) == false)
                Directory.CreateDirectory(uploadTempPath);
        }

        private static SampleController PrepareController()
        {
            return new SampleController();
        }
    }
}
