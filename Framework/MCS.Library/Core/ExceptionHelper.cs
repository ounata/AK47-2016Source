#region
// -------------------------------------------------
// Assembly	��	DeluxeWorks.Library
// FileName	��	ExceptionHelper.cs
// Remark	��	Exception���ߣ�TrueThrow�����ж����Ĳ�������ֵ�Ƿ�Ϊtrue���������׳��쳣��FalseThrow�����ж����Ĳ�������ֵ�Ƿ�Ϊfalse���������׳��쳣�� 
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    ���	    20070430		����
// -------------------------------------------------
#endregion
using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Diagnostics;
using System.Web.Hosting;

using MCS.Library.Properties;
using System.IO;

namespace MCS.Library.Core
{
    /// <summary>
    /// Exception���ߣ��ṩ��TrueThrow��FalseThrow�ȷ���
    /// </summary>
    /// <remarks>Exception���ߣ�TrueThrow�����ж����Ĳ�������ֵ�Ƿ�Ϊtrue���������׳��쳣��FalseThrow�����ж����Ĳ�������ֵ�Ƿ�Ϊfalse���������׳��쳣��
    /// </remarks>
    public static class ExceptionHelper
    {
        /// <summary>
        /// �������Ƿ�Ϊ�գ����Ϊ�գ��׳�ArgumentNullException
        /// </summary>
        /// <param name="data">�����Ķ���</param>
        /// <param name="message">����������</param>
        /// <returns>���ش����data�����Լ������к�������</returns>
        [DebuggerNonUserCode]
        public static object NullCheck(this object data, string message)
        {
            return NullCheck<ArgumentNullException>(data, message);
        }

        /// <summary>
        /// �������Ƿ�Ϊ�գ����Ϊ�գ��׳��쳣
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="messageParams"></param>
        /// <returns>���ش����data�����Լ������к�������</returns>
        [DebuggerNonUserCode]
        public static object NullCheck<T>(this object data, string message, params object[] messageParams) where T : System.Exception
        {
            (data == null).TrueThrow<T>(message, messageParams);

            return data;
        }

        /// <summary>
        /// ����������ʽboolExpression�Ľ��ֵΪ��(true)�����׳�strMessageָ���Ĵ�����Ϣ
        /// </summary>
        /// <param name="parseExpressionResult">�������ʽ</param>
        /// <param name="message">������Ϣ</param>
        /// <param name="messageParams">������Ϣ����</param>
        /// <returns>���ش����parseExpressionResult</returns>
        /// <remarks>
        /// ����������ʽboolExpression�Ľ��ֵΪ��(true)�����׳�strMessageָ���Ĵ�����Ϣ
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Test\Core\ExceptionsTest.cs"  lang="cs" title="ͨ���ж��������ʽboolExpression�Ľ��ֵ���ж��Ƿ��׳�ָ�����쳣��Ϣ" />
        /// <seealso cref="FalseThrow"/>
        /// <seealso cref="MCS.Library.Compression.ZipReader"/>
        /// </remarks>
        /// <example>
        /// <code>
        /// ExceptionTools.TrueThrow(name == string.Empty, "�Բ������ֲ���Ϊ�գ�");
        /// </code>
        /// </example>
        [DebuggerNonUserCode]
        public static bool TrueThrow(this bool parseExpressionResult, string message, params object[] messageParams)
        {
            return TrueThrow<SystemSupportException>(parseExpressionResult, message, messageParams);
        }

        /// <summary>
        /// ����������ʽboolExpression�Ľ��ֵΪ��(true)�����׳�strMessageָ���Ĵ�����Ϣ
        /// </summary>
        /// <param name="parseExpressionResult">�������ʽ</param>
        /// <param name="message">������Ϣ</param>
        /// <param name="messageParams">������Ϣ�Ĳ���</param>
        /// <typeparam name="T">�쳣������</typeparam>
        /// <returns>���ش����parseExpressionResult</returns>
        /// <remarks>
        /// ����������ʽboolExpression�Ľ��ֵΪ��(true)�����׳�messageָ���Ĵ�����Ϣ
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Test\Core\ExceptionsTest.cs" region = "TrueThrowTest" lang="cs" title="ͨ���ж��������ʽboolExpression�Ľ��ֵ���ж��Ƿ��׳�ָ�����쳣��Ϣ" />
        /// <seealso cref="FalseThrow"/>
        /// <seealso cref="MCS.Library.Logging.LogEntity"/>
        /// </remarks>
        [DebuggerNonUserCode]
        public static bool TrueThrow<T>(this bool parseExpressionResult, string message, params object[] messageParams) where T : System.Exception
        {
            if (parseExpressionResult)
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                Type exceptionType = typeof(T);

                Object obj = Activator.CreateInstance(exceptionType);

                Type[] types = new Type[1];
                types[0] = typeof(string);

                ConstructorInfo constructorInfoObj = exceptionType.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public, null,
                    CallingConventions.HasThis, types, null);

                Object[] args = new Object[1];

                args[0] = string.Format(message, messageParams);

                constructorInfoObj.Invoke(obj, args);

                throw (Exception)obj;
            }

            return parseExpressionResult;
        }

        /// <summary>
        /// ����������ʽboolExpression�Ľ��ֵΪ�٣�false�������׳�strMessageָ���Ĵ�����Ϣ
        /// </summary>
        /// <param name="parseExpressionResult">�������ʽ</param>
        /// <param name="message">������Ϣ</param>
        /// <param name="messageParams">������Ϣ����</param>
        /// <returns>���ش����parseExpressionResult</returns>
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Test\Core\ExceptionsTest.cs" region = "FalseThrowTest" lang="cs" title="ͨ���ж��������ʽboolExpression�Ľ��ֵ���ж��Ƿ��׳�ָ�����쳣��Ϣ" />
        /// <seealso cref="TrueThrow"/>
        /// <seealso cref="MCS.Library.Logging.LoggerFactory"/>
        /// <remarks>
        /// ����������ʽboolExpression�Ľ��ֵΪ�٣�false�������׳�messageָ���Ĵ�����Ϣ
        /// </remarks>
        /// <example>
        /// <code>
        /// ExceptionTools.FalseThrow(name != string.Empty, "�Բ������ֲ���Ϊ�գ�");
        /// </code>
        /// </example>
        [DebuggerNonUserCode]
        public static bool FalseThrow(this bool parseExpressionResult, string message, params object[] messageParams)
        {
            TrueThrow(false == parseExpressionResult, message, messageParams);

            return parseExpressionResult;
        }

        /// <summary>
        /// ����������ʽboolExpression�Ľ��ֵΪ�٣�false�������׳�messageָ���Ĵ�����Ϣ
        /// </summary>
        /// <typeparam name="T">�쳣������</typeparam>
        /// <param name="parseExpressionResult">�������ʽ</param>
        /// <param name="message">������Ϣ</param>
        /// <param name="messageParams">������Ϣ����</param>
        /// <returns>���ش����parseExpressionResult</returns>
        /// <remarks>
        /// ����������ʽboolExpression�Ľ��ֵΪ�٣�false�������׳�strMessageָ���Ĵ�����Ϣ
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Test\Core\ExceptionsTest.cs" region="FalseThrowTest" lang="cs" title="ͨ���ж��������ʽboolExpression�Ľ��ֵ���ж��Ƿ��׳�ָ�����쳣��Ϣ" />
        /// <seealso cref="TrueThrow"/>
        /// <seealso cref="MCS.Library.Core.EnumItemDescriptionAttribute"/>
        /// </remarks>
        /// <example>
        /// <code>
        /// ExceptionTools.FalseThrow(name != string.Empty, typeof(ApplicationException), "�Բ������ֲ���Ϊ�գ�");
        /// </code>
        /// </example>
        [DebuggerNonUserCode]
        public static bool FalseThrow<T>(this bool parseExpressionResult, string message, params object[] messageParams) where T : System.Exception
        {
            TrueThrow<T>(false == parseExpressionResult, message, messageParams);

            return parseExpressionResult;
        }

        /// <summary>
        /// ����ַ��������Ƿ�ΪNull��մ�������ǣ����׳��쳣
        /// </summary>
        /// <param name="data">�ַ�������ֵ</param>
        /// <param name="paramName">�ַ�������</param>
        /// <returns>���ش����data</returns>
        /// <remarks>
        /// ���ַ�������ΪNull��մ����׳�ArgumentException�쳣
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Test\Core\ExceptionsTest.cs" region="CheckStringIsNullOrEmpty" lang="cs" title="����ַ��������Ƿ�ΪNull��մ������ǣ����׳��쳣" />
        /// </remarks>
        [DebuggerNonUserCode]
        public static string CheckStringIsNullOrEmpty(this string data, string paramName)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException(string.Format(Resource.StringParamCanNotBeNullOrEmpty, paramName));

            return data;
        }

        /// <summary>
        /// ����ַ��������Ƿ�ΪNull��մ�������ǣ����׳��쳣
        /// </summary>
        /// <typeparam name="T">�쳣������</typeparam>
        /// <param name="data">����ַ��������Ƿ�ΪNull��մ�������ǣ����׳��쳣</param>
        /// <param name="message"></param>
        /// <returns>���ش����data</returns>
        [DebuggerNonUserCode]
        public static string CheckStringIsNullOrEmpty<T>(this string data, string message) where T : System.Exception
        {
            (string.IsNullOrEmpty(data)).TrueThrow(message);

            return data;
        }

        /// <summary>
        /// ��Exception�����У���ȡ������������Ĵ������
        /// </summary>
        /// <param name="ex">Exception����</param>
        /// <returns>������������Ĵ������</returns>
        public static Exception GetRealException(this Exception ex)
        {
            System.Exception lastestEx = ex;

            if (ex is SoapException)
            {
                lastestEx = new SystemSupportException(GetSoapExceptionMessage(ex), ex);
            }
            else
            {
                while (ex != null &&
                    (ex is System.Web.HttpUnhandledException || ex is System.Web.HttpException || ex is TargetInvocationException))
                {
                    if (ex.InnerException != null)
                        lastestEx = ex.InnerException;
                    else
                        lastestEx = ex;

                    ex = ex.InnerException;
                }
            }

            return lastestEx;
        }

        /// <summary>
        /// �õ�SoapException�еĴ�����Ϣ
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetSoapExceptionMessage(Exception ex)
        {
            string strNewMsg = ex.Message;

            if (ex is SoapException)
            {
                int i = strNewMsg.LastIndexOf("--> ");

                if (i > 0)
                {
                    strNewMsg = strNewMsg.Substring(i + 4);
                    i = strNewMsg.IndexOf(": ");

                    if (i > 0)
                    {
                        strNewMsg = strNewMsg.Substring(i + 2);

                        i = strNewMsg.IndexOf("\n   ");

                        strNewMsg = strNewMsg.Substring(0, i);
                    }
                }
            }

            return strNewMsg;
        }

        /// <summary>
        /// ִ��һ�����׳��쳣�Ĳ���
        /// </summary>
        /// <param name="action"></param>
        public static void DoSilentAction(Action action)
        {
            if (action != null)
            {
                try
                {
                    action();
                }
                catch (System.Exception)
                {
                }
            }
        }

        /// <summary>
        /// ִ��һ�����׳��쳣�ĺ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T DoSilentFunc<T>(Func<T> func, T defaultValue)
        {
            T result = defaultValue;

            if (func != null)
                try
                {
                    result = func();
                }
                catch (System.Exception)
                {
                }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        /// <returns>���ش����Exception</returns>
        public static System.Exception WriteToEventLog(this System.Exception ex, string source)
        {
            source.CheckStringIsNullOrEmpty("source");
            ex.NullCheck("ex");

            DoSilentAction(() => EventLog.WriteEntry(source, BuildExceptionWithEnvironmentInfo(ex)));

            return ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="entryType"></param>
        /// <param name="ex"></param>
        /// <returns>���ش����Exception</returns>
        public static System.Exception WriteToEventLog(this System.Exception ex, string source, EventLogEntryType entryType)
        {
            source.CheckStringIsNullOrEmpty("source");
            ex.NullCheck("ex");

            DoSilentAction(() => EventLog.WriteEntry(source, BuildExceptionWithEnvironmentInfo(ex), entryType));

            return ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="entryType"></param>
        /// <param name="eventID"></param>
        /// <param name="ex"></param>
        /// <returns>���ش����Exception</returns>
        public static System.Exception WriteToEventLog(this System.Exception ex, string source, EventLogEntryType entryType, int eventID)
        {
            source.CheckStringIsNullOrEmpty("source");
            ex.NullCheck("ex");

            DoSilentAction(() => EventLog.WriteEntry(source, BuildExceptionWithEnvironmentInfo(ex), entryType, eventID));

            return ex;
        }

        private static string BuildExceptionWithEnvironmentInfo(System.Exception ex)
        {
            StringBuilder strB = new StringBuilder();

            using (StringWriter sw = new StringWriter(strB))
            {
                sw.WriteLine(GetEnvironmentInfo());
                sw.WriteLine(ex.ToString());
            }

            return strB.ToString();
        }

        private static string GetEnvironmentInfo()
        {
            string envInfo = string.Empty;

            if (HostingEnvironment.IsHosted)
                envInfo = HostingEnvironment.ApplicationVirtualPath;
            else
                envInfo = AppDomain.CurrentDomain.SetupInformation.ApplicationName;

            return envInfo;
        }
    }
}
