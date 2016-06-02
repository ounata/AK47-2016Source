using MCS.Library.Core;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Exchange
{
    public class ExchangeAmountModel
    {
        public ExchangeAmountModel(string itemId, string productId)
        {
            OrderItem = OrderItemViewAdapter.Instance.Load(itemId);
            Product = Service.ProductService.GetProductsByIds(productId).SingleOrDefault();
            Asset = AssetViewAdapter.Instance.Load(itemId);
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView));
        }

        public OrderItemView OrderItem { set; get; }

        public ProductView Product { set; get; }

        public AssetView Asset { set; get; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }

    public class ExchangeOrderModel
    {
        public string ItemId { set; get; }
        public string ProductId { set; get; }


        /// <summary>
        /// 1:补差价 2:不补差价
        /// </summary>
        public int Type { set; get; }

        public Order Order { private set; get; }
        public OrderItem Item { private set; get; }

        private Asset Asset { set; get; }
        

        private OrderItemView orderItemView { set; get; }

        public Order ToOrder()
        {
            var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<OrderItemView, Order>(); }).CreateMapper();
            if (orderItemView == null)
            {
                orderItemView = OrderItemViewAdapter.Instance.Load(ItemId);
            }
            Order = mapper.Map<Order>(orderItemView);
            Order.OrderID = Guid.NewGuid().ToString();
            Order.OrderNo = null;
            Order.OrderType = (OrderType)(Type + 3);
            return Order;
        }

        public OrderItem ToOrderItem()
        {
            
            if (orderItemView == null)
            {
                orderItemView = OrderItemViewAdapter.Instance.Load(ItemId);
            }
            
            var productViews = Service.ProductService.GetProductsByIds(ProductId, orderItemView.ProductID);
            if(productViews.Any(s=>s.CategoryType != Data.Products.CategoryType.OneToOne))
            {
                ExceptionHelper.TrueThrow(productViews.Any(s => s.CategoryType != Data.Products.CategoryType.OneToOne), "只允许对1对1产品进行资产兑换");
            }

            var productView = productViews.SingleOrDefault(w => w.ProductID == ProductId);
            var asset = AssetViewAdapter.Instance.Load(ItemId);

            var mapper = new AutoMapper.MapperConfiguration(c =>
            {
                c.CreateMap<OrderItemView, OrderItem>();
                c.CreateMap<ProductView, OrderItem>();
            }).CreateMapper();

            Item = mapper.Map<OrderItem>(productView);



            //item.ProductID = ProductId;
            //item.ProductName = productView.ProductName;
            //item.ProductUnit = productView.ProductUnit;
            //item.Subject = productView.Subject;
            //item.LessonDuration = productView.LessonDuration;
            //item.Grade = productView.Grade;
            //item.CourseLevel = productView.CourseLevel;

            Item.CategoryType = ((int)productView.CategoryType).ToString();
            if (Type == 1)
            {
                //新产品折扣后单价
                var price = (asset.RealPrice / asset.OrderPrice) * productView.ProductPrice;
                //兑换新产品数量
                var count = asset.Amount * (asset.OrderPrice / price);
                Item.RealPrice = price;
                Item.RealAmount = count;
            }
            else if (Type == 2)
            {
                Item.RealPrice = asset.OrderPrice;
                Item.RealAmount = asset.Amount;
            }

            Item.OrderID = Order.OrderID;
            Item.ItemID = Guid.NewGuid().ToString() ;
            Item.ItemNo = null;
            return Item;
        }


        public Asset ToExchangeAsset() {
            if(Asset == null)
            {
                Asset = AssetAdapter.Instance.LoadByItemId(ItemId);
            }
            var asset = (Asset)Asset.CloneObject();
            asset.ExchangedAmount = Asset.Amount;
            return Asset;
        }

        public Asset ToAsset() {
            if (Asset == null)
            {
                Asset = AssetAdapter.Instance.LoadByItemId(ItemId);
            }

            var asset = (Asset)Asset.CloneObject();
            asset.ProductID = Item.ProductID;
            asset.Amount = Item.RealAmount;
            asset.AssetRefID = Item.ItemID;
            
            asset.AssetID = UuidHelper.NewUuidString();
            asset.AssetCode = null;

            return asset;
        }

        public void Validate() { }

    }

}