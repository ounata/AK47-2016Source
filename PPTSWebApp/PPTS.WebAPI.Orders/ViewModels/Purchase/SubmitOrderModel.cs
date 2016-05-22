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
        /// 提交清单类型 1:常规 2:买赠 3:插班
        /// </summary>
        public int ListType { set; get; }

        /// <summary>
        /// 关联缴费申请单ID
        /// </summary>
        public string ChargeApplyID { set; get; }

        public string CustomerID { set; get; }
        public string AccountId { set; get; }

        /// <summary>
        /// 特殊折扣类型代码
        /// </summary>
        public string SpecialType { set; get; }

        /// <summary>
        /// 特殊折扣说明
        /// </summary>
        public string SpecialMemo { set; get; }
        public string CustomerCampusID { set; get; }

        public List<OrderItemViewModel> item { set; get; }



        #region 

        public string CreatorID { set; get; }
        public string CreatorName { set; get; }

        public string JobId { set; get; }
        public string JobName { set; get; }
        public string JobType { set; get; }

        #endregion

        #region
        public Data.Customers.Entities.Account Account { set; get; }
        public List<Data.Products.Entities.ProductView> ProductViews { set; get; }

        #endregion

        private Order order = null;
        public Order ToOrder()
        {
            if (order == null)
            {
                var parentInfo = Service.CustomerService.GetPrimaryParentByCustomerId(CustomerID);
                var customerInfo = Service.CustomerService.GetCustomerByCustomerId(CustomerID);

                order = new Order()
                {
                    OrderID = Guid.NewGuid().ToString(),
                    CreatorID = CreatorID,
                    CreatorName = CreatorName,

                    SubmitterID = CreatorID,
                    SubmitterName = CreatorName,
                    SubmitterJobName = JobName,
                    SubmitterJobID = JobId,
                    

                    CustomerID = CustomerID,
                    OrderStatus = OrderStatus.PendingApproval,
                    ChargeApplyID = ChargeApplyID,
                    SpecialType = SpecialType,
                    SpecialMemo = SpecialMemo,
                    AccountID = AccountId,
                    AccountCode = Account.AccountCode,
                    CustomerCode = customerInfo.CustomerCode,
                    CustomerGrade = customerInfo.Grade,
                    CustomerName = customerInfo.CustomerName,
                    ParentID = parentInfo!=null?parentInfo.ParentID:"",
                    ParentName = parentInfo != null ? parentInfo.ParentName : "",

                    OrderType = (OrderType)ListType,
                    
                    ProcessStatus="1",
                    CampusID = CustomerCampusID,
                    CampusName = "--",
                };
            }
            return order;
        }

        private OrderItemCollection orderItems = null;
        public OrderItemCollection ToOrderItemCollection()
        {
            if (orderItems == null)
            {
                var cartids = item.Select(m => m.CartID).ToArray();
                var carts = Data.Orders.Adapters.ShoppingCartAdapter.Instance.Load(cartids);

                var mapper = new AutoMapper.MapperConfiguration(c =>
                {
                    c.CreateMap<Data.Products.Entities.ProductView, Data.Orders.Entities.OrderItem>();
                }).CreateMapper();

                Service.Present preset = null;
                //买赠
                if (ListType == 2)
                {
                    preset = Service.ProductService.GetPresentByOrgId(CustomerCampusID);
                }

                orderItems = new Data.Orders.Entities.OrderItemCollection();

                foreach (var product in ProductViews)
                {
                    var submitItem = item.Single(m => m.ProductID == product.ProductID);
                    var oitem = mapper.Map<OrderItem>(product);

                    oitem.OrderPrice = product.ProductPrice;
                    oitem.OrderAmount = 1;
                    oitem.CategoryType = ((int)product.CategoryType).ToString();
                    oitem.DiscountType = ((int)DiscountTypeDefine.None).ToString();

                    oitem.ProductCampusID = carts.Single(s=>s.ProductID == product.ProductID).ProductCampusID;

                    if (product.CategoryType == CategoryType.CalssGroup)
                    {
                        oitem.OrderAmount = product.LessonCount;
                    }
                    else
                    {
                        //是否允许修改订购产品数量
                        if (product.CanInput == 1)
                        {
                            oitem.OrderAmount = submitItem.OrderAmount;
                        }
                    }

                    //是否允许使用 客户折扣
                    if (product.TunlandAllowed == 1)
                    {
                        oitem.DiscountRate = oitem.TunlandRate = Account.DiscountRate;
                        oitem.DiscountType = ((int)DiscountTypeDefine.Tunland).ToString();
                    }
                    //是否允许特殊折扣
                    if (product.SpecialAllowed == 1)
                    {
                        if(oitem.DiscountRate> submitItem.SpecialRate)
                        {
                            oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate;
                            oitem.DiscountType = ((int)DiscountTypeDefine.Special).ToString();
                        }
                        
                    }
                    
                    //oitem.RealPrice = oitem.OrderPrice * oitem.OrderAmount * oitem.DiscountRate;


                    //是否允许产品优惠金额 因 优惠金额 出现 该属性失效
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
                    }
                    oitem.PresentAmount = submitItem.PresentAmount;

                    oitem.RealPrice = oitem.OrderPrice * oitem.DiscountRate;
                    oitem.RealAmount = oitem.OrderAmount + oitem.PresentAmount;

                    orderItems.Add(oitem);
                }
            }
            return orderItems;
        }




        //public AssetCollection ToAssetCollection() { return null; }


    }


}