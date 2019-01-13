using System;
using System.Text;
namespace UserSpace
{
    public class UserCode:MarshalByRefObject
    {
        private object m_telnet = null;

        public UserCode(object telnet)
        {
            m_telnet = telnet;
            Console.WriteLine("running userscript...");
            UserScript();
            Console.WriteLine("userscript finished!");
        }

        #region 获取状态
        /// <summary>
        /// 获取主机路径
        /// </summary>
        public string Host
        {
            get { return (string)GetProperty("Host"); }
        }

        /// <summary>
        /// 主机端口
        /// </summary>
        public int Port
        {
            get { return (int)GetProperty("Port"); }
        }

        /// <summary>
        /// 标识是否在打印文件
        /// </summary>
        public bool IsPrinting
        {
            get { return (bool)GetProperty("IsPrinting"); }
        }

        /// <summary>
        /// 当前光标的X位置
        /// </summary>
        public int CursorX
        {
            get { return (int)GetProperty("CursorX"); }
        }

        /// <summary>
        /// 当前光标Y位置
        /// </summary>
        public int CursorY
        {
            get { return (int)GetProperty("CursorY"); }
        }

        /// <summary>
        /// 获取指定位置的字符串
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string GetString(int row, int col, int len)
        {
            return (string)InvokeMethod("GetString", new object[] { row, col, len });
        }

        /// <summary>
        /// 等待指定字符串
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="text"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitString(int row, int col, string text, int timeout)
        {
            int len = text.Length;

            int cnt = timeout / 100;
            while (cnt-- > 0)
            {
                string scr = GetString(row, col, len);
                if (scr == text) return true;
                Sleep(100);
            }
            return false;
        }
        #endregion

        #region 控制操作
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(string data)
        {
            return (bool)InvokeMethod("SendData", new object[] { Encoding.ASCII.GetBytes(data) });
        }

        /// <summary>
        /// 设置文件输出路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SetOutputFileName(string filename)
        {
            return (bool)InvokeMethod("SetOutputFileName", new object[] { filename });
        }

        /// <summary>
        /// 休眠一段时间
        /// </summary>
        /// <param name="ms"></param>
        public void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        /// <summary>
        /// 连接主机
        /// </summary>
        public void Connect()
        {
            InvokeMethod("Connect", null);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            InvokeMethod("Close", null);
        }
        #endregion

        #region 反射调用方法
        private object InvokeMethod(string method,object[] param=null)
        {
            var sender = m_telnet;
            return sender.GetType().InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, sender, param);
        }

        private object GetProperty(string name, object[] param=null)
        {
            var sender = m_telnet;
            return sender.GetType().InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, sender, param);
        }

        private void SetProperty(string name,object[] param=null)
        {
            var sender = m_telnet;
            sender.GetType().InvokeMember(name, System.Reflection.BindingFlags.SetProperty, null, sender, param);
        }
        #endregion
    }
}
