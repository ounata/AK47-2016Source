using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Asset.
    /// 订购资产表（需要快照）
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.Assets_Current")]
    [DataContract]
    public class AssetView : Asset
    {

    }

    [Serializable]
    [DataContract]
    public class AssetViewCollection : EditableDataObjectCollectionBase<AssetView>
    {
    }
}