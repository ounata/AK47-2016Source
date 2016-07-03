using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Library.Validation;
using PPTS.Data.Common;
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
using PPTS.Data.Common.Security;

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

        [ObjectValidator]
        public Order Order { private set; get; }
        [ObjectValidator]
        public OrderItem Item { private set; get; }
        [ObjectValidator]
        private Asset Asset { set; get; }


        private OrderItemView orderItemView { set; get; }

        public ExchangeOrderModel FillOrder()
        {

            Order.IsNull(() =>
            {
                var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<OrderItemView, Order>(); }).CreateMapper();
                LoadItemView();

                Order = mapper.Map<Order>(orderItemView);
                Order.OrderID = UuidHelper.NewUuidString();
                Order.OrderNo = Helper.GetOrderCode("NOD");
                Order.OrderType = (OrderType)(Type + 3);
            });

            return this;
        }

        public ExchangeOrderModel FillOrderItem()
        {
            Item.IsNull(() =>
            {
                LoadItemView();
                LoadAsset();

                var productViews = Service.ProductService.GetProductsByIds(ProductId, orderItemView.ProductID);
                productViews.Any(s => s.CategoryType != CategoryType.OneToOne).TrueThrow("只允许对1对1产品进行资产兑换!");


                var productView = productViews.SingleOrDefault(w => w.ProductID == ProductId);
                var asset = AssetViewAdapter.Instance.Load(ItemId);

                var mapper = new AutoMapper.MapperConfiguration(c =>
                {
                    c.CreateMap<OrderItemView, OrderItem>();
                    c.CreateMap<ProductView, OrderItem>();
                }).CreateMapper();

                Item = mapper.Map<OrderItem>(productView);


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
                Item.ItemID = UuidHelper.NewUuidString();
                Item.ItemNo = Order.OrderNo+"1";

                Item.RelatedAssetID = Asset.AssetID;
                Item.RelatedAssetCode = Asset.AssetCode;

            });

            return this;
        }


        public Asset ToExchangeAsset()
        {
            LoadAsset();

            var asset = (Asset)Asset.CloneObject();

            asset.Amount = 0;
            asset.ExchangedAmount = Asset.Amount;

            FillUser(asset);

            return Asset;
        }

        public Asset ToAsset()
        {
            LoadAsset();

            var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<OrderItemView, Asset>(); }).CreateMapper();

            var asset = (Asset)Asset.CloneObject();

            mapper.Map(orderItemView, asset, orderItemView.GetType(), asset.GetType());

            asset.ProductID = Item.ProductID;

            asset.Amount = Item.RealAmount;
            asset.Price = Item.RealPrice;

            asset.AssetRefID = Item.ItemID;
            asset.AssetRefPID = Item.OrderID;

            asset.AssetID = UuidHelper.NewUuidString();
            asset.AssetCode = Item.ItemNo;
            
            asset.VersionStartTime = DateTime.MinValue;
            asset.VersionEndTime = DateTime.MinValue;

            FillUser(asset);

            return asset;
        }

        public void Validate() { }

        private void FillUser(object info)
        {
            if (DeluxeIdentity.CurrentUser != null)
            {
                if (info is IEntityWithCreator)
                {
                    DeluxeIdentity.CurrentUser.FillCreatorInfo(info as IEntityWithCreator);
                }
                if (info is IEntityWithModifier)
                {
                    DeluxeIdentity.CurrentUser.FillModifierInfo(info as IEntityWithModifier);
                }
                
            }

        }

        private void LoadAsset()
        {
            if (Asset == null)
            {
                Asset = AssetAdapter.Instance.LoadByItemId(ItemId);
            }
        }

        private void LoadItemView()
        {
            if (orderItemView == null)
            {
                orderItemView = OrderItemViewAdapter.Instance.Load(ItemId);
            }
        }

    }

}