using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    [DataContract]
    public class AssetConsumeViewModel: AssetConsumeView
    {        
        [DataMember]
        public string ConsumeTimeView {
            get {
                string dow = string.Empty;
                switch ((int)ConsumeTime.DayOfWeek)
                {
                    case 0:
                        dow = "星期日";
                        break;
                    case 1:
                        dow = "星期一";
                        break;
                    case 2:
                        dow = "星期二";
                        break;
                    case 3:
                        dow = "星期三";
                        break;
                    case 4:
                        dow = "星期四";
                        break;
                    case 5:
                        dow = "星期五";
                        break;
                    case 6:
                        dow = "星期六";
                        break;
                }
                return string.Format("{0}({1})", ConsumeTime.ToString("yyy-MM-dd"),dow);
            }
        }

        public string StartTime2EndTime { get {
                return string.Format("{0}-{1}",StartTime.ToString("HH:mm"),EndTime.ToString("HH:mm"));
            } }

        public decimal Hours { get {
                return (DurationValue * Amount) / (decimal)60.0;
            } }

        public decimal InComeMonye { get {
                return ConsumeMoney > 0 ? ConsumeMoney : 0;
            } }

        public decimal ExpendMoney{ get {
                return ConsumeMoney < 0 ? -ConsumeMoney : 0;
            } }
    }
    [DataContract]
    public class AssetConsumeViewCollectionModel : EditableDataObjectCollectionBase<AssetConsumeViewModel>
    {
    }

}