using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional
{
    public class TxProcessAdapter : UpdatableAndLoadableAdapterBase<TxProcess, TxProcessCollection>
    {
        private string connectionName = string.Empty;

        private TxProcessAdapter()
        {
        }

        public static readonly TxProcessAdapter DefaultInstance = new TxProcessAdapter();

        public static TxProcessAdapter GetInstance(string connectionName)
        {
            TxProcessAdapter result = DefaultInstance;

            if (connectionName.IsNotEmpty())
                result = new TxProcessAdapter(connectionName);

            return result;
        }

        /// <summary>
        /// 数据库连接串名默认是HB2008，若需要改变连接串名则使用本构造函数，该种用法下本类不是单件模式
        /// </summary>
        /// <param name="connectionName">使用的数据库连接串名</param>
        public TxProcessAdapter(string connectionName)
        {
            this.connectionName = connectionName;
        }

        public TxProcess Load(string processID)
        {
            processID.CheckStringIsNullOrEmpty("processID");

            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem("PROCESS_ID", processID), "PROCESS_ID")).SingleOrDefault();
        }

        public TxProcess CopyTo(TxProcess process, string targetConnectionName)
        {
            process.NullCheck("process");

            TxProcessAdapter.GetInstance(targetConnectionName).Update(process);

            return process;
        }

        public override TxProcess CreateNewData(DataRow row)
        {
            string data = row["DATA"].ToString();

            TxProcess process = GetDeserilizedProcess(data);

            if (process == null)
                process = base.CreateNewData(row);

            return process;
        }

        protected override string GetInsertSql(TxProcess data, ORMappingItemCollection mappings, Dictionary<string, object> context, string[] ignoreProperties)
        {
            InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(data, mappings, ignoreProperties);

            string serilizedProcess = context.GetValue("SerilizedProcess", string.Empty);

            if (serilizedProcess.IsNullOrEmpty())
                serilizedProcess = GetSerilizedProcess(data);

            builder.AppendItem("DATA", serilizedProcess);

            return string.Format("INSERT INTO {0} {1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
        }

        protected override string GetUpdateSql(TxProcess data, ORMappingItemCollection mappings, Dictionary<string, object> context, string[] ignoreProperties)
        {
            UpdateSqlClauseBuilder uBuilder = ORMapping.GetUpdateSqlClauseBuilder(data, mappings, ignoreProperties);

            string serilizedProcess = GetSerilizedProcess(data);

            uBuilder.AppendItem("DATA", serilizedProcess);

            context["SerilizedProcess"] = serilizedProcess;

            WhereSqlClauseBuilder wBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data);

            return string.Format("UPDATE {0} SET {1} WHERE {2}", this.GetTableName(),
                uBuilder.ToSqlString(TSqlBuilder.Instance), wBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        protected override string GetConnectionName()
        {
            string result = this.connectionName;

            if (result.IsNullOrEmpty())
                result = this.GetDefaultConnectionName();

            return result;
        }

        private string GetDefaultConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }

        private static string GetSerilizedProcess(TxProcess process)
        {
            XElementFormatter formatter = new XElementFormatter();

            return formatter.Serialize(process).ToString();
        }

        private static TxProcess GetDeserilizedProcess(string data)
        {
            TxProcess process = null;

            if (data.IsNotEmpty())
            {
                XElement root = XElement.Parse(data);

                XElementFormatter formatter = new XElementFormatter();

                process = (TxProcess)formatter.Deserialize(root);
            }

            return process;
        }
    }
}
