using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Validators
{
    public class Order
    {
        [StringLengthValidator(255, MessageTemplate = "订单名称的长度不能超过255个字符")]
        [StringEmptyValidator(MessageTemplate = "订单名称不允许为空")]
        public string OrderName
        {
            get;
            set;
        }
    }
}
