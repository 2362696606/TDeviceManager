using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Reflection;

namespace TCommon.Extensions;

public class LogHelper
{
    /// <summary>
    /// 获取文件日志对象，日志文件存储在"./Logs/{logName}/{datetime}.log"
    /// </summary>
    /// <param name="logName">日志名</param>
    /// <returns></returns>
    public static ILog GetFileLoggerByName(string logName)
    {
        if (LogManager.Exists(logName) == null)
        {
            // Pattern Layout defined
            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%date %thread %level %logger - %message%newline"
            };
            patternLayout.ActivateOptions();
            RollingFileAppender appender = new RollingFileAppender();
            appender.Name = logName;
            appender.LockingModel = new FileAppender.MinimalLock();
            //appender.File = AppDomain.CurrentDomain.BaseDirectory +
            //                $"Logs\\{logName}\\{DateTime.Now:yyyyMMdd}.log";
            appender.File = $"log\\{logName}\\";
            appender.DatePattern = "/yyyy-MM/MM-dd-HH\".log\"";
            appender.AppendToFile = true;// 如果文件存在，新的日志将追加到文件中，而不是覆盖文件
            appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
            appender.PreserveLogFileNameExtension = true;
            appender.StaticLogFileName = false;
            appender.MaximumFileSize = "5MB";
            appender.MaxSizeRollBackups = 20;
            PatternLayout layout = new PatternLayout();
            layout.ConversionPattern = @"%date 线程ID:[%thread][%-5level]:%message[%class:%L]%newline";
            layout.ActivateOptions();
            appender.Layout = layout;// 设置日志的格式
            var filter = new LevelMatchFilter { LevelToMatch = Level.All };
            filter.ActivateOptions();
            appender.AddFilter(filter);
            appender.ActivateOptions();

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            var lodger = hierarchy.GetLogger(logName, hierarchy.LoggerFactory); //!!! 此处写法是重点，不容更改
            lodger.Hierarchy = hierarchy;
            lodger.AddAppender(appender);
            lodger.Level = Level.All;

            BasicConfigurator.Configure();//!!! 此处写法是重点，不容更改

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            lodger.Log(Level.Info, $"Log logName {logName} created for Application: {assemblyName} Version: {version}", null);
        }
        var log = LogManager.GetLogger(logName);
        return log;
    }
}