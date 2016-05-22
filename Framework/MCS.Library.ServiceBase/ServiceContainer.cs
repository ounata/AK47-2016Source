using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.Services
{
    /// <summary>
    /// 服务容器
    /// </summary>
    public class ServiceContainer
    {
        private ServiceThreadCollection threads = new ServiceThreadCollection();
        private ManualResetEvent exitEvent = new ManualResetEvent(false);
        private ServiceStatusType serviceStatus = ServiceStatusType.Stopped;
        private ServiceLog log = null;
        private string serviceName = string.Empty;

        public ServiceContainer()
            : this("MCSServiceMain")
        {
        }

        public ServiceContainer(string serviceName)
        {
            this.serviceName = serviceName;
        }

        public event CreateThreadDelegete CreateThreadEvent;

        public ServiceStatusType ServiceStatus
        {
            get
            {
                return this.serviceStatus;
            }
        }

        public string ServiceName
        {
            get
            {
                return this.serviceName;
            }
        }

        public ServiceLog Log
        {
            get
            {
                if (this.log == null)
                    this.log = new ServiceLog(this.ServiceName);

                return this.log;
            }
        }

        public ServiceThreadCollection Threads
        {
            get
            {
                return this.threads;
            }
        }

        public ServiceContainer StarService()
        {
            this.exitEvent.Reset();

            OnStart();

            this.serviceStatus = ServiceStatusType.Running;

            return this;
        }

        public ServiceContainer StopService()
        {
            OnStop();

            this.serviceStatus = ServiceStatusType.Stopped;

            return this;
        }

        public void OnStart(params string[] args)
        {
            try
            {
                CreateAllThreads();

                this.Log.Write("服务启动", GetStartupInfo(), ServiceLogEventID.SERVICEMAIN_STARTUPINFO);

                this.threads.StartAllThreads();
            }
            catch (Exception ex)
            {
                this.Log.Write(ex, ServiceLogEventID.SERVICEMAIN_ONSTART);

                throw;
            }
        }

        public void OnStop()
        {
            this.exitEvent.Set();

            this.threads.AbortAllThreads();
            this.threads.Clear();

            if (ServiceArguments.Current.EntryType == ServiceEntryType.Service)
                this.Log.Write("服务停止", "服务停止", ServiceLogEventID.SERVICEMAIN_ONSTOP);
        }

        private void CreateAllThreads()
        {
            ThreadParamCollection threadParams = GetAllThreadParams();

            for (int i = 0; i < threadParams.Count; i++)
            {
                ThreadParam tp = threadParams[i];

                if (string.Compare(tp.OwnerServiceName, this.ServiceName, true) == 0)
                    AddThread(tp);
            }
        }

        private void AddThread(ThreadParam tp)
        {
            try
            {
                tp.ExitEvent = this.exitEvent;
                tp.EntryType = ServiceArguments.Current.EntryType;

                if (tp.ThreadTask != null)
                {
                    tp.ThreadTask.Params = tp;

                    this.threads.Add(ServiceThread.CreateThread(tp, CreateThreadEvent));
                }
            }
            catch (Exception ex)
            {
                this.Log.Write(string.Format("载入线程\"{0}\"出错", tp.Name), ex, ServiceLogEventID.SERVICEMAIN_ADDTHREAD);

                throw;
            }
        }

        private ThreadParamCollection GetAllThreadParams()
        {
            return ServiceMainSettings.GetConfig().ThreadParams;
        }

        private string GetStartupInfo()
        {
            StringBuilder strB = new StringBuilder(1024);

            StringWriter sw = new StringWriter(strB);

            try
            {
                sw.WriteLine("服务启动");
                sw.WriteLine("应用程序的根目录：{0}", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                foreach (ServiceThread thread in this.threads)
                    sw.WriteLine("\t Thread： {0}, Status： {1} ", thread.Params.Name, thread.Status);
            }
            finally
            {
                sw.Close();
            }

            return strB.ToString();
        }
    }
}
