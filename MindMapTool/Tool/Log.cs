using Avalonia.Threading;
using Microsoft.IdentityModel.Logging;
using MindMapTool.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Controls;

namespace MindMapTool.Tool
{
    /// <summary>
    /// 日志弹窗通知管理类
    /// </summary>
    public class Log
    {
        #region ---------------------------------------日志输出模式----------------------------------

        public static void info(string message)
        {
            //输出日志和文件
            pro("Info:" + message).Info("Info:" + message);

        }


        public static void error(string message)
        {
            pro("Error:" + message).Error("Error:" + message);
            MessageBox.ShowAsync(message, "Error", MessageBoxIcon.Warning, MessageBoxButton.OK);
        }
        public static void warn(string message)
        {
            pro("Warn:" + message).Warn("Warn:" + message);
        }

        public static void debug(string message)
        {
            pro("Debug:" + message).Debug("Debug:" + message);
        }

        #endregion
        public static Logger pro(string message = "")
        {
            //创建一个配置文件对象
            var config = new NLog.Config.LoggingConfiguration();
            //创建日志写入目的地
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"logs/{DateTime.Now:yyyy-MM-dd}.txt" };
            //添加日志路由规则
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            //配置文件生效
            LogManager.Configuration = config;
            //创建日志记录对象方法
            Logger Logger = LogManager.GetCurrentClassLogger();
            Debug.WriteLine(message);
            return Logger;
        }
    }
    public class CustomDrawerNavigationCommand 
    {
        /// <summary>
        /// 打开日志文件夹
        /// </summary>

        public void Execute()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = currentDirectory + "logs\\";
            Process.Start("explorer.exe", logDirectory);
        }
    }
}
