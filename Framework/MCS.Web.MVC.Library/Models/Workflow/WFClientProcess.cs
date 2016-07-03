using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientProcess
    {
        public string ProcessID
        {
            get;
            set;
        }

        public string ProcessStatus
        {
            get;
            set;
        }

        public string ProcessStatusString
        {
            get;
            set;
        }

        public string ResourceID
        {
            get;
            set;
        }

        public List<WfClientActivity> Activities
        {
            get;
            set;
        }

        public WfClientActivity CurrentActivity
        {
            get;
            set;
        }

        public List<WfClientActivityHistory> ActivityHistories
        {
            get;
            set;
        }

        public Dictionary<string, object> ProcessParameters
        {
            get;
            set;
        }

        public WfClientOpinion CurrentOpinion
        {
            get;
            set;
        }

        /// <summary>
        /// UI开关
        /// </summary>
        public WfClientUISwitches UISwitches
        {
            get;
            set;
        }

        public WfClientProcess()
        {
            this.Activities = new List<WfClientActivity>();
            this.ActivityHistories = new List<WfClientActivityHistory>();
            this.ProcessParameters = new Dictionary<string, object>();
            this.UISwitches = new WfClientUISwitches();
        }
    }
}
