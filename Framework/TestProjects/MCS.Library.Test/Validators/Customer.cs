using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Validators
{
    public class Customer
    {
        [StringLengthValidator(255, MessageTemplate = "客户名称的长度不能超过255个字符")]
        [StringEmptyValidator(MessageTemplate = "客户名称不允许为空")]
        public string CustomerName
        {
            get;
            set;
        }
    }
}
