using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FingerWcfService
{
    /// <summary> 客户端连接服务端规则 </summary>
    public class WcfRegisterConfiger
    {
        private WcfRegisterConfiger()
        {

        }

        public static WcfRegisterConfiger Instance = new WcfRegisterConfiger();

        /// <summary> 服务端IP </summary>
        public string IP = "127.0.0.1";
      
        /// <summary> 通信端口号 </summary>
        public string Port = "22889";

        /// <summary> 服务名 </summary>
        public string SvcName;

        /// <summary> 连接地址规则 </summary>
        public static string RomoteFormat = "http://{0}:{1}/{2}";
    }
}
