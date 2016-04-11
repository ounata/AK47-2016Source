#region
// -------------------------------------------------
// Assembly	��	DeluxeWorks.Library.Data
// FileName	��	GenericDbContext.cs
// Remark	��	Generic database processing context��
// -------------------------------------------------
// VERSION  	AUTHOR			DATE			CONTENT
// 1.0		    ����			20070430		����
//	1.1			ccic\yuanyong	20070725		��������internal string ConnName
// -------------------------------------------------
#endregion
#region using
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
#endregion
namespace MCS.Library.Data
{
    /// <summary>
    /// Generic database processing context.
    /// <remarks>
    /// <list type="bullet">
    ///     <item>this context is attatch to current HttpContext(web app) or Thread CurrentContext property.</item>
    ///     <item>the primary goal is to harmonize the Transaction management in a call stack.</item>
    ///     <item>itself could be disposed automatically.</item>
    /// </list>
    /// </remarks>
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class DbContext : IDisposable
    {
        private bool _autoClose = true;
        private TimeSpan _commandTimeout = TimeSpan.FromSeconds(30);

        #region Public Methods
        ///// <summary>
        ///// ������������Ӵ�ִ�е�SQL��䣬�Զ�������ָ���
        ///// </summary>
        ///// <param name="sqlBuilder"></param>
        ///// <param name="format"></param>
        ///// <param name="args"></param>
        //public abstract void AppendSqlWithSperatorInContext(SqlBuilderBase sqlBuilder, string format, params object[] args);

        ///// <summary>
        ///// ������������Ӵ�ִ�е�SQL���
        ///// </summary>
        ///// <param name="sqlBuilder"></param>
        ///// <param name="format"></param>
        ///// <param name="args"></param>
        //public abstract void AppendSqlInContext(SqlBuilderBase sqlBuilder, string format, params object[] args);

        ///// <summary>
        ///// ����������ע���ѯ���صļ��б�Ľ������
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="action"></param>
        //public abstract void RegisterTableAction(string tableName, Action<DataTable> action);

        ///// <summary>
        ///// ����������е�SQL���
        ///// </summary>
        //public abstract void ClearSqlInContext();

        ///// <summary>
        ///// �õ��������е�SQL���
        ///// </summary>
        //public abstract string GetSqlInContext();

        /// <summary>
        /// ִ�б������������е�SQL��䣬����DataSet
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <param name="tableNames">��ѡ������DataSet�еı���</param>
        /// <returns></returns>
        public abstract DataSet ExecuteDataSetSqlInContext(bool clearSqlAfterExecute = true, params string[] tableNames);

        /// <summary>
        /// ִ�б������������е�SQL��䣬����DataReader
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <returns></returns>
        public abstract DbDataReader ExecuteReaderSqlInContext(bool clearSqlAfterExecute = true);

        /// <summary>
        /// ִ�б������������е�SQL��䣬���ص�ֵ
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <returns></returns>
        public abstract object ExecuteScalarSqlInContext(bool clearSqlAfterExecute = true);

        /// <summary>
        /// ִ�б������������е�SQL��䣬������Ӱ�������
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        public abstract int ExecuteNonQuerySqlInContext(bool clearSqlAfterExecute = true);

        /// <summary>
        /// ִ�б������������е�SQL��䣬��ǰ������ʱ���������ı���
        /// </summary>
        /// <param name="clearSqlAfterExecute"></param>
        public abstract void ExecuteTimePointSqlInContext(bool clearSqlAfterExecute = true);
        #endregion End Public Methods

        /// <summary>
        /// ��ȡһ��DbContext����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="autoClose">�Ƿ��Զ��ر�</param>
        /// <returns>DbContext����</returns>
        public static DbContext GetContext(string name, bool autoClose)
        {
            //�õ�ӳ�����������
            name = DbConnectionMappingContext.GetMappedConnectionName(name);

            DbProviderFactory factory = DbConnectionManager.GetDbProviderFactory(name);

            DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

            csb.ConnectionString = DbConnectionManager.GetConnectionString(name);

            bool enlist = true;

            if (csb.ContainsKey("enlist"))
                enlist = (bool)csb["enlist"];

            DbContext result = null;

            if (enlist)
                result = new AutoEnlistDbContext(name, autoClose);
            else
                result = new NotEnlistDbContext(name, autoClose);

            result.InitDbContext(name, autoClose);

            result._autoClose = autoClose;
            result._commandTimeout = DbConnectionManager.GetCommandTimeout(name);

            return result;
        }

        /// <summary>
        /// ���ػ�ȡDbContext����
        /// </summary>
        /// <param name="name">��������</param>
        /// <returns></returns>
        public static DbContext GetContext(string name)
        {
            return GetContext(name, true);
        }

        /// <summary>
        /// ��ȡһ��DbContext����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="autoClose">�Ƿ��Զ��ر�</param>
        /// <returns>DbContext����</returns>
        public static async Task<DbContext> GetContextAsync(string name, bool autoClose)
        {
            //�õ�ӳ�����������
            name = DbConnectionMappingContext.GetMappedConnectionName(name);

            DbProviderFactory factory = DbConnectionManager.GetDbProviderFactory(name);

            DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

            csb.ConnectionString = DbConnectionManager.GetConnectionString(name);

            bool enlist = true;

            if (csb.ContainsKey("enlist"))
                enlist = (bool)csb["enlist"];

            DbContext result = null;

            if (enlist)
                result = new AutoEnlistDbContext(name, autoClose);
            else
                result = new NotEnlistDbContext(name, autoClose);

            await result.InitDbContextAsync(name, autoClose);

            result._autoClose = autoClose;
            result._commandTimeout = DbConnectionManager.GetCommandTimeout(name);

            return result;
        }

        /// <summary>
        /// �첽��ȡDbContext����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<DbContext> GetContextAsync(string name)
        {
            return await GetContextAsync(name, true);
        }

        #region Public ��Ա

        /// <summary>
        /// �Ƿ��Զ��ر�
        /// </summary>
        public bool AutoClose
        {
            get
            {
                return this._autoClose;
            }
            private set
            {
                this._autoClose = value;
            }
        }

        /// <summary>
        /// �������Ӷ���
        /// </summary>
        public abstract DbConnection Connection
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// �����������
        /// </summary>
        public abstract DbTransaction LocalTransaction
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// SQL����ĳ�ʱʱ��
        /// </summary>
        public TimeSpan CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                this._commandTimeout = value;
            }
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// ��ʼ��DbContext
        /// </summary>
        /// <param name="name"></param>
        /// <param name="autoClose"></param>
        protected virtual void InitDbContext(string name, bool autoClose)
        {
        }

        /// <summary>
        /// �첽��ʼ��DbContext
        /// </summary>
        /// <param name="name"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        protected async virtual Task InitDbContextAsync(string name, bool autoClose)
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
