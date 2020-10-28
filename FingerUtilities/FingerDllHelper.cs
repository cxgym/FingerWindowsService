using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FingerUtilities
{
    public class FingerDllHelper
    {
        [DllImport("ZAZAPIt.dll")]//打开设备
        public unsafe static extern int ZAZOpenDeviceEx(ref IntPtr pHandle, int nDeviceType, int iCom, int iBaud, int nPackageSize, int iDevNum);
        [DllImport("ZAZAPIt.dll")]//关闭设备
        public unsafe static extern int ZAZCloseDeviceEx(IntPtr pHandle);

        [DllImport("ZAZAPIt.dll")]//检测手指并录取图像
        public unsafe static extern int ZAZGetImage(IntPtr pHandle, UInt32 nAddr);
        [DllImport("ZAZAPIt.dll")]//根据原始图像生成指纹特征
        public unsafe static extern int ZAZGenChar(IntPtr pHandle, UInt32 nAddr, int iBufferID);
        [DllImport("ZAZAPIt.dll")]//直接在窗体上显示指纹图像
        public unsafe static extern int ZAZShowFingerData(IntPtr hWnd, byte[] pFingerData);
        [DllImport("ZAZAPIt.dll")]//将CharBufferA与CharBufferB中的特征文件合并生成模板存于ModelBuffer
        public unsafe static extern int ZAZRegModule(IntPtr pHandle, UInt32 nAddr);
        [DllImport("ZAZAPIt.dll")]//将ModelBuffer中的文件储存到flash指纹库中
        public unsafe static extern int ZAZStoreChar(IntPtr pHandle, UInt32 nAddr, int iBufferID, int iPageID);
        [DllImport("ZAZAPIt.dll")]//精确比对CharBufferA与CharBufferB中的特征文件
        public unsafe static extern int ZAZMatch(IntPtr pHandle, UInt32 nAddr, int[] iScore);
        [DllImport("ZAZAPIt.dll")]//以CharBufferA或CharBufferB中的特征文件搜索整个或部分指纹库
        public unsafe static extern int ZAZSearch(IntPtr pHandle, UInt32 nAddr, int iBufferID, int iStartPage, int iPageNum, int[] iMbAddress, int[] iscore);
        //++高速搜索
        [DllImport("ZAZAPIt.dll")]//以CharBufferA或CharBufferB中的特征文件搜索整个或部分指纹库
        public unsafe static extern int ZAZHighSpeedSearch(IntPtr pHandle, UInt32 nAddr, int iBufferID, int iStartPage, int iPageNum, int[] iMbAddress, int[] iscore);

        [DllImport("ZAZAPIt.dll")]//删除flash指纹库中的一个特征文件
        public unsafe static extern int ZAZDelChar(IntPtr pHandle, UInt32 nAddr, int iStartPageID, int nDelPageNum);
        [DllImport("ZAZAPIt.dll")]//清空flash指纹库
        public unsafe static extern int ZAZEmpty(IntPtr pHandle, UInt32 nAddr);
        [DllImport("ZAZAPIt.dll")]//读参数表
        public unsafe static extern int ZAZReadParTable(IntPtr pHandle, UInt32 nAddr, Byte[] pParTable);
        [DllImport("ZAZAPIt.dll")]//++读Flash
        public unsafe static extern int ZAZReadInfPage(IntPtr pHandle, UInt32 nAddr, Byte[] pInf);
        [DllImport("ZAZAPIt.dll")]//++读有效模板个数
        public unsafe static extern int ZAZTemplateNum(IntPtr pHandle, UInt32 nAddr, int[] iMbNum);
        [DllImport("ZAZAPIt.dll")]//++写模块寄存器
        public unsafe static extern int ZAZWriteReg(IntPtr pHandle, UInt32 nAddr, int iRegAddr, int iRegValue);
        [DllImport("ZAZAPIt.dll")]//写模块寄存器－波特率设置
        public unsafe static extern int ZAZSetBaud(IntPtr pHandle, UInt32 nAddr, int nBaudNum);
        [DllImport("ZAZAPIt.dll")]//写模块寄存器－安全等级设置
        public unsafe static extern int ZAZSetSecurLevel(IntPtr pHandle, UInt32 nAddr, int nLevel);
        [DllImport("ZAZAPIt.dll")]//写模块寄存器－数据包大小设置
        public unsafe static extern int ZAZSetPacketSize(IntPtr pHandle, UInt32 nAddr, int nSize);
        [DllImport("ZAZAPIt.dll")]//获取随机数
        public unsafe static extern int ZAZGetRandomData(IntPtr pHandle, UInt32 nAddr, byte[] pRandom);
        [DllImport("ZAZAPIt.dll")]//设置芯片地址
        public unsafe static extern int ZAZSetChipAddr(IntPtr pHandle, UInt32 nAddr, byte[] pChipAddr);
        [DllImport("ZAZAPIt.dll")]//读模版索引表	nPage,0,1,2,3对应模版从0-256，256-512，512-768，768-1024
        public unsafe static extern int ZAZReadIndexTable(IntPtr pHandle, UInt32 nAddr, int nPage, byte[] UserContent);
        [DllImport("ZAZAPIt.dll")]//设置红绿灯
        public unsafe static extern int ZAZDoUserDefine(IntPtr pHandle, UInt32 nAddr, int GPIO, int STATE);
        [DllImport("ZAZAPIt.dll")]//从flash指纹库中读取一个模板到ModelBuffer
        public unsafe static extern int ZAZLoadChar(IntPtr pHandle, UInt32 nAddr, int iBufferID, int iPageID);
        [DllImport("ZAZAPIt.dll")]//将特征缓冲区中的文件上传给上位机
        public unsafe static extern int ZAZUpChar(IntPtr pHandle, UInt32 nAddr, int iBufferID, Byte[] pTemplet, int[] iTempletLength);
        [DllImport("ZAZAPIt.dll")]//从上位机下载一个特征文件到特征缓冲区
        public unsafe static extern int ZAZDownChar(IntPtr pHandle, UInt32 nAddr, int iBufferID, Byte[] pTemplet, int iTempletLength);
        [DllImport("ZAZAPIt.dll")]//上传原始图像
        public unsafe static extern int ZAZUpImage(IntPtr pHandle, UInt32 nAddr, byte[] pImageData, int[] iTempletLength);
        [DllImport("ZAZAPIt.dll")]//上传原始图像
        public unsafe static extern int ZAZDownImage(IntPtr pHandle, UInt32 nAddr, byte[] pImageData, int[] iTempletLength);
        [DllImport("ZAZAPIt.dll")]//上传原始图像
        public unsafe static extern int ZAZImgData2BMP(byte[] pImgData, string pImageFile);
        [DllImport("ZAZAPIt.dll")]//下载原始图像
        public unsafe static extern int ZAZGetImgDataFromBMP(IntPtr pHandle, string[] pImageFile, byte[] pImageData, int[] pnImageLen);
        [DllImport("ZAZAPIt.dll")]//读记事本
        public unsafe static extern int ZAZReadInfo(IntPtr pHandle, UInt32 nAddr, int nPage, byte[] UserContent);
        [DllImport("ZAZAPIt.dll")]//写记事本
        public unsafe static extern int ZAZWriteInfo(IntPtr pHandle, UInt32 nAddr, int nPage, byte[] UserContent);
        [DllImport("ZAZAPIt.dll")]//设置设备握手口令
        public unsafe static extern int ZAZSetPwd(IntPtr pHandle, UInt32 nAddr, byte[] pPassword);
        [DllImport("ZAZAPIt.dll")]//验证设备握手口令
        public unsafe static extern int ZAZVfyPwd(IntPtr pHandle, UInt32 nAddr, byte[] pPassword);
        [DllImport("ZAZAPIt.dll")]//上传指纹为.BAT文件
        public unsafe static extern int ZAZUpChar2File(IntPtr pHandle, UInt32 nAddr, int iBufferID, string[] pFileName);
        [DllImport("ZAZAPIt.dll")]//.BAT文件转指纹
        public unsafe static extern int ZAZDownCharFromFile(IntPtr pHandle, UInt32 nAddr, int iBufferID, string[] pFileName);
        [DllImport("ZAZAPIt.dll")]//设置特征模版库的大小 通用-512 AES1711-1024
        public unsafe static extern int ZAZSetCharLen(int nLen);
        [DllImport("ZAZAPIt.dll")]//获取1枚特征的大小
        public unsafe static extern int ZAZGetCharLen(int[] nLen);
        [DllImport("ZAZAPIt.dll")]
        public unsafe static extern int ZAZSetledsound(IntPtr pHandle, UInt32 nAddr, byte red, byte green, byte sound, byte moveflag, string[] pFileName);

        [DllImport("ZAZAPIt.dll")]
        public unsafe static extern int ZAZTestUsb();

        [DllImport("ZAZAPIt.dll")]//获取1枚特征的大小
        public unsafe static extern String ZAZErr2Str(int nErrCode);

        [DllImport("eAlgDLL.dll")]//获取1枚特征的大小
        public unsafe static extern int CharMatch(Byte[] srcData, Byte[] dstData);

        //[DllImport("synoDll.dll")]//获取1枚特征的大小
        //public unsafe static extern int MatchScore(Byte[] srcData, Byte[] dstData); 
        [DllImport("ARTH_DLL.dll")]//获取1枚特征的大小
        public unsafe static extern int Match2Fp(Byte[] srcData, Byte[] dstData);

        [DllImport("ARTH_DLL.dll")]//获取1枚特征的大小
        public unsafe static extern int UserMatch(Byte[] Src, Byte[] Dst, Byte SecuLevel, int[] MatchScore);

        public static string ZAZErr2Strt(int nErrCode)
        {
            return ZAZErr2Str(nErrCode);
        }
    }
}
