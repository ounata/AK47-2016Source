#region
// -------------------------------------------------
// Assembly	��	DeluxeWorks.Library.Data
// FileName	��	DefaultDbContext.cs
// Remark	��	Generic database processing context��
// -------------------------------------------------
// VERSION  	AUTHOR			DATE			CONTENT
//  1.0		    ����			20070430		����
//	1.1			ccic\yuanyong	20070725		��������internal string ConnName
//	1.2			���			20080919		��ԭ��DbContext�Ĵ���Ǩ�ƹ���
// -------------------------------------------------
#endregion

#region using
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Properties;
using MCS.Library.Net.SNTP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
#endregion

namespace MCS.Library.Data
{
    [System.Diagnostics.DebuggerNonUserCode]
    internal abstract class DeluxeDbContextBase : DbContext
    {
        protected static readonly object GraphWithTxSyncObject = new object();

        #region Protected type(Class)

        protected class Connections : Dictionary<string, ReferenceConnection>
        {
        }

        protected class GraphWithoutTransaction : Connections
        {
        }
        #endregion

        #region Private const and field
        /// <summary>
        /// Private const
        /// <remarks>
        ///     db context key name prefix
        /// </remarks>
        /// </summary>
        private const string NamePrefix = "DeluxeWorks.Context";

        /// <summary>
        /// Private const
        /// <remarks>
        ///     the context key name postfix that doesn't exists in transaction.
        /// </remarks>
        /// </summary>
        private const string NamePostfixWithoutTransaction = ".GraphWithoutTx";

        /// <summary>
        /// �������е�SQL��仺��
        /// </summary>
        private const string NamePostfixSqlContext = ".SqlContext";

        /// <summary>
        /// Logical database name
        /// </summary>
        private string _name;

        /// <summary>
        /// ���ݿ��߼�����
        /// </summary>
        /// <remarks>
        /// ���ݿ����Ӷ��������������GenericDatabaseFactory����
        /// </remarks>
        public override string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// ��ǰ�������Ƿ������ӵĴ�����
        /// </summary>
        protected bool IsConnectionCreator
        {
            get
            {
                return this._isConnectionCreator;
            }
            set
            {
                this._isConnectionCreator = value;
            }
        }

        /// <summary>
        /// Internal connection object for non-transactional context
        /// </summary>
        private DbConnection _connection = null;

        /// <summary>
        /// Internal transaction object for non-transactional context
        /// </summary>
        private DbTransaction _localTransaction = null;

        ///// <summary>
        ///// Key of current context exists in HttpContext or Thread
        ///// </summary>
        //private string contextKey;

        /// <summary>
        /// Whether exists a transaction in constructor
        /// </summary>
        private bool _isInTransaction = false;

        /// <summary>
        /// Whether this context created a new DbConnection instance.
        /// </summary>
        private bool _isConnectionCreator = false;
        #endregion

        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="autoClose">�漴�ر�</param>
        protected DeluxeDbContextBase(string name, bool autoClose)
        {
        }

        #region Public property
        /// <summary>
        /// Current context connection.
        /// </summary>
        public override DbConnection Connection
        {
            get
            {
                return this._connection;
            }
            internal protected set
            {
                this._connection = value;
            }
        }

        public override DbTransaction LocalTransaction
        {
            get
            {
                return this._localTransaction;
            }
            protected internal set
            {
                this._localTransaction = value;
            }
        }

        /// <summary>
        /// �Ƿ��������й����������
        /// </summary>
        public bool IsInTransaction
        {
            get
            {
                return _isInTransaction;
            }
        }
        #endregion

        #region Public Methods
        ///// <summary>
        ///// ������������Ӵ�ִ�е�SQL��䣬�Զ�������ָ���
        ///// </summary>
        ///// <param name="sqlBuilder"></param>
        ///// <param name="format"></param>
        ///// <param name="args"></param>
        //public override void AppendSqlWithSperatorInContext(SqlBuilderBase sqlBuilder, string format, params object[] args)
        //{
        //    this.GetSqlContext().AppendSqlInContext(sqlBuilder, true, format, args);
        //    //DoSafeConnectionOp(this.Name, GetSqlContextReferenceConnections(), refConnection => refConnection.AppendSqlInContext(sqlBuilder, true, format, args));
        //}

        ///// <summary>
        ///// ������������Ӵ�ִ�е�SQL���
        ///// </summary>
        ///// <param name="sqlBuilder"></param>
        ///// <param name="format"></param>
        ///// <param name="args"></param>
        //public override void AppendSqlInContext(SqlBuilderBase sqlBuilder, string format, params object[] args)
        //{
        //    DoSafeConnectionOp(this.Name, GetSqlContextReferenceConnections(), refConnection => refConnection.AppendSqlInContext(sqlBuilder, false, format, args));
        //}

        ///// <summary>
        ///// ����������ע���ѯ���صļ��б�Ľ������
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="action"></param>
        //public override void RegisterTableAction(string tableName, Action<DataTable> action)
        //{
        //    DoSafeConnectionOp(this.Name, GetSqlContextReferenceConnections(), refConnection => refConnection.RegisterTableAction(tableName, action));
        //}

        ///// <summary>
        ///// ����������е�SQL���
        ///// </summary>
        //public override void ClearSqlInContext()
        //{
        //    DoSafeConnectionOp(this.Name, GetSqlContextReferenceConnections(), refConnection => refConnection.ClearSqlInContext());
        //}

        ///// <summary>
        ///// �õ��������е�SQL���
        ///// </summary>
        //public override string GetSqlInContext()
        //{
        //    string result = string.Empty;

        //    DoSafeConnectionOp(this.Name, GetSqlContextReferenceConnections(), refConnection => result = refConnection.GetSqlInContext());

        //    return result;
        //}

        /// <summary>
        /// ִ�б������������е�SQL��䣬����DataSet
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <param name="tableNames">��ѡ������DataSet�еı���</param>
        /// <returns></returns>
        public override DataSet ExecuteDataSetSqlInContext(bool clearSqlAfterExecute = true, params string[] tableNames)
        {
            DataSet result = null;

            SqlContextItem sqlContext = this.GetSqlContext();

            this.ExecuteSqlInContext((refConnection, db, sql) =>
            {
                if (tableNames == null || tableNames.Length == 0)
                    tableNames = sqlContext.TableActions.ToTableNames();

                result = db.ExecuteDataSet(CommandType.Text, sql, tableNames);

                //����Table�ص���ÿ���¼�
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    if (i < sqlContext.TableActions.Count)
                    {
                        TableAction ta = sqlContext.TableActions[i];

                        if (ta.Action != null)
                            ta.Action(result.Tables[i]);
                    }
                }
            },
            clearSqlAfterExecute);

            return result;
        }

        /// <summary>
        /// ִ�б������������е�SQL��䣬����DataReader
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <returns></returns>
        public override DbDataReader ExecuteReaderSqlInContext(bool clearSqlAfterExecute = true)
        {
            DbDataReader result = null;

            this.ExecuteSqlInContext((refConnection, db, sql) => result = db.ExecuteReader(CommandType.Text, sql), clearSqlAfterExecute);

            return result;
        }

        /// <summary>
        /// ִ�б������������е�SQL��䣬���ص�ֵ
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        /// <returns></returns>
        public override object ExecuteScalarSqlInContext(bool clearSqlAfterExecute = true)
        {
            object result = null;

            this.ExecuteSqlInContext((refConnection, db, sql) => result = db.ExecuteScalar(CommandType.Text, sql), clearSqlAfterExecute);

            return result;
        }

        /// <summary>
        /// ִ�б������������е�SQL��䣬������Ӱ�������
        /// </summary>
        /// <param name="clearSqlAfterExecute">ִ������Ƿ�����������е�SQL��Ĭ����true</param>
        public override int ExecuteNonQuerySqlInContext(bool clearSqlAfterExecute = true)
        {
            int result = 0;

            this.ExecuteSqlInContext((refConnection, db, sql) => result = db.ExecuteNonQuery(CommandType.Text, sql), clearSqlAfterExecute);

            return result;
        }

        /// <summary>
        /// �õ����õ����ݿ�����
        /// </summary>
        /// <returns></returns>
        protected virtual Connections GetSqlContextReferenceConnections()
        {
            return this.GraphWithoutTx;
        }

        private void ExecuteSqlInContext(Action<ReferenceConnection, Database, string> dbAction, bool clearSqlAfterExecute)
        {
            SqlContextItem sqlContext = this.GetSqlContext();

            DoSafeConnectionOp(this.Name, this.GetSqlContextReferenceConnections(), refConnection =>
            {
                string sql = sqlContext.GetSqlInContext();

                if (sql.IsNotEmpty() && dbAction != null)
                {
                    Database db = DatabaseFactory.Create(this);

                    dbAction(refConnection, db, sql);

                    if (clearSqlAfterExecute)
                        sqlContext.ClearSqlInContext();
                }
            });
        }
        #endregion

        #region IDisposable ��Ա
        /// <summary>
        /// ɾ����������
        /// <remarks>
        ///     the dispose process is varied according to whether a Current Transaction exists.
        /// <list type="bullet">
        /// </list>
        /// </remarks>
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ReleaseConnection();

                // not transactional operation
                if (this.AutoClose)
                    this.RemoveConnection();
            }
        }

        #endregion

        #region Protected methods
        /// <summary>
        /// �����ʼ��
        /// </summary>
        protected virtual void OnInitWithTransaction()
        {
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        protected abstract DbConnection OnGetConnectionWithTransaction(Transaction ts);

        protected virtual GraphWithoutTransaction GraphWithoutTx
        {
            get
            {
                return StaticGraphWithoutTx;
            }
        }

        /// <summary>
        /// ��ȡ����������������ֵ�
        /// </summary>
        /// <returns></returns>
        protected static GraphWithoutTransaction StaticGraphWithoutTx
        {
            get
            {
                const string itemKey = NamePrefix + NamePostfixWithoutTransaction;

                return (GraphWithoutTransaction)ObjectContextCache.Instance.GetOrAddNewValue(itemKey, (cache, key) =>
                {
                    GraphWithoutTransaction gwt = new GraphWithoutTransaction();

                    cache.Add(key, gwt);

                    return gwt;
                });
            }
        }

        protected SqlContextItem GetSqlContext()
        {
            return SqlContext.GetContext(this.Name);
        }

        /// <summary>
        /// �ͷ�����
        /// </summary>
        protected void ReleaseConnection()
        {
            DoSafeConnectionOp(this.Name, this.GraphWithoutTx, (refConnection) => refConnection.ReferenceCount--);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        protected void RemoveConnection()
        {
            DoSafeConnectionOp(this.Name, this.GraphWithoutTx, (refConnection) =>
            {
                if (refConnection.ReferenceCount == 0)
                {
                    if (this.IsInTransaction == false)
                    {
                        try
                        {
                            if (refConnection.Connection.State != ConnectionState.Closed)
                                refConnection.Connection.Close();

                            WriteTraceInfo(refConnection.Connection.DataSource + "." + refConnection.Connection.Database
                                        + "[" + SNTPClient.AdjustedTime.ToString("yyyyMMdd HH:mm:ss.fff") + "]",
                                        " Close Connection ");
                        }
                        finally
                        {
                            this.GraphWithoutTx.Remove(this.Name);
                        }
                    }
                }
            });
        }
        #endregion

        #region private methods
        /// <summary>
        /// ��ʼ�������������
        /// </summary>
        /// <param name="name">������������</param>
        /// <param name="autoClose">�Ƿ��Զ��ر�</param>
        protected override void InitDbContext(string name, bool autoClose)
        {
            ExceptionHelper.CheckStringIsNullOrEmpty(name, "name");

            this._name = name;

            // current execution without transaction
            if (Transaction.Current == null)
            {
                this._isInTransaction = false;
            }
            else
            {
                this._isInTransaction = true;
                Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler(CompleteIndividualTransaction);
                this.OnInitWithTransaction();
            }

            Tuple<DbConnection, bool> connectionInfo = this.CreateConnection(name);

            this._connection = connectionInfo.Item1;
            this._isConnectionCreator = connectionInfo.Item2;
        }

        protected async override Task InitDbContextAsync(string name, bool autoClose)
        {
            ExceptionHelper.CheckStringIsNullOrEmpty(name, "name");

            this._name = name;

            // current execution without transaction
            if (Transaction.Current == null)
            {
                this._isInTransaction = false;
            }
            else
            {
                this._isInTransaction = true;
                Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler(CompleteIndividualTransaction);
                this.OnInitWithTransaction();
            }

            Tuple<DbConnection, bool> connectionInfo = await this.CreateConnectionAsync(name);

            this._connection = connectionInfo.Item1;
            this._isConnectionCreator = connectionInfo.Item2;
        }

        /// <summary>
        /// ִ���̰߳�ȫ�����Ӳ���
        /// </summary>
        /// <param name="connName"></param>
        /// <param name="connections"></param>
        /// <param name="foundAction"></param>
        /// <param name="notFoundAction"></param>
        protected void DoSafeConnectionOp(string connName, Connections connections, Action<ReferenceConnection> foundAction, Action notFoundAction = null)
        {
            lock (connections)
            {
                ReferenceConnection refConnection = null;

                if (connections.TryGetValue(connName, out refConnection) == false)
                {
                    if (notFoundAction != null)
                        notFoundAction();
                }
                else
                {
                    if (foundAction != null)
                        foundAction(refConnection);
                }
            }
        }

        /// <summary>
        /// ����һ������
        /// <remarks>
        ///     the connection retrieve process is as the following procedure:
        /// <list type="bullet">
        ///     <item>if no transaction exists, this method create and return a new DbConnection instance</item>
        ///     <item>if transaction exists, this method should return a cached DbConnection instance</item>
        /// </list>
        /// <param name="name">���ݿ���������</param>
        /// </remarks>
        /// </summary>
        private Tuple<DbConnection, bool> CreateConnection(string name)
        {
            bool isConnectionCreator = false;

            // non-transactional operation
            GraphWithoutTransaction connections = this.GraphWithoutTx;

            DbConnection connection = GetConnection(name);

            if ((connection != null) && (connection.State != ConnectionState.Open))
            {
                if (connection.ConnectionString.IsNullOrEmpty())
                    connection.ConnectionString = DbConnectionManager.GetConnectionString(name);

                OpenConnection(name, connection);

                WriteTraceInfo(connection.DataSource + "." + connection.Database
                    + "[" + SNTPClient.AdjustedTime.ToString("yyyyMMdd HH:mm:ss.fff") + "]", " Open Connection ");
            }

            return new Tuple<DbConnection, bool>(connection, isConnectionCreator);
        }

        private async Task<Tuple<DbConnection, bool>> CreateConnectionAsync(string name)
        {
            bool isConnectionCreator = false;

            // non-transactional operation
            GraphWithoutTransaction connections = this.GraphWithoutTx;

            DbConnection connection = GetConnection(name);

            if ((connection != null) && (connection.State != ConnectionState.Open))
            {
                if (connection.ConnectionString.IsNullOrEmpty())
                    connection.ConnectionString = DbConnectionManager.GetConnectionString(name);

                await OpenConnectionAsync(name, connection);

                WriteTraceInfo(connection.DataSource + "." + connection.Database
                    + "[" + SNTPClient.AdjustedTime.ToString("yyyyMMdd HH:mm:ss.fff") + "]", " Open Connection ");
            }

            return new Tuple<DbConnection, bool>(connection, isConnectionCreator);
        }

        /// <summary>
        /// �������ӵ����ƣ��Ӵ���������Ӽ��ϣ����߲�����������Ӽ����л�ȡ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private DbConnection GetConnection(string name)
        {
            DbConnection connection = null;

            if (Transaction.Current == null)
                connection = this.GetConnectionWithoutTx(name);
            else
                connection = this.OnGetConnectionWithTransaction(Transaction.Current);

            return connection;
        }

        protected virtual void OnTransactionCompleted(TransactionEventArgs args)
        {
        }

        /// <summary>
        /// �������Ƶõ�GraphWithoutTx�����Ӷ�����������ڣ��Զ�����һ���µ����Ӷ���û��Open��
        /// </summary>
        /// <param name="connName">���ݿ���������</param>
        /// <returns>GraphWithoutTx�����Ӷ���</returns>
        protected DbConnection GetConnectionWithoutTx(string connName)
        {
            ReferenceConnection refConnection = this.GetRefConnectionWithoutTx(connName);

            DbConnection connection = null;

            if (refConnection != null)
                connection = refConnection.Connection;

            return connection;
        }

        /// <summary>
        /// �������Ƶõ���ǰ���õ�����
        /// </summary>
        /// <param name="connName">���ݿ���������</param>
        /// <returns>���Ӷ���</returns>
        protected ReferenceConnection GetRefConnectionWithoutTx(string connName)
        {
            ReferenceConnection result = null;

            DoSafeConnectionOp(connName,
                this.GraphWithoutTx,
                (refConnection) =>
                {
                    refConnection.ReferenceCount++;
                    result = refConnection;
                },
                () =>
                {
                    DbConnection connection = DbConnectionManager.GetConnection(connName);
                    this._isConnectionCreator = true;

                    result = new ReferenceConnection(connName, connection);
                    this.GraphWithoutTx.Add(connName, result);
                });

            return result;
        }

        protected static void WriteTraceInfo(string info, string category)
        {
#if DELUXEWORKSTEST
			Trace.WriteLine(info, category);
#endif
        }

        protected static void WriteTraceInfo(string info)
        {
#if DELUXEWORKSTEST
			Trace.WriteLine(info);
#endif
        }
        #endregion

        /// <summary>
        /// �����ӣ���������򷵻���������
        /// </summary>
        /// <param name="name"></param>
        /// <param name="conn"></param>
        private static void OpenConnection(string name, DbConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                string message = string.Format("Open connection '{0}' error. {1}", name, ex.Message);

                throw new SystemSupportException(message);
            }
        }

        /// <summary>
        /// �����ӣ���������򷵻���������
        /// </summary>
        /// <param name="name"></param>
        /// <param name="conn"></param>
        private static async Task OpenConnectionAsync(string name, DbConnection conn)
        {
            try
            {
                await conn.OpenAsync();
            }
            catch (System.Exception ex)
            {
                string message = string.Format("Open connection '{0}' error. {1}", name, ex.Message);

                throw new SystemSupportException(message);
            }
        }

        #region Event handler
        /// <summary>
        /// Event handler when transaction has completed.
        /// <remarks>
        ///     clear all associated DbConnection and remove associated graph element.
        /// </remarks>
        /// </summary>
        private void CompleteIndividualTransaction(object sender, TransactionEventArgs args)
        {
            WriteTraceInfo("CompleteIndividualTransaction ManagedThreadId :"
                + Thread.CurrentThread.ManagedThreadId.ToString());

            OnTransactionCompleted(args);
        }
        #endregion
    }
}
