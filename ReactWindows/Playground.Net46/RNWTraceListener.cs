using System;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;

namespace Playground.Net46
{
    public class RNWTraceListener : AbstractTraceSourceTraceListener
    {
        protected string LoggingOutputDirectory = String.Empty;

        private string rollingFileAppenderName = "RNWCustomRollingFileAppender";
        protected override string RollingFileAppenderName => rollingFileAppenderName;

        private string logRecordHeader = "RNWLog";
        protected override string LogRecordHeader => logRecordHeader;

        private ILog _log;
        private ILog _log2;

        protected override ILog LogInctance => _log;

        protected override ILog LogInctance2 => _log2;

        public  RNWTraceListener(string initializeData):base(initializeData)
        {
        }

        protected override void InitializeLogger(string loggingOutputDirectory)
        {
            if (loggingOutputDirectory == null)
            {
                throw new ArgumentNullException(nameof(loggingOutputDirectory));
            }

            //checking path valid
            if (!Directory.Exists(loggingOutputDirectory))
            {
                throw new ArgumentException($"Wrong value for {nameof(loggingOutputDirectory)}. Folder isn't exist");
            }

            this.LoggingOutputDirectory = loggingOutputDirectory;
            string path = String.Format("{0}-{1}-{2}-{3}{4}", "RNW", DateTime.Now.Month.ToString("00")
                                   , DateTime.Now.Day.ToString("00")
                                   , DateTime.Now.Year.ToString(), ".log");
            string path2 = String.Format("{0}-{1}-{2}-{3}{4}", "RNW_2_", DateTime.Now.Month.ToString("00")
                                   , DateTime.Now.Day.ToString("00")
                                   , DateTime.Now.Year.ToString(), ".log");

            LevelMatchFilter filter = new LevelMatchFilter();
            filter.LevelToMatch = Level.All;
            filter.ActivateOptions();

            RollingFileAppender appender = new RollingFileAppender();
            appender.File = System.IO.Path.Combine(String.IsNullOrEmpty(this.LoggingOutputDirectory) ? appender.File : this.LoggingOutputDirectory, path);
            appender.MaxFileSize = 10485760; //10mb
            appender.MaxSizeRollBackups = 3;
            appender.ImmediateFlush = true;
            appender.AppendToFile = true;
            appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
            appender.LockingModel = new FileAppender.MinimalLock();
            appender.Name = RollingFileAppenderName;
            appender.AddFilter(filter);
            appender.Layout = new PatternLayout("%message%newline");
            appender.ActivateOptions();

            ILoggerRepository repository = LoggerManager.CreateRepository("RNWLoggingRepository");
            BasicConfigurator.Configure(repository, appender);

            this._log = LogManager.GetLogger("RNWLoggingRepository", "RNWLog");

            //Another log from config file - only for demo. Should be deleted fro prod
            this._log2 = LogManager.GetLogger("RNWLog2");
            this.ChangeFilePath("RNWRollingFileAppender", path2);
        }

        //needs only for demo another log file. Should be deleted fro prod
        private void ChangeFilePath(string appenderName, string newFilename)
        {
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetRepository();
            foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
            {
                if (appender.Name.CompareTo(appenderName) == 0 && appender is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)appender;
                    fileAppender.File = System.IO.Path.Combine(String.IsNullOrEmpty(this.LoggingOutputDirectory) ? fileAppender.File : this.LoggingOutputDirectory, newFilename);
                    fileAppender.ActivateOptions();
                }
            }
        }
    }
}
