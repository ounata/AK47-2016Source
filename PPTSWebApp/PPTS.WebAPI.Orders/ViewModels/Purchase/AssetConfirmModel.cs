using MCS.Library.Validation;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Configuration;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common;
using PPTS.Contracts.Customers.Operations;
using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Orders.Service;
using PPTS.Contracts.Customers.Models;
using PPTS.Data.Customers;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class AssetConfirmModel : AssetConfirm
    {
        [DataMember]
        public string OrderID { set; get; }
        [DataMember]
        public string ItemID { set; get; }
        [DataMember]
        public decimal ConfirmedMoney { set; get; }
        [DataMember]
        public string ProductID { set; get; }


        #region Private

        

        private Order Order { set; get; }


        #endregion



        public Asset Asset { private set; get; }
        public AssetConfirmModel FillAsset()
        {
            if (null == Asset)
            {
                Asset = AssetAdapter.Instance.LoadByItemId(ItemID);
                Asset.IsNotNull(m =>
                {
                    Asset.ConfirmedMoney += ConfirmedMoney;
                    FillUser(Asset);
                });
            }
            return this;
        }

        public AssetConfirmModel FillAssetConfirm()
        {
            Order.IsNull(() => { Order = OrdersAdapter.Instance.Load(OrderID); });

            Asset.IsNotNull(asset => {

                Order.IsNotNull(model => {

                    var mapper = new AutoMapper.MapperConfiguration(c => {
                        c.CreateMap<Order, AssetConfirm>();
                        c.CreateMap<Asset, AssetConfirm>();
                    }).CreateMapper();

                    mapper.Map(Asset, this, Asset.GetType(), this.GetType());
                    mapper.Map(Order, this, Order.GetType(), this.GetType());


                    ConfirmID = UuidHelper.NewUuidString();

                    //AssetID = Asset.AssetID;
                    AccountID = Order.AccountID;

                    ConfirmMoney = ConfirmedMoney;

                    AssetType = AssetTypeDefine.NonCourse;
                    ConfirmFlag = ConfirmFlagDefine.Confirm;
                    ConfirmStatus = ConfirmStatusDefine.Confirmed;

                    ConsultantID = Order.ConsultantID;
                    ConsultantJobID = Order.ConsultantJobID;
                    ConsultantName = Order.ConsultantName;
                    EducatorID = Order.EducatorID;
                    EducatorJobID = Order.EducatorJobID;
                    EducatorName = Order.EducatorName;



                    FillUser(this);

                });

            });

            
            
            return this;
        }

        public MCS.Library.OGUPermission.IUser CurrentUser { set; get; }
        private void FillUser(object info)
        {
            if (CurrentUser != null)
            {
                if (info is IEntityWithCreator)
                {
                    CurrentUser.FillCreatorInfo(info as IEntityWithCreator);
                }
                if (info is IEntityWithModifier)
                {
                    CurrentUser.FillModifierInfo(info as IEntityWithModifier);
                }

                if(info is AssetConfirmModel)
                {
                    var model = info as AssetConfirmModel;

                    model.ConfirmerID = CreatorID;
                    model.ConfirmerName = CreatorName;
                    model.ConfirmerJobID = CurrentUser.GetCurrentJob().ID;
                    model.ConfirmerJobName = CurrentUser.GetCurrentJob().JobName;
                    model.ConfirmerJobType = ((int)CurrentUser.GetCurrentJob().JobType).ToString();
                }

            }

        }

        public void Validate()
        {

            CustomerID.CheckStringIsNullOrEmpty("CustomerID");
            OrderID.CheckStringIsNullOrEmpty("OrderID");
            ItemID.CheckStringIsNullOrEmpty("ItemID");
            ConfirmedMoney.NullCheck("ConfirmedMoney");
            (ConfirmedMoney<0).TrueThrow("确认金额 有误！");
            
            Order.NullCheck("该 数据 有误，不存在订单！");
            Asset.NullCheck("该 数据 有误，不存在资产！");

            Service.ProductService.IsAssetConfirm(ProductID).FalseThrow("该数据不允许手工确认 或者 已过时间！");


            (Order.CustomerID == CustomerID).FalseThrow("CustomerID 提交数据有误！");
            var itemData =  OrderItemAdapter.Instance.Load(ItemID);
            itemData.NullCheck("该订单无明细！");
            (ConfirmedMoney > itemData.RealPrice * itemData.RealAmount - Asset.ConfirmedMoney).TrueThrow("确认金额 有误！");
            
            

        }
    }
}