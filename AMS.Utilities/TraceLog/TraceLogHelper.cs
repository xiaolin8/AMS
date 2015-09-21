using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DotNet.Utilities
{
    /// <summary>
    /// 日志类。利用C#自带的Trace机制实现，使用本类的函数前，必须先注册适当的TraceListener。
    /// </summary>
    public static class TraceLogHelper
    {
        private static object _lockHelper = new object();

        public static ConsoleTraceListener RegisterConsoleListener()
        {
            ConsoleTraceListener listener = new ConsoleTraceListener();
            Trace.Listeners.Add(listener);
            return listener;
        }

        public static LogFileTraceListener RegisterTextWriterListener()
        {
            LogFileTraceListener listener = new LogFileTraceListener();
            Trace.Listeners.Add(listener);
            return listener;
        }

        public static void Error(string message, Exception ex)
        {
            WriteLogger(message, ex, "异常");
        }
        public static void Exception(string message, Exception ex)
        {
            WriteLogger(message, ex, "异常");
            throw ex;
        }
        public static void Exception(string message, List<Parameter> list, Exception ex)
        {
            message = "(1)" + message + "\r\n (2)"; ;

            list.ForEach(
                p => message += "'" + p.Value.ToString() + "',"
                );

            WriteLogger(message, ex, "异常");
            throw ex;
        }

        public static void Exception(string message, object[] list, Exception ex)
        {

            message = "(1)" + message + "\r\n (2)"; ;
            message += "'" + string.Join("','", list.ToArray()) + "'";
            WriteLogger(message, ex, "异常");
            throw ex;
        }


        private static void WriteLogger(string message, Exception exception, string type)
        {
            string userName = string.Empty;
            try
            {
                userName = AppContext.Instance.GetLoginName();
            }
            catch { }
            //lock (_lockHelper)
            //{
            //    Trace.WriteLine(string.Format("[{0}][{1}]:\r\n SQL: \r\n {2}\r\n Exception:\r\n {3}",
            //                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //                          type,
            //                          message+"\r\n ---------------------------------------------------------------------- ",
            //                          exceptionMessage + "\r\n =================================================================================== "));
            //}

            lock (_lockHelper)
            {
                string loggerText = @"*********************************************************************
{0}[{1}]:
【SQL】:
{2}
----------------------------------------------------------------------
【Exception】:
{3}
【Source】:
{4}
【StackTrace】:
{5}
===================================================================================";

                loggerText = string.Format(loggerText, userName + " \r\n " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message, exception.Message, exception.Source, exception.StackTrace);
                Trace.WriteLine(loggerText);
            }
        }
    }

    /// <summary>
    /// 将Trace结果写入一个文件的TraceListener。支持线程同步。
    /// </summary>
    public class LogFileTraceListener : TraceListener
    {

        private string _logFolder = AppDirectoryHelper.GetAppRootDir("LogFile//TraceLog");

        public LogFileTraceListener()
        {

        }

        public override void Write(string message)
        {

            File.AppendAllText(GetTodayLogFileName(), message);
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        private string GetTodayLogFileName()
        {
            string fileName = DateTime.Now.ToString("yyyy.MM.dd HH") + ".Log";
            string fileNameLong = _logFolder + fileName;
            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }
            if (!File.Exists(fileNameLong))
            {
                StreamWriter sw = File.CreateText(fileNameLong);
                sw.Close();
            }
            return fileNameLong;
        }
    }
}