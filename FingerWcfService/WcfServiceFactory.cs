using FingerUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace FingerWcfService
{
    /// <summary> 注册服务 </summary>
    public class ServiceRegisterService
    {
        public static ServiceRegisterService Instance = new ServiceRegisterService();

        List<ServiceHost> _hosts = new List<ServiceHost>();

        public void RegisterConfigDemo()
        {
            this.RegisterWithConfig<FingerWcfService.FingerService>();
        }

        public void RegisterService<T>()
        {
            this.RegisterWithConfig<T>();
        }

        public void RegisterCodeDemo<T>()
        {
            this.RegisterWithCode<T>();
        }

        /// <summary> 用配置文件注册 （在配置文件中配置） </summary>
        public void RegisterWithConfig<T>()
        {
            try
            {              
                ServiceHost host = new ServiceHost(typeof(T));

                host.Opened += (s, e) =>
                {                   
                    Log4Servcie.Instance.Info("WCF服务启动成功");
                };

                host.Open();
            }
            catch (Exception ex)
            {
                Log4Servcie.Instance.Info("WCF服务启动失败");
                Log4Servcie.Instance.Error(ex);
            }
        }

        /// <summary> 用代码注册（完全脱离配置文件） </summary>
        public void RegisterWithCode<T>()
        {
            try
            {
                Uri uri = new Uri("http://LocalHost:22999/");

                ServiceHost host = new ServiceHost(typeof(T), uri);

                // 要点一：定义元数据发布方式，此处  通过在服务所在的URL后加“?wsdl”的方式公布WSDL，可直接通过HTTP访问得到。
                System.ServiceModel.Description.ServiceMetadataBehavior behavior = new System.ServiceModel.Description.ServiceMetadataBehavior();
                //此处没有定义mex终结点，必须设置HttpGetEnabled为true，否则客户端无法访问服务
                behavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(behavior);

                // 要点二：WebHttpBinding
                ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(T), new WebHttpBinding(), string.Empty);

                // 要点三:
                endpoint.EndpointBehaviors.Add(new WebHttpBehavior());

                //设置wcf支持ajax调用,仅适用于WebHttpBinding
                //System.ServiceModel.Description.WebScriptEnablingBehavior' is only intended for use with WebHttpBinding or similar bindings.
                //endpoint.Behaviors.Add(new WebScriptEnablingBehavior());

                host.Opened += (s, e) =>
                {
                    Log4Servcie.Instance.Info("WCF服务启动成功");
                };
                host.Open();

            }
            catch (Exception ex)
            {
                Log4Servcie.Instance.Info("WCF服务启动失败");
                Log4Servcie.Instance.Error(ex);
            }
        }

        public void CancelRegister()
        {
            foreach (var item in _hosts)
            {
                Log4Servcie.Instance.Info("Window服务停止成功");

                if (item != null)
                {
                    try
                    {
                        item.Close();
                        Log4Servcie.Instance.Info("WCF服务停止成功");
                    }
                    catch (Exception ex)
                    {
                        Log4Servcie.Instance.Info("WCF服务停止失败");
                        Log4Servcie.Instance.Error(ex);
                    }

                }
            }

        }

    }
}
