using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    public class ProcessFileResult
    {
        public ProcessFileResult()
        {
            DataChanged = false;
            CloseWindow = true;
            ProcessLog = string.Empty;
        }

        public bool DataChanged
        {
            get;
            set;
        }

        public bool CloseWindow
        {
            get;
            set;
        }

        public string ProcessLog
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }
    }
}
