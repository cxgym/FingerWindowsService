using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FingerUtilities
{
    public class FingerCommonHelper
    {
        public static int connect_usb(ref IntPtr hHandle)
        {
            LoggerHelper.Instance.Info("FingerprintGet成功进入2");
            int ret = 0;
            int devce_usb = 0;
            byte[] iPwd = new byte[4];
            uint nDevAddr = 0xffffffff;
            // Fingerdll.ZAZCloseDeviceEx(hHandle);

            ret = FingerDllHelper.ZAZOpenDeviceEx(ref hHandle, 0, 0, 0, 2, 0);
            LoggerHelper.Instance.Info("FingerprintGet成功进入3");
            if (ret == 0)
            {
                devce_usb = 2;  //无驱
            }
            else
            {
                ret = FingerDllHelper.ZAZOpenDeviceEx(ref hHandle, 2, 0, 0, 2, 0);
                if (ret == 0)
                {
                    devce_usb = 2;  //无驱
                }
            }
            if (devce_usb == 1)
            {
                if (FingerDllHelper.ZAZVfyPwd(hHandle, nDevAddr, iPwd) == 0)
                {
                    //	ShowInfomation("指昂有驱USB指纹设备",RGB(0,0,255));	
                    return 0;
                }
                else
                { 
                    return 1; 
                }

            }
            else if (devce_usb == 2)
            {
                return 0;
            }
            else
            {               
                return 1;              
            }
        }

        public static void connect_close(ref IntPtr hHandle)
        {
            FingerDllHelper.ZAZCloseDeviceEx(hHandle);
        }

    }
}
