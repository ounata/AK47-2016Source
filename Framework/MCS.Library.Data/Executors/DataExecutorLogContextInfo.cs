using MCS.Library.Caching;
using MCS.Library.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Executors
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataExecutorLogContextInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static TextWriter Writer
        {
            get
            {
                object writer = null;

                if (ObjectContextCache.Instance.TryGetValue("ExecutorLogContextInfoWriter", out writer) == false)
                {
                    StringBuilder strB = new StringBuilder(1024);

                    writer = new StringWriter(strB);

                    ObjectContextCache.Instance["ExecutorLogContextInfoWriter"] = writer;
                }

                return (TextWriter)writer;
            }
        }

        /// <summary>
        /// 提交到真正的Logger
        /// </summary>
        public static void CommitInfoToLogger()
        {
            StringBuilder strB = ((StringWriter)Writer).GetStringBuilder();

            if (strB.Length > 0)
            {
                try
                {
                    Logger logger = LoggerFactory.Create("INExecutor");

                    if (logger != null)
                        logger.Write(strB.ToString(), LogPriority.Normal, 8008, TraceEventType.Information, "Executor上下文信息");
                }
                catch (System.Exception)
                {
                }
                finally
                {
                    strB.Clear();
                }
            }
        }
    }
}
