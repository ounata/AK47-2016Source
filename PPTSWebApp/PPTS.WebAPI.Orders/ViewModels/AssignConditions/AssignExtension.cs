using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Orders.Entities;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Orders.ViewModels.AssignConditions
{
    [Serializable]
    [DataContract]
    public class AssignExtension : Assign
    {

        /// <summary>
        /// 排课条件ID
        /// </summary>
        [DataMember]
        public string ConditionID
        {
            get;
            set;
        }

        /// <summary>
        /// 排课条件名称（资产编码+科目+老师+年级）
        /// </summary>
        [DataMember]
        public string ConditionName
        {
            get;
            set;
        }


        /// <summary>
        /// 资产名称（资产编号+产品名称）
        /// </summary>
        [DataMember]
        public string AssetName
        {
            get;
            set;
        }


        /// <summary>
        /// 课程级别代码
        /// </summary>
        [DataMember]
        public string CourseLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 课程级别名称
        /// </summary>
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长代码
        /// </summary>
        [DataMember]
        public string LessonDuration
        {
            get;
            set;
        }
        /// <summary>
        /// 产品归属校区ID
        /// </summary>
        [DataMember]
        public string ProductCampusID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品归属校区名称
        /// </summary>
        [DataMember]
        public string ProductCampusName
        {
            get;
            set;
        } 


        /// <summary>
        /// 课次时长值
        /// </summary>
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }


        public IList<AssignExtension> CombineModel(AssetCollection ac, OrderItemCollection oic, OrderCollection oc)
        {
            IList<AssignExtension> result = new List<AssignExtension>();
            if (ac == null || ac.Count == 0)
                return result;
            //构造Asset扩展类型
            foreach (var v in ac)
            {
                OrderItem oiObj = null;
                if(oic!= null)
                    oiObj = oic.Where(p => p.ItemID == v.AssetRefID).FirstOrDefault();
                Order oObj = null;
                if (oc != null && oiObj != null)
                    oObj = oc.Where(p => p.OrderID == oiObj.OrderID).FirstOrDefault();
                result.Add(Combine(v, oiObj, oObj));
            }
            return result;
        }
        private AssignExtension Combine(Asset asset, OrderItem oi, Order order)
        {
            AssignExtension avm = new AssignExtension();
            avm.AssetID = asset.AssetID;
            avm.AssetCode = asset.AssetCode;
            avm.AssetName = asset.AssetName;          
            avm.CustomerID = asset.CustomerID;
            avm.Price = asset.Price;
            if (order != null)
            {
                avm.CustomerCode = order.CustomerCode;
                avm.CustomerName = order.CustomerName;
                avm.ConsultantID = order.ConsultantID;
                avm.ConsultantJobID = order.ConsultantJobID;
                avm.ConsultantName = order.ConsultantName;
                avm.EducatorID = order.EducatorID;
                avm.EducatorJobID = order.EducatorJobID;
                avm.EducatorName = order.EducatorName;
            }
            if (oi != null)
            {
                avm.Grade = oi.Grade;
                avm.GradeName = oi.GradeName;
                avm.Subject = oi.Subject;
                avm.SubjectName = oi.SubjectName;
                avm.CourseLevel = oi.CourseLevel;
                avm.CourseLevelName = oi.CourseLevelName;
                avm.LessonDuration = oi.LessonDuration;
                avm.LessonDurationValue = oi.LessonDurationValue;
                avm.ProductID = oi.ProductID;
                avm.ProductCode = oi.ProductCode;
                avm.ProductName = oi.ProductName;
                avm.ProductCampusID = oi.ProductCampusID;
                avm.ProductCampusName = oi.ProductCampusName;
            }
            return avm;
        }

    }
}