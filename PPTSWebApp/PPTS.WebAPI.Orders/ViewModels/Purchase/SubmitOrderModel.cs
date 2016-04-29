using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{

    public class SubmitOrderModel
    {
        /// <summary>
        /// 提交清单类型 1:常规 2:买赠
        /// </summary>
        public int ListType { set; get; }

        /// <summary>
        /// 关联缴费申请单ID
        /// </summary>
        public string ChargeApplyID { set; get; }

        public string CustomerID { set; get; }
        public string AccountId { set; get; }

        public List<OrderItem> item { set; get; }



        #region 

        public string CreatorID { set; get; }
        public string CreatorName { set; get; }
        public string CustomerCampusID { set; get; }
        public string ProductCampusID { set; get; }
        public string ProductCampusName { set; get; }
        public string OrgID { set; get; }

        #endregion

        #region

        public Service.Account Account { set; get; }
        public List<Data.Products.Entities.ProductView> ProductViews { set; get; }

        #endregion

        private Order order = null;
        public Order ToOrder()
        {
            if (order == null)
            {
                order = new Order()
                {
                    OrderID = Guid.NewGuid().ToString(),
                    CreatorID = CreatorID,
                    CreatorName = CreatorName,
                    CustomerID = CustomerID,
                    OrderStatus = OrderStatus.PendingApproval,
                    ChargeApplyID = ChargeApplyID,
                };
            }
            return order;
        }

        private OrderItemCollection orderItems = null;
        public OrderItemCollection ToOrderItemCollection()
        {
            if (orderItems == null)
            {
                Service.Present preset = null;
                //买赠
                if (ListType == 2)
                {
                    preset = Service.ProductService.GetPresentByOrgId(OrgID);
                }

                orderItems = new Data.Orders.Entities.OrderItemCollection();

                foreach (var product in ProductViews)
                {
                    var submitItem = item.Single(m => m.ProductID == product.ProductID);
                    var oitem = new Data.Orders.Entities.OrderItem()
                    {
                        OrderPrice = product.ProductPrice,
                        OrderAmount = 1,
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        Subject = product.Subject,
                        Grade = product.Grade,
                        Catalog = product.Catalog,
                        CatalogName = product.CatalogName,
                        CategoryType = ((int)product.CategoryType).ToString(),
                        CourseLevel = product.CourseLevel,
                        //CourseLevelName = 
                        DiscountType = ((int)DiscountTypeDefine.None).ToString(),

                    };

                    //是否允许修改订购产品数量
                    if (product.CanInput == 1)
                    {
                        oitem.OrderAmount = submitItem.OrderAmount;
                    }
                    //是否允许特殊折扣
                    if (product.SpecialAllowed == 1)
                    {
                        oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate;
                        oitem.DiscountType = ((int)DiscountTypeDefine.Special).ToString();
                    }
                    //是否允许使用 客户折扣
                    if (product.TunlandAllowed == 1)
                    {
                        oitem.DiscountRate = oitem.TunlandRate = Account.DiscountRate;
                        oitem.DiscountType = ((int)DiscountTypeDefine.Tunland).ToString();
                    }


                    ////是否允许产品优惠金额 因 优惠金额 出现 该属性失效
                    //if (product.PromotionAllowed == 1)
                    //{
                    //    oitem.RealPrice = submitItem.RealPrice;
                    //}
                    //else
                    //{
                    //    oitem.RealPrice = oitem.OrderPrice * oitem.OrderAmount * (oitem.SpecialRate < 1 ? oitem.SpecialRate : oitem.TunlandRate);
                    //}

                    //买赠
                    if (preset != null)
                    {
                        var presetitem = preset.Items.FirstOrDefault(s => oitem.OrderAmount >= s.PresentStandard);
                        oitem.PresentID = preset.PresentID;
                        oitem.PresentQuato = presetitem == null ? 0 : presetitem.PresentStandard;
                        oitem.PresentAmount = submitItem.PresentAmount;
                    }

                    orderItems.Add(oitem);
                }
            }
            return orderItems;
        }




        //public AssetCollection ToAssetCollection() { return null; }


    }


}