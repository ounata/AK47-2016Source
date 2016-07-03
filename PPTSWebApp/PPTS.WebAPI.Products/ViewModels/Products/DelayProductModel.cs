using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Products
{
    public class DelayProductModel
    {
        public string[] Ids { set; get; }
        public DateTime EndDate { set; get; }


        public List<Data.Products.Entities.Product> Model { set; get; }
        public DelayProductModel FillProduct()
        {
            Validate();
            
            if (Model != null)
            {
                Model.ToList().ForEach(m => { FillUser(m); });
            }

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
            }

        }

        public void Validate()
        {

        }

    }
}