using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spoonson.Apps.SuperTerm
{
    /// <summary>
    /// Telnet 控制方法
    /// </summary>
    interface ITelnetControl
    {
        /// <summary>
        /// 等待接收字符串
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="strText"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool WaitString(int x,int y, string strText, int timeout);

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        string GetString(int row, int col, int len);

        /// <summary>
        /// 发送用户命令
        /// </summary>
        /// <param name="bs"></param>
        bool SendMessage(byte[] bs);

        /// <summary>
        /// 输出文件数据路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool SetOutputFileName(string filename);

        /// <summary>
        /// 获取打印状态
        /// </summary>
        /// <returns></returns>
        bool IsPrinting { get; set; }

        /// <summary>
        /// 睡眠等待
        /// </summary>
        /// <param name="ms"></param>
        void Sleep(int ms);

    }
}
