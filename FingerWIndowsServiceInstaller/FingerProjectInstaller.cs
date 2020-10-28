using FingerUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;

/// <summary>
/// 添加->新建项->安装程序类
/// </summary>
namespace FingerWIndowsServiceInstaller
{
    [RunInstaller(true)]
    public partial class FingerProjectInstaller : System.Configuration.Install.Installer
    {
        //private string TARGETDIR { get; set; }

        public FingerProjectInstaller()
        {
            InitializeComponent();
        }
       
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }
       
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);           
            //TARGETDIR = stateSaver["TARGETDIR"] as string;
        }
      
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
        
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
       
        protected override void OnAfterInstall(IDictionary savedState)
        {
            //获取用户设定的安装目标路径, 注意
            //需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\" 
            string path = this.Context.Parameters["targetdir"];

            base.OnAfterInstall(savedState);

            var files = Directory.GetFiles(path).Where(w => w.ToLower().EndsWith(".exe") || w.ToLower().EndsWith(".dll"));
            var serviceBase = typeof(ServiceBase);
            foreach (var item in files)
            {
                var assembly = Assembly.LoadFile(item);
                var type = assembly.GetTypes().FirstOrDefault(f => f.BaseType == serviceBase);
                if (type != null)
                {
                    var service = (ServiceBase)assembly.CreateInstance(type.FullName);
                    var installer = new AssemblyInstaller();
                    //IDictionary savedState1 = new Hashtable();
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
                        //安装服务                  
                        installer.Install(savedState);
                        installer.Commit(savedState);
                        //启动服务
                        Process.Start("sc", "start FingerService");
                    }
                    break;
                };
            }
        }

        protected override void OnAfterRollback(IDictionary savedState)
        {
            base.OnAfterRollback(savedState);
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            //获取用户设定的安装目标路径, 注意
            //需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\" 
            string path = this.Context.Parameters["targetdir"];

            base.OnAfterUninstall(savedState);

            var files = Directory.GetFiles(path).Where(w => w.ToLower().EndsWith(".exe"));
            var serviceBase = typeof(ServiceBase);
            foreach (var item in files)
            {
                var assembly = Assembly.LoadFile(item);
                var type = assembly.GetTypes().FirstOrDefault(f => f.BaseType == serviceBase);
                if (type != null)
                {
                    var installer = new AssemblyInstaller();                   
                    installer.UseNewContext = true;
                    installer.Path = item;
                    //停止服务
                    Process.Start("sc", "stop FingerService");
                    //卸载服务
                    installer.Uninstall(savedState);
                    break;
                }                   
            }             
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
        }
  
        protected override void OnBeforeRollback(IDictionary savedState)
        {
            base.OnBeforeRollback(savedState);
        }
    
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
        }
  
        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
        }
      
        protected override void OnCommitting(IDictionary savedState)
        {
            base.OnCommitting(savedState);
        }

    }
}
