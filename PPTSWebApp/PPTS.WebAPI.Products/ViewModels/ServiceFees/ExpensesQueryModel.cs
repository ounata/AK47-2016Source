using MCS.Library.Data.DataObjects;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.ServiceFees
{
    /// <summary>
    /// 综合服务费QueryModel
    /// </summary>
    [Serializable]
    public class ExpensesQueryModel: Expense
    {

    }
    [Serializable]
    public class ExpensesQueryCollection : EditableDataObjectCollectionBase<ExpensesQueryModel>
    {

    }
}