using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Validation;

namespace MCS.Web.MVC.Library.Providers
{
    public class MCSBodyModelValidator : DefaultBodyModelValidator
    {
        public override bool ShouldValidateType(Type type)
        {
            bool oguType = typeof(IOguObject).IsAssignableFrom(type);

            return oguType == false && base.ShouldValidateType(type);
        }
    }
}
