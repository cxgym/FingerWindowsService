using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FingerUtilities
{
    public class LoggerHelper
    {
        public static LoggerHelper Instance = new LoggerHelper();

        public void InitLogger()
        {
            string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = Path.GetDirectoryName(exeFileFullPath);

            binPath = Path.GetDirectoryName(binPath);

            string logFilePath = Path.GetDirectoryName(binPath);

            var exe = System.Diagnostics.Process.GetCurrentProcess();

            if (exe == null) return;

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string logPath = Path.Combine(binPath, "Logs", exeName);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //  初始化日志
            Log4Servcie.Instance.InitLogger(logPath, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
        }

        /// <summary> 运行日志 </summary>
        public void Info(params string[] message)
        {
            Log4Servcie.Instance.Info(message);
        }

        /// <summary> 错误日志 </summary>
        public void Error(params Exception[] ex)
        {
            Log4Servcie.Instance.Error(ex);
        }

        public void Error(string message, Exception ex)
        {
            Log4Servcie.Instance.Error(message, ex);
        }
    }
}
