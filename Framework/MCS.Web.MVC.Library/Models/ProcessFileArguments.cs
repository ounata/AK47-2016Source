using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    public class ProcessFileArguments
    {
        /// <summary>
        /// 上传上来的Form内容
        /// </summary>
        public NameValueCollection FormData
        {
            get;
            internal set;
        }

        /// <summary>
        /// 上传上来的文件流
        /// </summary>
        public Stream UploadedStream
        {
            get;
            internal set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public ProcessFileResult ProcessResult
        {
            get;
            internal set;
        }

        public ProcessProgress Progress
        {
            get
            {
                return ProcessProgress.Current;
            }
        }
    }
}
