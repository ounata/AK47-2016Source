using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    /// <summary>
    /// 客户端的意见对象
    /// </summary>
    public class WfClientOpinion
    {
        public WfClientOpinion()
        {
        }

        public WfClientOpinion(GenericOpinion genericOpinion)
        {
            if (genericOpinion != null)
            {
                this.ID = genericOpinion.ID;
                this.Content = genericOpinion.Content;
                this.OpinionType = genericOpinion.OpinionType;
            }
        }

        public string ID
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public string OpinionType
        {
            get;
            set;
        }

        public GenericOpinion ToGenericOpinion()
        {
            GenericOpinion opinion = new GenericOpinion();

            opinion.ID = this.ID;
            opinion.Content = this.Content;
            opinion.OpinionType = this.OpinionType;

            return opinion;

        }
    }
}
