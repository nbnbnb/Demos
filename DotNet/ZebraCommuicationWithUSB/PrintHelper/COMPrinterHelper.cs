using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowApp.Zebra.PrintHelper
{
    public static class COMPrinterHelper
    {
        /// <summary>
        /// 通过COM口打印
        /// </summary>
        /// <param name="cmdBytes">需要打印的字节流</param>
        /// <param name="comPort">串口名称 如 COM1</param>
        /// <returns>是否打印成功</returns>
        public static bool COMPrint(byte[] cmdBytes,string comPort)
        {
            bool result = false;
            SerialPort com = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);
            try
            {
                com.Open();
                com.Write(cmdBytes, 0, cmdBytes.Length);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (com.IsOpen)
                {
                    com.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 通过COM口打印
        /// </summary>
        /// <param name="command">需要打印的原始命令，可以由软件导出</param>
        /// <param name="comPort">串口名称 如 COM1</param>
        /// <returns></returns>
        public static bool COMPrint(string command, string comPort)
        {
            return COMPrint(System.Text.Encoding.Default.GetBytes(command), comPort);
        }
    }
}
