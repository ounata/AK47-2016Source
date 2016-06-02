using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 文件上传的Model信息。这些信息都应该绑定到控件上
    /// </summary>
    public class MaterialUploadModel
    {
        public const string DefaultRootPathName = "GenericProcess";

        public MaterialUploadModel()
        {
            this.RootPathName = DefaultRootPathName;
        }

        /// <summary>
        /// 附件的名称
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginalName
        {
            get;
            set;
        }

        /// <summary>
        /// 附件的序号
        /// </summary>
        public int SortID
        {
            get;
            set;
        }

        /// <summary>
        /// 附件所属的表单
        /// </summary>
        public string ResourceID
        {
            get;
            set;
        }

        /// <summary>
        /// 附件的类别
        /// </summary>
        public string MaterialClass
        {
            get;
            set;
        }

        /// <summary>
        /// 服务端根路径的配置名称
        /// </summary>
        public string RootPathName
        {
            get;
            set;
        }

        /// <summary>
        /// 附件的页数
        /// </summary>
        public int PageQuantity
        {
            get;
            set;
        }

        public MaterialModel GenerateMaterial()
        {
            MaterialModel material = new MaterialModel();

            material.ID = UuidHelper.NewUuidString();

            this.FillMaterial(material);

            return material;
        }

        public MaterialModel FillMaterial(MaterialModel material)
        {
            material.NullCheck("material");

            material.ResourceID = this.ResourceID;
            material.MaterialClass = this.MaterialClass;
            material.RelativeFilePath = GetRelativeFilePath(material.ID, this.OriginalName);
            material.SortID = this.SortID;

            if (this.Title.IsNullOrEmpty())
                material.Title = this.OriginalName;
            else
                material.Title = this.Title;

            material.OriginalName = this.OriginalName;
            material.LastUploadTag = UuidHelper.NewUuidString();
            material.Status = MaterualModelStatus.Inserted;

            if (DeluxePrincipal.IsAuthenticated)
            {
                material.Creator = DeluxeIdentity.CurrentUser;

                if (DeluxeIdentity.CurrentUser.Parent != null)
                    material.Department = DeluxeIdentity.CurrentUser.Parent;
            }
            return material;
        }

        internal static string GetRelativeFilePath(string materialID, string originalFileName)
        {
            return materialID + Path.GetExtension(originalFileName);
        }
    }
}
