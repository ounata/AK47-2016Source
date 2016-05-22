using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.API.Models
{
    public enum GenderType
    {
        Unknown = 0,
        Male,
        Female
    }

    public class User
    {
        public string UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public GenderType Gender
        {
            get;
            set;
        }
    }
}