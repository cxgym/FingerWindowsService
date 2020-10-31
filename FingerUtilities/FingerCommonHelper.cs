using Microsoft.Win32;
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

        /// <summary>
        /// 获取所有已经安装的程序
        /// </summary>
        /// <returns>程序名称,安装路径</returns>
        public static List<Dictionary<string, string>> GetProgramAndPath()
        {
            var reg = new string[] {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            string tempType = null;
            int softNum = 0;   //所有已经安装的程序数量
            RegistryKey currentKey = null;
            var ls = new List<Dictionary<string, string>>();

            foreach (var item0 in reg)
            {
                object displayName = null, uninstallString = null, installLocation = null, releaseType = null;
                RegistryKey pregkey = Registry.LocalMachine.OpenSubKey(item0);//获取指定路径下的键 
                foreach (string item1 in pregkey.GetSubKeyNames())               //循环所有子键
                {
                    currentKey = pregkey.OpenSubKey(item1);
                    displayName = currentKey.GetValue("DisplayName");           //获取显示名称
                    installLocation = currentKey.GetValue("InstallLocation");   //获取安装路径
                    uninstallString = currentKey.GetValue("UninstallString");   //获取卸载字符串路径
                    releaseType = currentKey.GetValue("ReleaseType");           //发行类型,值是Security Update为安全更新,Update为更新
                    bool isSecurityUpdate = false;
                    if (releaseType != null)
                    {
                        tempType = releaseType.ToString();
                        if (tempType == "Security Update" || tempType == "Update")
                        {
                            isSecurityUpdate = true;
                        }
                    }
                    if (!isSecurityUpdate && displayName != null && uninstallString != null)
                    {
                        softNum++;
                        if (installLocation == null)
                        {
                            ls.Add(new Dictionary<string, string> { { displayName.ToString(), "" } });
                        }
                        else
                        {
                            ls.Add(new Dictionary<string, string> { { displayName.ToString(), installLocation.ToString() } });
                        }
                    }
                }
            }
            return ls;
        }

    }
}
