using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows;

namespace FingerWIndowsServiceInstaller
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //private void OnAppStartup(object sender, StartupEventArgs e)
        //{
        //    var files = Directory.GetFiles(Environment.CurrentDirectory).Where(w => w.ToLower().EndsWith(".exe") || w.ToLower().EndsWith(".dll"));
        //    var serviceBase = typeof(ServiceBase);
        //    foreach (var item in files)
        //    {
        //        var assembly = Assembly.LoadFile(item);
        //        var type = assembly.GetTypes().FirstOrDefault(f => f.BaseType == serviceBase);
        //        if (type != null)
        //        {
        //            var service = (ServiceBase)assembly.CreateInstance(type.FullName);
        //            //安装服务
        //            var installer = new AssemblyInstaller();
        //            installer.UseNewContext = true;
        //            installer.Path = item;
        //            IDictionary savedState = new Hashtable();
        //            installer.Install(savedState);
        //            installer.Commit(savedState);
        //            //启动服务
        //            Process.Start("sc", "start FingerService");
        //            //Application.Current.Shutdown();
        //            break;
        //        };
        //    }     
        //}
    }
}
