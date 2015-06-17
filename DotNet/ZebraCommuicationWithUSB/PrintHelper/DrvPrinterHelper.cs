using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace WindowApp.Zebra.PrintHelper
{
    /// <summary>
    /// 定义打印文档信息类
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DocInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string DocName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string OutputFile;
        [MarshalAs(UnmanagedType.LPStr)]
        public string DataType;
    }

    /// <summary>
    /// 原始系统的打印帮助类
    /// 其它帮助类，都是根据此类进行封装 
    /// </summary>
    public static class DrvPrinterHelper
    {
        #region 定义API方法

        #region 写打印口(Drv)方法

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string printerName, out IntPtr intptrPrinter, IntPtr intptrPrintDocument);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr intptrPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr intptrPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DocInfo docInfo);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr intptrPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr intptrPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr intptrPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr intptrPrinter, IntPtr intptrBytes, Int32 count, out Int32 written);

        #endregion

        #endregion

        /// <summary>
        /// 发送字节流到打印机
        /// 不管是打印字符串，打印文件，最终会转换成字节流，调用此方法
        /// 注意，此方法为私有，客户端不应看到，下面3个方法对其进行封装
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <param name="intptrBytes">字节流的指针地址</param>
        /// <param name="count">字节流的长度</param>
        /// <returns>是否打印成功</returns>
        private static bool SendBytesToPrinter(string printerName, IntPtr intptrBytes, Int32 count)
        {
            Int32 error = 0, written = 0;
            IntPtr intptrPrinter = new IntPtr(0);
            DocInfo docInfo = new DocInfo();
            bool bSuccess = false;

            docInfo.DocName = ".NET RAW Document";
            docInfo.DataType = "RAW";

            // Open the printer.
            if (OpenPrinter(printerName.Normalize(), out intptrPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(intptrPrinter, 1, docInfo))
                {
                    // Start a page.
                    if (StartPagePrinter(intptrPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(intptrPrinter, intptrBytes, count, out written);
                        EndPagePrinter(intptrPrinter);
                    }
                    EndDocPrinter(intptrPrinter);
                }
                ClosePrinter(intptrPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                error = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        /// <summary>
        /// 通过字节流进行打印
        /// 此字节流应该是对 原始打印机代码 的编码
        /// 例如，可以通过将图片编码转换为打印机能够识别的 字节流进行打印
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <param name="bytes">字节流</param>
        /// <returns>是否打印成功</returns>
        public static bool SendBytesToPrinter(string printerName, byte[] bytes)
        {
            bool bSuccess = false;
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength = bytes.Length;
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(printerName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        /// <summary>
        /// 通过指定特定的文本字符串发送
        /// 文本字符串应为 打印机 原始代码
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <param name="command">需要打印的原始打印机代码</param>
        /// <returns>是否打印成功</returns>
        public static bool SendStringToPrinter(string printerName, string command)
        {
            return SendBytesToPrinter(printerName, System.Text.Encoding.Default.GetBytes(command));
        }
    }
}
