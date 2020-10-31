using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FingerWIndowsServiceInstaller
{
    /// <summary>
    /// FingerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FingerWindow : Window
    {
        public string FingerWindowContent
        {
            get { return (string)GetValue(ServiceNameProperty); }
            set { SetValue(ServiceNameProperty, value); }
        }

        public static readonly DependencyProperty ServiceNameProperty =
            DependencyProperty.Register("FingerWindowContent", typeof(string), typeof(FingerWindow), new PropertyMetadata(""));

        public FingerWindow()
        {
            InitializeComponent();
            FingerWindowContent = "指 纹 仪 服 务 启 动 中  。。。";
            Thread thread = new Thread(new ThreadStart(CreateProcess));
            thread.Start();
            thread.IsBackground = true;
        }

        private void CreateProcess()
        {
            Thread.Sleep(3000);

            //启动自动检测安装netframework4.5.2


            var files = Directory.GetFiles(Environment.CurrentDirectory).Where(w => w.ToLower().EndsWith(".exe") || w.ToLower().EndsWith(".dll"));
            var serviceBase = typeof(ServiceBase);
            foreach (var item in files)
            {
                var assembly = Assembly.LoadFile(item);
                var type = assembly.GetTypes().FirstOrDefault(f => f.BaseType == serviceBase);
                if (type != null)
                {
                    var service = (ServiceBase)assembly.CreateInstance(type.FullName);
                    var installer = new AssemblyInstaller();
                    IDictionary savedState = new Hashtable();
                    installer.UseNewContext = true;
                    installer.Path = item;

                    //判断服务是否存在启动
                    if (ServiceController.GetServices().Count(p => p.ServiceName == "FingerService") > 0)
                    {
                        if (ServiceController.GetServices().Count(p => p.Status == ServiceControllerStatus.Running) == 0)
                        {
                            //启动服务
                            Process.Start("sc", "start FingerService");
                        }
                    }
                    else
                    {
                        //停止服务
                        //Process.Start("sc", "stop FingerService");
                        //卸载服务
                        //installer.Uninstall(savedState);

                        //安装服务                  
                        installer.Install(savedState);
                        installer.Commit(savedState);
                        //启动服务
                        Process.Start("sc", "start FingerService");
                    }

                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        FingerWindowContent = $"指 纹 仪 服 务 启 动 成 功 ！";
                    });

                    Thread.Sleep(1000);
                    break;
                };
            }
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.Close();
            });
        }
    }
}
