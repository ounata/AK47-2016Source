using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Converters
{
    public class JavascriptOguObjectConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            JToken objTypeToken = jObject.GetValue("objectType");
            string id = (string)jObject.GetValue("id");

            SchemaType objType = DataConverter.ChangeType<int, SchemaType>((int)objTypeToken);

            OguBase oguObj = (OguBase)OguBase.CreateWrapperObject(id, objType);

            oguObj.Name = (string)jObject.GetValue("name");
            oguObj.DisplayName = (string)jObject.GetValue("displayName");
            oguObj.Description = (string)jObject.GetValue("description");
            oguObj.FullPath = (string)jObject.GetValue("fullPath");

            string codeName = (string)jObject.GetValue("codeName");

            (oguObj as OguUser).IsNotNull(user => user.LogOnName = codeName);
            (oguObj as OguOrganization).IsNotNull(org => org.Properties["codeName"] = codeName);
            (oguObj as OguGroup).IsNotNull(group => group.Properties["codeName"] = codeName);

            return oguObj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject o = new JObject();

            IOguObject oguObj = (IOguObject)value;

            o.Add("id", oguObj.ID);
            o.Add("name", oguObj.Name);
            o.Add("fullPath", oguObj.FullPath);
            o.Add("description", oguObj.Description);
            o.Add("displayName", oguObj.DisplayName);
            o.Add("objectType", (int)oguObj.ObjectType);

            string codeName = string.Empty;

            (oguObj as IUser).IsNotNull(user => codeName = user.LogOnName);
            (oguObj as IOrganization).IsNotNull(org => codeName = org.Properties.GetValue("CodeName", string.Empty));
            (oguObj as IGroup).IsNotNull(group => codeName = group.Properties.GetValue("CodeName", string.Empty));

            o.Add("codeName", codeName);

            o.WriteTo(writer);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IOguObject).IsAssignableFrom(objectType);
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
    }
}
