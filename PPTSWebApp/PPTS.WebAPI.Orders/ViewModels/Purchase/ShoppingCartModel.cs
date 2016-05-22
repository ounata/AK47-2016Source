using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{


    public class ShoppingCartModel
    {
        public int ListType { set; get; }

        public List<AccountModel> Account { private set; get; }

        public List<CartModel> Cart { private set; get; }

        public Service.Present Present { private set; get; }
        /// <summary>
        /// 缴费单列表
        /// </summary>
        public List<AccountChargePayment> ChargePayments { set; get; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }




        public ShoppingCartModel FillAccount(List<Data.Customers.Entities.Account> accounts)
        {
            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<Data.Customers.Entities.Account, AccountModel>()).CreateMapper();
            Account = new List<AccountModel>();
            accounts.ForEach(m =>
            {
                var n = mapper.Map<AccountModel>(m);
                Account.Add(n);
            });
            return this;
        }

        public ShoppingCartModel FillCart(ShoppingCartCollection collection)
        {
            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<ShoppingCart, CartModel>()).CreateMapper();
            Cart = new List<CartModel>();
            foreach (var item in collection)
            {
                var n = mapper.Map<CartModel>(item);
                n.Item = new OrderItemViewModel() { CartID = item.CartID, OrderAmount = 1 };
                Cart.Add(n);
            }
            return this;
        }

        public ShoppingCartModel FillCartProduct(List<ProductView> products)
        {
            //var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<ProductView, ProductViewModel>(); }).CreateMapper();
            products.ForEach(m =>
            {
                Cart.Single(s => s.ProductID == m.ProductID).Product = m;
                //Cart.Single(s => s.ProductID == m.ProductID).Product = mapper.Map<ProductViewModel>(m);
            });
            return this;
        }

        public ShoppingCartModel FillClassGroupAmount()
        {
            if (ListType == 3)
            {
                var classIds = Cart.Where(w => !string.IsNullOrWhiteSpace(w.ClassID)).Select(s => s.ClassID).ToArray();
                if (classIds.Length > 0)
                {
                    ClassesAdapter.Instance.Load(classIds).ForEach(m => {
                        var cart = Cart.Single(c => c.ClassID == m.ClassID);

                        cart.NoOpenClassCount = m.LessonCount - m.FinishedLessons;
                        cart.RemainLessonCount = m.LessonCount - m.InvalidLessons;

                        cart.Item.OrderAmount = cart.RemainLessonCount;
                    });

                    

                    //ClassLessonsAdapter.Instance.GetNoOpenAccountLessonCountInContext(d =>
                    //{
                    //    d.ToList().ForEach(kv =>
                    //    {
                    //        var cart = Cart.Single(s => s.ClassID == kv.Key);
                    //        cart.RemainLessonCount += kv.Value;
                    //        cart.Item.OrderAmount = cart.RemainLessonCount;
                    //    });
                    //}, openAccountDate, classIds);

                    //PPTS.Data.Orders.ConnectionDefine.GetDbContext().DoAction(c => c.ExecuteDataSetSqlInContext());

                }
            }
            return this;
        }

        public ShoppingCartModel FillPreset(Service.Present preset)
        {
            if (ListType == 2)
            {
                Present = preset;
            }
            return this;
        }
        /// <summary>
        /// 设置使用折扣
        /// </summary>
        public ShoppingCartModel SetDiscount()
        {
            Cart.ForEach(m =>
            {
                m.Item.ProductID = m.ProductID;
                m.Item.SpecialRate = m.Item.OrderAmount = 1;
                if (m.Product.CategoryType == Data.Products.CategoryType.CalssGroup)
                {
                    m.Item.OrderAmount = m.Product.LessonCount;
                }
            });

            if (Account.Count > 0)
            {
                var currentAccount = Account.Last();
                currentAccount.Selected = true;
            }
            return this;
        }



    }


    public class AccountModel : Data.Customers.Entities.Account
    {
        [DataMember]
        public bool Selected { set; get; }
    }


    public class CartModel : Data.Orders.Entities.ShoppingCart
    {

        [DataMember]
        public ProductView Product { set; get; }

        [DataMember]
        public OrderItemViewModel Item { set; get; }

        /// <summary>
        /// 未开课 次数
        /// </summary>
        [DataMember]
        public int NoOpenClassCount { set; get; }

        /// <summary>
        /// 插班 剩余课时
        /// </summary>
        [DataMember]
        public int RemainLessonCount { set; get; }


    }

    //public class ProductViewModel : ProductView { }

    public class OrderItemViewModel
    {
        public string CartID { set; get; }

        public string ProductID { set; get; }

        /// <summary>
        /// 订购数量
        /// </summary>
        public decimal OrderAmount { set; get; }

        /// <summary>
        /// 特殊折扣
        /// </summary>
        public decimal SpecialRate { set; get; }

        /// <summary>
        /// 买赠-赠送数量
        /// </summary>
        public decimal PresentAmount { set; get; }

    }



}