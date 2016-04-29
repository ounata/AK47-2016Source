using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class ClassSearchModel:Class
    {
    }

    public class ClassSearchModelCollection:  EditableDataObjectCollectionBase<ClassSearchModel>
    {

    }
}