using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{


    public class ShoppingCartModel
    {
        public List<Account> Account { private set; get; }

        public List<Cart> Cart { private set; get; }

        public void FillAccount(List<Service.Account> accounts)
        {
            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<Service.Account, Purchase.Account>()).CreateMapper();
            Account = new List<Purchase.Account>();
            accounts.ForEach(m => {
                var n = mapper.Map<Purchase.Account>(m);
                Account.Add(n);
            });
        }

        public void FillCart(ShoppingCartCollection collection)
        {
            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<ShoppingCart, Purchase.Cart>()).CreateMapper();
            Cart = new List<Purchase.Cart>();
            foreach (var item in collection)
            {
                var n = mapper.Map<Purchase.Cart>(item);
                n.Item = new OrderItem() { CartID = item.CartID, OrderAmount = 1 };
                Cart.Add(n);
            }
        }

        public void FillCartProduct( List<Service.Product> products)
        {
            products.ForEach(m => { Cart.Single(s => s.ProductID == m.ProductID).Product = m;  });
        }
        
        public Service.Present Present { set; get; }
        /// <summary>
        /// 缴费单列表
        /// </summary>
        public List<Service.AccountChargePayment> ChargePayments { set; get; }


        /// <summary>
        /// 设置使用折扣
        /// </summary>
        public void SetDiscount()
        {
            if (Account.Count < 1) return;

            var currentAccount = Account.Last();
            currentAccount.Selected = true;

            Cart.ForEach(m => {
                m.Item.ProductID = m.ProductID;
                m.Item.SpecialRate = 1;
                m.Item.OrderAmount = 1;
                if (m.Product.CategoryType == Data.Products.CategoryType.CalssGroup)
                {
                    m.Item.OrderAmount = m.Product.LessonCount;
                }
            });
            
        }


        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }


    public class Account: Service.Account
    {
        [DataMember]
        public bool Selected { set; get; }
    }


    public class Cart: Data.Orders.Entities.ShoppingCart
    {
        
        [DataMember]
        public Service.Product Product { set; get; }

        [DataMember]
        public OrderItem Item { set; get; }

    }

    public class OrderItem
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