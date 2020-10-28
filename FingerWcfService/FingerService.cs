using FingerUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace FingerWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class FingerService : IFingerService
    {
        public ResultMessage PostEntity(MessageEntity message)
        {
            LoggerHelper.Instance.Info("message:" + message.message);
            LoggerHelper.Instance.Info("type:" + message.type);
            LoggerHelper.Instance.Info("level:" + message.level);

            return ResultMessage.Create();
        }

        public ResultMessage PostMessage(string message, string type, string level)
        {
            LoggerHelper.Instance.Info("message:" + message);
            LoggerHelper.Instance.Info("type:" + type);
            LoggerHelper.Instance.Info("level:" + level);

            //return DataManager.Instance.JsonResult();

            return ResultMessage.Create();

        }

        public string PostMMM(string message)
        {
            LoggerHelper.Instance.Info("message:" + message);

            return "TRUE";
        }

        public string TestGet()
        {
            Console.WriteLine("TestGet成功");

            LoggerHelper.Instance.Info("TestGet成功");

            return "TestGet成功";
        }

        public string TestPost(string message)
        {
            Console.WriteLine(message);

            LoggerHelper.Instance.Info(message);

            return "成功";
        }

        public ResultMessage FingerprintGet()
        {
            try
            {
                LoggerHelper.Instance.Info("FingerprintGet成功进入");
                IntPtr hHandle = new IntPtr();
                bool isusb;
                bool isconnect;
                int retusb;
                retusb = FingerCommonHelper.connect_usb(ref hHandle);
                LoggerHelper.Instance.Info("FingerprintGet成功进入1");
                if (retusb != 0)
                { 
                    FingerCommonHelper.connect_close(ref hHandle);
                    return new ResultMessage {
                        Code = "412",
                        Message = "打开USB设备失败,请查看设备是否连接"
                    }; 
                }
                isusb = true;
                isconnect = true;

                int IMAGE_SIZE = (256 * 288);
                UInt32 nDevAddr = 0xffffffff;
                int ret;
                byte[] ImgData = new byte[IMAGE_SIZE];
                int[] ImgLen = new int[1];
                int timeout = 20;   //定义等待超时

                BEIG1:
                    ret = FingerDllHelper.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
                    if (ret == 0)
                    {
                        //获取成功
                        ret = FingerDllHelper.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
                        if (ret == 0)
                        {
                            //返回图片流
                            return new ResultMessage
                            {
                                Code = "200",
                                Data = ImgData,
                                Message = "获取指纹图像成功"
                            };
                        }
                    }
                    else
                    {
                        if (timeout < 0)
                        {
                            return new ResultMessage
                            {
                                Code = "412",
                                Message = "等待超时，请将手指平放在传感器上"
                            };
                        }
                        timeout--;
                        Thread.Sleep(10);
                        goto BEIG1;
                    }                             
            }
            catch (Exception ex)
            {
                return new ResultMessage
                {
                    Code = "500",
                    Message = ex.Message
                };
            }
            return null;
        }

        
    }
}
