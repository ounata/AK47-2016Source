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
	/// This object represents the properties and methods of a ShoppingCart.
	/// 购物车表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.ShoppingCarts")]
    [DataContract]
	public class ShoppingCart
	{		
		public ShoppingCart()
		{
		}		

		/// <summary>
		/// 购物车ID
		/// </summary>
		[ORFieldMapping("CartID", PrimaryKey=true)]
        [DataMember]
		public string CartID
		{
			get;
            set;
		}

        /// <summary>
        /// 订购类型（参考订购表）
        /// </summary>
        [ORFieldMapping("OrderType")]
        [DataMember]
        public int OrderType { set; get; }

        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
		public string CustomerID
		{
			get;
            set;
		}

		/// <summary>
		/// 开课校区ID
		/// </summary>
		[ORFieldMapping("ProductCampusID")]
        [DataMember]
		public string ProductCampusID
        {
			get;
            set;
		}

		/// <summary>
		/// 产品ID
		/// </summary>
		[ORFieldMapping("ProductID")]
        [DataMember]
		public string ProductID
		{
			get;
            set;
		}

		/// <summary>
		/// 班级ID（针对插班订购）
		/// </summary>
		[ORFieldMapping("ClassID")]
        [DataMember]
		public string ClassID
		{
			get;
            set;
		}

		/// <summary>
		/// 订购数量
		/// </summary>
		[ORFieldMapping("Amount")]
        [DataMember]
		public decimal Amount
		{
			get;
            set;
		}

		/// <summary>
		/// 创建人ID
		/// </summary>
		[ORFieldMapping("CreatorID")]
        [DataMember]
		public string CreatorID
		{
			get;
            set;
		}

		/// <summary>
		/// 创建人姓名
		/// </summary>
		[ORFieldMapping("CreatorName")]
        [DataMember]
		public string CreatorName
		{
			get;
            set;
		}

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class ShoppingCartCollection : EditableDataObjectCollectionBase<ShoppingCart>
    {
    }
}