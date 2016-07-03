using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMappingPerformance.Test
{
    [ORTableMapping("TEST_TABLE")]
    [Serializable]
    public class TestObject
    {
        [ORFieldMapping("ID", PrimaryKey = true)]
        public string ID
        {
            get;
            set;
        }

        [ORFieldMapping("NAME")]
        public string Name
        {
            get;
            set;
        }

        [ORFieldMapping("AMOUNT")]
        public Decimal Amount
        {
            get;
            set;
        }

        [ORFieldMapping("LOCAL_TIME")]
        public DateTime LocalTime
        {
            get;
            set;
        }

        [ORFieldMapping("UTC_TIME", UtcTimeToLocal = true)]
        public DateTime UtcTime
        {
            get;
            set;
        }

        #region Group1
        public string StringField1
        {
            get;
            set;
        }

        public string StringField2
        {
            get;
            set;
        }

        public string StringField3
        {
            get;
            set;
        }

        public string StringField4
        {
            get;
            set;
        }

        public string StringField5
        {
            get;
            set;
        }

        public string StringField6
        {
            get;
            set;
        }

        public string StringField7
        {
            get;
            set;
        }

        public string StringField8
        {
            get;
            set;
        }

        public string StringField9
        {
            get;
            set;
        }

        public string StringField10
        {
            get;
            set;
        }
        #endregion Group1

        #region Group2
        public string StringField11
        {
            get;
            set;
        }

        public string StringField12
        {
            get;
            set;
        }

        public string StringField13
        {
            get;
            set;
        }

        public string StringField14
        {
            get;
            set;
        }

        public string StringField15
        {
            get;
            set;
        }

        public string StringField16
        {
            get;
            set;
        }

        public string StringField17
        {
            get;
            set;
        }

        public string StringField18
        {
            get;
            set;
        }

        public string StringField19
        {
            get;
            set;
        }

        public string StringField20
        {
            get;
            set;
        }
        #endregion Group2

        #region Group3
        public string StringField21
        {
            get;
            set;
        }

        public string StringField22
        {
            get;
            set;
        }

        public string StringField23
        {
            get;
            set;
        }

        public string StringField24
        {
            get;
            set;
        }

        public string StringField25
        {
            get;
            set;
        }

        public string StringField26
        {
            get;
            set;
        }

        public string StringField27
        {
            get;
            set;
        }

        public string StringField28
        {
            get;
            set;
        }

        public string StringField29
        {
            get;
            set;
        }

        public string StringField30
        {
            get;
            set;
        }
        #endregion Group3
    }

    [Serializable]
    public class TestObjectCollection : EditableDataObjectCollectionBase<TestObject>
    {

    }
}
