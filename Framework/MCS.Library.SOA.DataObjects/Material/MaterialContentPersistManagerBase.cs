﻿using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects
{
    public abstract class MaterialContentPersistManagerBase : IMaterialContentPersistManager
    {
        private FileInfo _SourceFileInfo;
        private FileInfo _DestFileInfo;
        private bool _CheckSourceFileExists = true;
        private string _PathRoot = string.Empty;

        public virtual string PathRoot
        {
            get { return this._PathRoot; }
            set { this._PathRoot = value; }
        }

        public virtual bool CheckSourceFileExists
        {
            get { return this._CheckSourceFileExists; }
            set { this._CheckSourceFileExists = value; }
        }

        public virtual FileInfo SourceFileInfo
        {
            get { return this._SourceFileInfo; }
            set { this._SourceFileInfo = value; }
        }

        public virtual FileInfo DestFileInfo
        {
            get { return this._DestFileInfo; }
            set { this._DestFileInfo = value; }
        }


        public abstract void SaveMaterialContent(MaterialContent content);

        public virtual Stream GetMaterialContent(string contentID)
        {
            this.DestFileInfo.NullCheck("DestFileInfo");

            ExceptionHelper.FalseThrow<FileNotFoundException>(File.Exists(DestFileInfo.FullName), Resources.FileNotFound, DestFileInfo.FullName);

            return this.DestFileInfo.OpenRead();
        }

        public abstract void DeleteMaterialContent(MaterialContent content);

        public abstract bool ExistsContent(string contentID);

        /// <summary>
        /// 保存临时文件内容
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="contentStream"></param>
        public virtual void SaveTempMaterialContent(string materialID, Stream contentStream)
        {
            this.DestFileInfo.NullCheck("DestFileInfo");

            using (FileStream fs = new FileStream(this.DestFileInfo.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                contentStream.CopyTo(fs);
            }
        }

        protected static void DeleteFile(FileInfo file)
        {
            if (file != null && file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch (System.Exception)
                {
                }
            }
        }

        protected static void MoveFile(FileInfo sourceFile, FileInfo destFile)
        {
            if (destFile.Directory.Exists == false)
                destFile.Directory.Create();

            if (destFile.Exists)
                destFile.Delete();

            if (sourceFile.Exists)
                sourceFile.MoveTo(destFile.FullName);
        }

        protected static byte[] GetFileContent(FileInfo sourceFile)
        {
            using (MemoryStream stream = new MemoryStream(4096))
            {
                using (FileStream fs = sourceFile.OpenRead())
                {
                    fs.CopyTo(stream);
                }

                return stream.ToArray();
            }
        }
    }
}
