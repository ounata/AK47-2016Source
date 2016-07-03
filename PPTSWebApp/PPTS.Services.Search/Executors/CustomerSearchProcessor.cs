using PPTS.Contracts.Search.Models;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PPTS.Services.Search.Executors
{
    public static class CustomerSearchProcessor
    {
        static Dictionary<CustomerSearchUpdateType, CustomerSearchExecutorBase> _dict;
        static CustomerSearchProcessor()
        {
            _dict = new Dictionary<CustomerSearchUpdateType, CustomerSearchExecutorBase>();
            foreach(Type typ in Assembly.GetExecutingAssembly().GetTypes())
            {
                Type t = typeof(CustomerSearchExecutorBase);
                if (typ.IsSubclassOf(t))
                {
                    CustomerSearchExecutorBase executeor = (CustomerSearchExecutorBase)Activator.CreateInstance(typ);
                    _dict.Add(executeor.SearchUpdateType, executeor);
                }
            }
        }

        public static void Process(CustomerSearchUpdateModel data)
        {
            Process(new CustomerSearchUpdateModel[] { data });
        }
        public static void Process(IList<CustomerSearchUpdateModel> data)
        {
            Dictionary<CustomerSearchUpdateType, List<string>> dict = new Dictionary<CustomerSearchUpdateType, List<string>>();
            foreach (CustomerSearchUpdateModel d in data)
            {
                if (!dict.ContainsKey(d.Type))
                    dict.Add(d.Type, new List<string>());
                dict[d.Type].Add(d.CustomerID);
            }
            foreach (CustomerSearchUpdateType t in dict.Keys)
            {
                Process(t, dict[t]);
            }
        }

        public static void Process(IList<string> data)
        {
            Type type = typeof(CustomerSearchUpdateType);
            foreach (string s in type.GetEnumNames())
            {
                CustomerSearchUpdateType t = (CustomerSearchUpdateType)Enum.Parse(type, s);
                Process(t, data);
            }
        }
        private static void Process(CustomerSearchUpdateType t, IList<string> data)
        {
            if (_dict.ContainsKey(t))
            {
                List<string> page = new List<string>();
                for (int i = 1; i <= data.Count; i++)
                {
                    page.Add(data[i-1]);
                    if (i % 1000 == 0 || i == data.Count)
                    {
                        _dict[t].Execute(page);
                        page.Clear();
                    }
                }
                
            }
        }
    }
}