using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Library;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Providers;
using MCS.Web.Responsive.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MCS.Web.MVC.Library.ApiCore
{
    public static class ProcessHelper
    {
        public static void ProcessFileUpload(this HttpRequestMessage request, Func<ProcessFileArguments, bool> processor)
        {
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Write(new string(' ', 4096));
            HttpContext.Current.Response.Flush();

            ProcessProgress.Current.RegisterResponser(ScriptProgressResponser.Instance);

            ProcessFileResult result = new ProcessFileResult();

            try
            {
                ProcessProgress.Current.MinStep = 0;
                ProcessProgress.Current.CurrentStep = 0;
                ProcessProgress.Current.MaxStep = 100;

                request.Content.IsMimeMultipartContent("form-data").FalseThrow("上传请求的格式不正确");

                InMemoryMultipartFormDataStreamProvider provider =
                    request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider()).Result;

                bool needContinue = true;

                foreach (StreamContent fileContent in provider.Files)
                {
                    ProcessFileArguments args = new ProcessFileArguments()
                    {
                        FormData = provider.FormData,
                        UploadedStream = fileContent.ReadAsStreamAsync().Result,
                        ProcessResult = result
                    };

                    if (processor != null)
                        needContinue = processor(args);

                    if (needContinue == false)
                        break;
                }
            }
            catch (System.Exception ex)
            {
                result.Error = ex.Message;
            }
            finally
            {
                ScriptProgressResponser.Instance.ResponseResult("processComplete", result);
                HttpContext.Current.Response.End();
            }
        }

        public static HttpResponseMessage ProcessMaterialDownload(this MaterialModel material, string rootPathName = MaterialUploadModel.DefaultRootPathName)
        {
            material.NullCheck("material");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            FileInfo destFileInfo = null;

            if (rootPathName.IsNullOrEmpty())
                rootPathName = MaterialUploadModel.DefaultRootPathName;

            if (material.Status == MaterualModelStatus.Inserted)
                destFileInfo = GetTempUploadFilePath(material.ID, rootPathName, material.OriginalName);

            IMaterialContentPersistManager persistManager =
                        GetMaterialContentPersistManager(material.ID, destFileInfo);

            Stream stream = persistManager.GetMaterialContent(material.ID);
            
                result.Content = new StreamContent(stream);

                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(GetFileContentType(material.OriginalName));

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("Attachment")
                {
                    FileName = HttpContext.Current.EncodeFileNameByBrowser(material.OriginalName)
                };


           
            return result;

        }

        public static MaterialModelCollection ProcessMaterialUpload(this HttpRequestMessage request)
        {
            MaterialModelCollection materials = new MaterialModelCollection();

            request.Content.IsMimeMultipartContent("form-data").FalseThrow("上传请求的格式不正确");

            InMemoryMultipartFormDataStreamProvider provider =
                request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider()).Result;

            string mumJson = provider.FormData.GetValue("materialUploadModel", string.Empty);

            MaterialUploadModel uploadModel = null;

            if (mumJson.IsNotEmpty())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                uploadModel = JsonConvert.DeserializeObject<MaterialUploadModel>(mumJson);
            }
            else
                uploadModel = new MaterialUploadModel();

            foreach (StreamContent fileContent in provider.Files)
            {
                if (uploadModel.OriginalName.IsNullOrEmpty())
                {
                    ContentDispositionHeaderValue contentDisposition = fileContent.Headers.ContentDisposition;

                    if (contentDisposition != null)
                    {
                        string fileName = contentDisposition.FileName.Replace("\"", string.Empty);
                        uploadModel.OriginalName = Path.GetFileName(fileName);
                    }
                }

                MaterialModel material = uploadModel.GenerateMaterial();

               

                IMaterialContentPersistManager persistManager = GetMaterialContentPersistManager(material.ID,
                    GetTempUploadFilePath(material.ID, uploadModel.RootPathName, uploadModel.OriginalName));

                persistManager.SaveTempMaterialContent(material.ID, fileContent.ReadAsStreamAsync().Result);

                //write DB
                var content = UploadMaterialWriteDB(material);

                materials.Add(material);
            }

            return materials;
        }

        public static MaterialContent UploadMaterialWriteDB(MaterialModel model)
        {
            MaterialModel material = model;

            MaterialModelCollection materials = new MaterialModelCollection();

            materials.Add(material);

            MaterialModelHelper helper = MaterialModelHelper.GetInstance("DataAccessTest");

            helper.Update(materials);

            Material loaded = helper.LoadByResourceID(material.ResourceID).FirstOrDefault();



            MaterialContent content = MaterialContentAdapter.Instance.Load(loaded.ID);

            return content;
        }

        private static IMaterialContentPersistManager GetMaterialContentPersistManager(string contentID, FileInfo destFile)
        {
            IMaterialContentPersistManager persistManager = MaterialContentSettings.GetConfig().PersistManager;

            persistManager.DestFileInfo = destFile;

            return persistManager;
        }

        private static FileInfo GetTempUploadFilePath(string materialID, string rootPathName, string originalFileName)
        {
            string fileName = MaterialUploadModel.GetRelativeFilePath(materialID, Path.GetExtension(originalFileName));

            string uploadRootPath = GetUploadRootPath(rootPathName);

            AutoCreateUploadPath(uploadRootPath);

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

        /// <summary>
        /// 获得ContentType
        /// </summary>
        /// <param name="originalName">文件名</param>
        /// <returns></returns>
        private static string GetFileContentType(string originalName)
        {
            string contentType = "application/octet-stream";

            ContentTypeConfigElement elem = GetConfigElement(originalName);

            if (elem != null)
                contentType = elem.ContentType;

            return contentType;
        }

        /// <summary>
        /// 获得配置节点
        /// </summary>
        /// <param name="originalName">文件名</param>
        /// <returns></returns>
        private static ContentTypeConfigElement GetConfigElement(string originalName)
        {
            ContentTypeConfigElement elem =
                ContentTypesSection.GetConfig().ContentTypes.FindElementByFileName(originalName);

            if (elem != null)
                return elem;
            else
                return ContentTypesSection.GetConfig().DefaultElement;
        }
    }
}
