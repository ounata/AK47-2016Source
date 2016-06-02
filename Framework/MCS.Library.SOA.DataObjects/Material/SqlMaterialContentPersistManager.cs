using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Properties;
using System.Transactions;
using MCS.Library.Data;

namespace MCS.Library.SOA.DataObjects
{
	public class SqlMaterialContentPersistManager : MaterialContentPersistManagerBase
	{
		public override void SaveMaterialContent(MaterialContent content)
		{
			if (content.ContentData == null)
				SaveMaterialContentFromFile(content);
			else
				SaveMaterialContentFromData(content);
		}

		private void SaveMaterialContentFromData(MaterialContent content)
		{
			content.FileSize = content.ContentData.Length;

			using (TransactionScope scope = TransactionScopeFactory.Create())
			{
				using (MemoryStream stream = new MemoryStream(content.ContentData))
				{
					MaterialContentAdapter.Instance.Update(content, stream);

					TransactionScopeFactory.AttachCommittedAction(new Action<TransactionEventArgs>(Current_TransactionCompleted), false);
				}

				scope.Complete();
			}
		}

		private void SaveMaterialContentFromFile(MaterialContent content)
		{
			SourceFileInfo.NullCheck("SourceFileInfo");

			if (CheckSourceFileExists)
				ExceptionHelper.FalseThrow(SourceFileInfo.Exists, string.Format(Resources.FileNotFound, SourceFileInfo.Name));

			if (SourceFileInfo.Exists)
			{
				content.FileSize = SourceFileInfo.Length;

				using (TransactionScope scope = TransactionScopeFactory.Create())
				{
					using (FileStream stream = SourceFileInfo.OpenRead())
					{
						MaterialContentAdapter.Instance.Update(content, stream);

						TransactionScopeFactory.AttachCommittedAction(new Action<TransactionEventArgs>(Current_TransactionCompleted), false);
					}

					scope.Complete();
				}
			}
		}

		private void Current_TransactionCompleted(TransactionEventArgs e)
		{
			DeleteFile(this.SourceFileInfo);
		}

		public override Stream GetMaterialContent(string contentID)
		{
			MaterialContent content = MaterialContentAdapter.Instance.Load(builder => builder.AppendItem("CONTENT_ID", contentID)).SingleOrDefault();
            Stream result = null;

            if (content != null)
            {
                if (content.ContentData == null)
                    content.ContentData = new byte[0];

                result = new MemoryStream(content.ContentData);
            }
            else
            {
                //数据库里如果没有附件，则从文件系统获取（基类实现）
                if (this.DestFileInfo != null)
                    result = base.GetMaterialContent(contentID);
                else
                    throw new SystemSupportException(string.Format("不能找到ContentID为'{0}'的附件内容", contentID));
            }

			return result;
		}

		public override bool ExistsContent(string contentID)
		{
			return MaterialContentAdapter.Instance.Exists(builder => builder.AppendItem("CONTENT_ID", contentID));
		}

		public override void DeleteMaterialContent(MaterialContent content)
		{
			MaterialContentAdapter.Instance.Delete(content);
		}
	}
}
