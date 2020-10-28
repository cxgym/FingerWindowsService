using FingerUtilities;
using FingerWcfService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace FingerWindowsService
{
    public partial class FingerService : ServiceBase
    {
        public FingerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Thread.Sleep(30000);
                string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Todo ：初始化 
                LoggerHelper.Instance.InitLogger();

                LoggerHelper.Instance.Info("开始启动服务！");

                ServiceRegisterService.Instance.RegisterConfigDemo();

                LoggerHelper.Instance.Info(documentPath);

                LoggerHelper.Instance.Info("服务启动成功！");

            }
            catch (Exception ex)
            {
                LoggerHelper.Instance.Info("服务启动错误！");
                LoggerHelper.Instance.Error(ex);
            }
        }

        protected override void OnStop()
        {
            LoggerHelper.Instance.Info("服务停止！");
        }
    }
}
