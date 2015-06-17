using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace WindowApp.Zebra.PrintHelper
{
    public static class LPTPrinterHelper
    {
        #region 写打印口(LPT)方法
        private const short FILE_ATTRIBUTE_NORMAL = 0x80;
        private const short INVALID_HANDLE_VALUE = -1;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint CREATE_NEW = 1;
        private const uint CREATE_ALWAYS = 2;
        private const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(string strFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr intptrSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr intptrTemplateFile);

        #endregion

        /// <summary>
        /// 通过LPT口进行打印
        /// </summary>
        /// <param name="cmdBytes">传递需要打印的字节数组</param>
        /// <param name="lptPort">LPT端口 如 LPT1</param>
        /// <returns></returns>
        public static bool LPTPrint(byte[] cmdBytes,string lptPort)
        {
            bool result = false;
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            SafeFileHandle handle = null;
            try
            {
                handle = CreateFile(lptPort, GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                if (!handle.IsInvalid)
                {
                    fileStream = new FileStream(handle, FileAccess.ReadWrite);
                    streamWriter = new StreamWriter(fileStream, Encoding.Default);
                    streamWriter.Write(cmdBytes);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream = null;
                }
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (handle != null)
                {
                    handle.Close();
                    handle = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">需要打印的原始命令</param>
        /// <param name="lptPort">LPT端口 如 LPT1</param>
        /// <returns></returns>
        public static bool LPTPrint(string command, string lptPort)
        {
            return LPTPrint(System.Text.Encoding.Default.GetBytes(command), lptPort);
        }
    }
}
