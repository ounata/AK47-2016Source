using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    ///初始化新增排课的基类
    public class AssignBaseModel
    {
        /// 已经有的排课条件
        public AssignConditionCollection AssignCondition { get; set;}
        ///视图返回的下行模型
        public AssignSuperModel Assign { get; set; }

        public AssignBaseModel()
        {
            Assign = new AssignSuperModel();
            Assign.CopyAllowed = true;
        }
    }
}