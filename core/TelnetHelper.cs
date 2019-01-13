using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using Spoonson.Common;

namespace Spoonson.Apps.SuperTerm
{
    /// <summary>
    /// 打印前的事件触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void BeginPrintEventHandler(object sender, PrintEventArgs args);

    /// <summary>
    /// 打印后的事件触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void EndPrintEventHandler(object sender, PrintEventArgs args);

    /// <summary>
    /// 连接关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ConnectionClosedEventHandler(object sender, EventArgs args);

    public class TelnetHelper : MarshalByRefObject, IDisposable
    {
        #region 常量参数
        //TELNET 命令列表
        const byte EOF = 236;//文件结束符
        const byte SUSP = 237;//挂起当前进程
        const byte ABORT = 238;//终止进程
        const byte EOR = 239;//记录结束符
        const byte SE = 240;//子选项结束
        const byte NOP = 241;//空操作
        const byte DM = 242;//数据标记
        const byte BRK = 243;//终止符（break）
        const byte IP = 244;//终止进程
        const byte AO = 245;//终止输出
        const byte AYT = 246;//请求应答
        const byte EC = 247;//终止符
        const byte EL = 248;//擦除一行
        const byte GA = 249;//继续
        const byte SB = 250;//子选项开始
        const byte WILL = 251;//选项协商
        const byte WONT = 252;//选项协商
        const byte DO = 253;//选项协商
        const byte DONT = 254;//选项协商
        const byte IAC = 255;//命令起始符号

        //子协商选项
        const byte ECHO = 1;//回显
        const byte NOGA = 3;//禁止连续
        const byte STATUS = 5;//状态
        const byte TIME = 6;//时钟标志
        const byte TERMTYPE = 24;//终端类型
        const byte NAWS = 31;//窗口大小(Negotiate about window size)
        const byte SPEED = 32;//终端速率
        const byte RFC = 33;//远程流量控制
        const byte LINEMODE = 34;//行模式
        const byte ENV = 36;//环境变量

        //vt220协议
        const byte ESC = 0x1B;//命令解释符号
        #endregion

        #region 事件

        /// <summary>
        /// 开始打印事件
        /// </summary>
        private event BeginPrintEventHandler m_beginPrintEvent = null;
        public event BeginPrintEventHandler BeginPrintEvent
        {
            add { m_beginPrintEvent += value; }
            remove { m_beginPrintEvent -= value; }
        }
        /// <summary> 
        /// 结束打印事件
        /// </summary>
        private event EndPrintEventHandler m_endPrintEvent = null;
        public event EndPrintEventHandler EndPrintEvent
        {
            add { m_endPrintEvent += value; }
            remove { m_endPrintEvent -= value; }
        }

        /// <summary>
        /// 连接关闭事件
        /// </summary>
        private event ConnectionClosedEventHandler m_connectionClosedEvent = null;
        public event ConnectionClosedEventHandler ConnectionClosed
        {
            add { m_connectionClosedEvent += value; }
            remove { m_connectionClosedEvent -= value; }
        }

        #region 事件方法
        /// <summary>
        /// 开始打印
        /// </summary>
        /// <param name="e"></param>
        public void OnBeginPrint(PrintEventArgs e)
        {
            m_beginPrintEvent?.Invoke(this, e);
        }
        /// <summary>
        /// 结束打印
        /// </summary>
        /// <param name="e"></param>
        public void OnEndPrint(PrintEventArgs e)
        {
            m_endPrintEvent?.Invoke(this, e);
        }

        public void OnConnectionClosed(EventArgs e)
        {
            m_connectionClosedEvent?.Invoke(this, e);
        }
        #endregion
        #endregion

        #region 内部变量
        /// <summary>
        /// TCP连接对象
        /// </summary>
        private TcpClient m_conn = null;

        /// <summary>
        /// 用户命令
        /// </summary>
        private RingQueue<byte> m_userQueue = null;

        /// <summary>
        /// VT220待解析命令
        /// </summary>
        private RingQueue<byte> m_telnetDataQueue = null;

        #endregion

        #region 属性
        //终端相关参数
        /// <summary>
        /// 协议类型
        /// </summary>
        public string TermType { get; } = "vt220";

        /// <summary>
        /// 屏幕列
        /// </summary>
        public int ScreenColumns { get; } = 80;

        /// <summary>
        /// 屏幕行
        /// </summary>
        public int ScreenRows { get; } = 24;

        //VT220协议
        /// <summary>
        /// 标记是否输出打印信息
        /// </summary>
        public bool IsPrinting { get; set; } = false;
        /// <summary>
        /// 打印文件路径
        /// </summary>
        public string PrintFileName { get; set; } = "";
        /// <summary>
        /// 输出文件流
        /// </summary>
        private System.IO.Stream Output { get; set; } = null;//输出文件
        /// <summary>
        /// 计时器(用于记录文件输出时间)
        /// </summary>
        private int TickCount { get; set; } = 0;

        /// <summary>
        /// 基础TcpClient对象
        /// </summary>
        public TcpClient TcpClient
        {
            get { return m_conn; }
        }

        /// <summary>
        /// 主机名称
        /// </summary>
        public string Host { get; set; } = "shk.ap.averydennison.net";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 23;

        /// <summary>
        /// 绑定窗体(必须绑定)
        /// </summary>
        public TermScreen Screen { get; set; } = null;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="screen">屏幕显示控件</param>
        public TelnetHelper(TermScreen screen)
        {
            Screen = screen;
        }

        public TelnetHelper(string host, int port, TermScreen screen)
        {
            Host = host;
            Port = port;
            Screen = screen;
        }

        /// <summary>
        /// 连接主机
        /// </summary>
        public void Connect()
        {
            try
            {
                Disconnect();
                m_conn = new TcpClient(Host, Port)
                {
                    ReceiveBufferSize = 10240,
                    //SendBufferSize=10240
                };
                Process();
            }
            catch (System.Net.Sockets.SocketException e)
            {
                var old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ForegroundColor = old;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Disconnect()
        {
            if (m_conn != null)
            {
                m_conn.Close();
                m_conn = null;
                OnConnectionClosed(new EventArgs());
            }
        }

        /// <summary>
        /// 进入消息等待
        /// </summary>
        public void Process()
        {
            //用户发送队列
            m_userQueue = new RingQueue<byte>(1024);
            //vt220待解析数据
            m_telnetDataQueue = new RingQueue<byte>(8192);
            //消息处理
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate ()
            {
                //接收缓存队列
                var recvQueue = new RingQueue<byte>(8192);

                //发送缓存队列
                var sendQueue = new RingQueue<byte>(1024);

                var io = m_conn.GetStream();
                var sendStream = new System.IO.BufferedStream(io);
                var recvStream = new System.IO.BufferedStream(io);

                while(m_conn!=null && m_conn.Connected)
                {
                    //接收服务端数据
                    while (io.DataAvailable && recvQueue.Available > 0)
                    {
                        var b = io.ReadByte();
                        if (b == -1) break;
                        recvQueue.Push((byte)b);
                    }
                    //解析Telnet协议 并 响应服务器
                    ParseTelnetCommand(recvQueue, sendQueue, m_telnetDataQueue);

                    //解析VT220协议
                    ParseVT220Command(m_telnetDataQueue);

                    //客户端发送数据
                    while (sendQueue.Count > 0 && m_conn.Connected)
                    {
                        var data = sendQueue.ToArray();
                        sendStream.Write(data, 0, data.Length);
                        sendStream.Flush();
                        sendQueue.Clear();
                    }

                    //发送用户数据
                    while (m_userQueue.Count > 0 && m_conn.Connected)
                    {
                        var data = m_userQueue.ToArray();
                        sendStream.Write(data, 0, data.Length);
                        sendStream.Flush();
                        m_userQueue.Clear();
                    }

                    //休眠100ms(文件输出时20ms)
                    System.Threading.Thread.Sleep(IsPrinting?10:100);

                    //检测终止标志(不确定)
                    if(m_conn!=null && m_conn.Client.Poll(1,SelectMode.SelectRead) && m_conn.Client.Available == 0)
                    {
                        this.Disconnect();
                        Logger.Info("Telnet 连接关闭(收到服务端指令)");
                    }
                }
            })).Start();

            //绑定控件
            //绑定用户输入事件
            Screen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(delegate (object sender, System.Windows.Forms.KeyPressEventArgs e)
            {
                m_userQueue.Push(Convert.ToByte(e.KeyChar));
            });

            Screen.KeyDown += new System.Windows.Forms.KeyEventHandler(delegate (object sender, System.Windows.Forms.KeyEventArgs e)
            {
                SendKey(e.KeyCode);
                //Console.WriteLine(e.KeyCode);
            });
            //绑定用户输入事件
        }

        /// <summary>
        /// 处理Telnet命令并返回数据
        /// </summary>
        /// <param name="recv"></param>
        /// <param name="send"></param>
        /// <param name="data"></param>
        void ParseTelnetCommand(RingQueue<byte> recv, RingQueue<byte> send, RingQueue<byte> data)
        {
            while (recv.Count > 0 && data.Available > 0)
            {
                var b = recv.Shift();

                if (b == IAC)
                {
                    //命令处理
                    if (recv.Count < 2)
                    {
                        recv.Unshift(b);
                        break;
                    }

                    var action = recv.Shift();
                    var option = recv.Shift();

                    switch (option)
                    {
                        case ECHO:
                        case NOGA:
                        case TERMTYPE:
                        case NAWS:
                            switch (action)
                            {
                                case WILL:
                                    send.PushValues(new byte[] { IAC, DO, option });
                                    break;
                                case DO:
                                    send.PushValues(new byte[] { IAC, WILL, option });
                                    break;
                                case SB:
                                    switch (option)
                                    {
                                        case TERMTYPE:
                                            if (recv.Count < 3)
                                            {
                                                recv.UnshiftValues(new byte[] { option, action, b });
                                                break;
                                            }
                                            if (recv.Shift() == 1)
                                            {
                                                recv.Shift(); recv.Shift();
                                                //发送客户端型号
                                                send.PushValues(new byte[] { IAC, SB, TERMTYPE, 0 });
                                                send.PushValues(Encoding.Default.GetBytes(TermType));
                                                send.PushValues(new byte[] {IAC, SE });
                                            }
                                            break;
                                    }
                                    break;
                            }
                            if (action == WILL && option == NAWS)
                            {
                                //发送窗体大小
                                send.PushValues(new byte[] { IAC, SB, NAWS,
                                    (byte)((ScreenColumns>>8)&0xFF), (byte)(ScreenColumns  & 0xFF),
                                    (byte)((ScreenRows >> 8) & 0xFF), (byte)(ScreenRows & 0xFF), IAC, SE });
                            }
                            break;
                        default:
                            //拒绝其他一切选项
                            switch (action)
                            {
                                case WILL:
                                case WONT:
                                    send.PushValues(new byte[] { IAC, DONT, option });
                                    break;
                                case DO:
                                case DONT:
                                    send.PushValues(new byte[] { IAC, WONT, option });
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    //丢给VT220解析器
                    data.Push(b);
                }
            }
        }

        #region VT220协议解析

        /// <summary>
        /// 处理VT220协议数据
        /// </summary>
        /// <param name="data"></param>
        void ParseVT220Command(RingQueue<byte> data)
        {
            while (data.Count > 0)
            {
                var b = data.GetFirstValue();
                if (b == ESC)
                {
                    if (data.Count < 2) break;
                    /**
                     * 第二指令说明： ESC [opt]
                     * 
                     * N: 转交SS2命令处理
                     * O: 转交SS3命令处理
                     * P: 转交DCS命令处理
                     * [: 转交CSI命令处理
                     * \: 转交ST命令处理
                     * D: 光标向下移动一行(同IND命令),若光标在底部则内容向上滚动
                     * M: 光标向上移动一行(同RI命令),若光标在顶部则内容向下滚动
                     * E: 光标移动到下一行首位(同NEL命令),若光标在底部则内容向上滚动
                     * 7: 保存光标状态(光标位置,图形渲染,字符偏移状态,自动换行,参考点,删除内容区域)
                     * 8: 重置光标状态
                     * H: 设置水平Tab光标停留位置(同HTS命令)
                     * #: 行属性
                     *      ESC # 3 上半部
                     *      ESC # 4 下半部
                     *      ESC # 5 单倍宽度
                     *      ESC # 6 双倍宽度
                     * c: 硬重置终端
                     * =: 键盘模式(应用模式)
                     * >：键盘模式(数字模式)
                     * (: G0字符集 ESC ( [final]
                     * ): G1字符集 ESC ) [final]
                     * *: G2字符集 ESC * [final]
                     * +: G3字符集 ESC + [final]
                     */
                    switch (data.GetNextValue())
                    {
                        case 0x5B:// [ ==> CSI
                            if (!ParseCSICommand(data)) return;
                            break;

                        #region 编码集设定
                        case 0x28:// (
                            //Console.WriteLine("G0 " + data.GetNextValue().ToString());
                            Logger.Debug("编码集设定: " + "G0 " + data.GetNextValue().ToString());
                            Screen.Charset0 = ParseCharsets(data.Value);
                            data.LTrim();
                            break;
                        case 0x29:// ) 
                            Logger.Debug("编码集设定: " + "G1 " + data.GetNextValue().ToString());
                            //Console.WriteLine("G1 " + data.GetNextValue().ToString());
                            Screen.Charset1 = ParseCharsets(data.Value);
                            data.LTrim();
                            break;
                        case 0x2A:// *
                            Logger.Debug("编码集设定: " + "G2 " + data.GetNextValue().ToString());
                            //Console.WriteLine("G2 " + data.GetNextValue().ToString());
                            Screen.Charset2 = ParseCharsets(data.Value);
                            data.LTrim();
                            break;
                        case 0x2B:// +
                            Logger.Debug("编码集设定: " + "G3 " + data.GetNextValue().ToString());
                            //Console.WriteLine("G3 " + data.GetNextValue().ToString());
                            Screen.Charset3 = ParseCharsets(data.Value);
                            data.LTrim();
                            break;
                        #endregion

                        case 0x3E:// > 键盘模式(数字键盘) 参考4.6.18
                            Logger.Debug("键盘模式:数字模式");
                            //Console.WriteLine("键盘模式:数字模式");
                            data.LTrim();
                            break;
                    }
                }
                else if (b == 0x0F) //LS0 将G0字符集映射到GL
                {
                    Logger.Debug("G0字符集映射到GL");
                    Screen.Charset = Screen.Charset0;
                    data.LTrim();
                }
                else if (b == 0x0E) //LS1 将G1字符集映射到GL
                {
                    Logger.Debug("G1字符集映射到GL");
                    Screen.Charset = Screen.Charset1;
                    data.LTrim();
                }
                else if (b == 0x07) //Beep
                {
                    Console.Beep(250, 100);
                    data.LTrim();
                }
                else if(b==0x08) //退格
                {
                    var pos = Screen.CursorPos;
                    pos.X--;
                    Screen.CursorPos = pos;
                    Screen.WriteByte(0);
                    Screen.CursorPos = pos;
                    data.LTrim();
                }
                else
                {
                    data.LTrim();
                    if (b != 0)
                    {
                        if (IsPrinting)
                        {
                            //输出到文件
                            Output.WriteByte(b);
                        }
                        else
                        {
                            Screen.WriteByte(b);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解析CSI类命令
        /// </summary>
        /// <param name="data"></param>
        bool ParseCSICommand(RingQueue<byte> data)
        {
            /**
             * 命令基本为字母结尾
             * h: 模式设定(SET)
             * l: 模式设定(RESET)
             * A: 光标上移指定行 CSI [num] A
             * B: 光标下移指定行 CSI [num] B
             * C: 光标左移指定列 CSI [num] C
             * D: 光标右移指定列 CSI [num] D
             * H: 指定光标位置 CSI [num|column] ; [num|row] H
             * f: 同H
             * g: 清除一个水平Tab停止位置 CSI [num] g
             *      null/0: 清除当前位置的水平Tab停止  
             *      3:清除所有
             * m: 设置图形渲染模式 CSI [num] ; [num] ... m
             *      0/null: 关闭所有
             *      1: 粗体
             *      4: 下划线
             *      5: 闪烁
             *      7: 反转显示
             *      2 2: 一般
             *      2 4: 不显示下划线
             *      2 5: 不显示闪烁
             *      2 7: 不反转
             * L: 光标指定位置插入指定行 CSI [num] L
             * M: 从光标所在行开始删除指定行 CSI [num] M
             * @: 从光标当前位置插入指定个字符(仅VT200) CSI [num] @
             * P: 从光标位置开始删除指定个字符 CSI [num] P
             * K: 删除字符 CSI [num] K
             *      null/0: 删除光标所在行右侧的字符(包含光标字符)
             *      1: 删除光标所在行左侧的字符(包含光标字符)
             *      2: 删除整行
             *      
             *      ? null/0: 删除光标所在行右侧的字符(包含光标字符|仅VT200)
             *      ? 1: 删除光标所在行左侧的字符(包含光标字符|仅VT200)
             *      ? 2: 删除整行(仅VT200)
             * J: 删除字符 CSI [num] J
             *      null/0: 删除光标到屏幕结束的字符(包含光标字符)
             *      1: 删除光标到屏幕开始的字符(包含光标字符)
             *      2: 清除屏幕
             * r: 设置上下的页边距 CSI [top] ; [bottom] r
             * i: 打印设定 CSI [num] i
             *      ? 5: 开启自动打印模式
             *      ? 4: 关闭自动打印模式
             *      5: 开启打印控制模式
             *      4: 关闭打印控制模式
             *      ? 1: 打印当前行
             *      null/0: 打印当前屏幕
             * 
             */

            while (!Encoding.ASCII.GetBytes("hlABCDHgmLMPJK@rif").Contains(data.GetNextValue()) &&
                data.Current != data.FOOT) ;

            if (!Encoding.ASCII.GetBytes("hlABCDHgmLMPJK@rif").Contains(data.Value)) return false;

            var cmdbytes = data.LTrim().Select(o => Convert.ToByte(o)).ToArray();
            var cmd = Encoding.ASCII.GetString(cmdbytes, 2, cmdbytes.Length - 3);

            switch (cmdbytes.Last())
            {
                case 0x66: //f
                    if (cmdbytes.Length > 3)
                    {
                        if (cmd == ";")
                        {
                            Screen.CursorSetPos(0, 0);
                        }
                        else
                        {
                            var wh = cmd.Split(';');
                            wh[0] = wh[0] == "" ? "1" : wh[0];
                            wh[1] = wh[1] == "" ? "1" : wh[1];
                            Screen.CursorSetPos(int.Parse(wh[0]) - 1, int.Parse(wh[1]) - 1);
                        }
                    }
                    break;
                case 0x48: //H
                    if (cmdbytes.Length > 3)
                    {
                        if (cmd == ";")
                        {
                            Screen.CursorSetPos(0, 0);
                        }
                        else
                        {
                            var wh = cmd.Split(';');
                            Screen.CursorSetPos(int.Parse(wh[0]) - 1, int.Parse(wh[1]) - 1);
                        }
                    }
                    break;
                case 0x4A: //J
                    if (cmdbytes.Length == 3)
                    {
                        Screen.ScreenClearAfterCursor();
                    }
                    else
                    {
                        switch (cmd)
                        {
                            case "0":
                                Screen.ScreenClearAfterCursor();
                                break;
                            case "1":
                                Screen.ScreenClearBeforeCursor();
                                break;
                            case "2":
                                Screen.ScreenClear();
                                break;
                        }
                    }
                    break;
                case 0x4B: //K
                    if (cmdbytes.Length == 3)
                    {
                        Screen.LineClearAfterCursor();
                    }
                    else
                    {
                        switch (cmd)
                        {
                            case "0":
                                Screen.LineClearAfterCursor();
                                break;
                            case "1":
                                Screen.LineClearBeforeCursor();
                                break;
                            case "2":
                                Screen.LineClear();
                                break;
                        }
                    }
                    break;
                case 0x68: //h
                    Logger.Debug("模式设定(SET)：" + cmd.Substring(1));
                    break;
                case 0x6C: //l
                    Logger.Debug("模式设定(RESET)：" + cmd.Substring(1));
                    break;
                case 0x6D: //m
                    if (cmdbytes.Length == 3)
                    {
                        Screen.IsBold = false;
                        Screen.IsUnderline = false;
                        Screen.IsBlinking = false;
                        Screen.IsReverse = false;
                    }
                    else
                    {
                        foreach (var v in cmd.Split(';'))
                        {
                            switch (v)
                            {
                                case "":
                                case "0":
                                    Screen.IsBold = false;
                                    Screen.IsUnderline = false;
                                    Screen.IsBlinking = false;
                                    Screen.IsReverse = false;
                                    break;
                                case "1":
                                    Screen.IsBold = true;
                                    break;
                                case "4":
                                    Screen.IsUnderline = true;
                                    break;
                                case "5":
                                    Screen.IsBlinking = true;
                                    break;
                                case "7":
                                    Screen.IsReverse = true;
                                    break;
                                case "22":
                                    Screen.IsBold = false;
                                    break;
                                case "24":
                                    Screen.IsUnderline = false;
                                    break;
                                case "25":
                                    Screen.IsBlinking = false;
                                    break;
                                case "27":
                                    Screen.IsReverse = false;
                                    break;
                            }
                        }
                    }
                    Logger.Debug("样式渲染:" + cmd);
                    break;
                case 0x69: // i
                    if (cmdbytes.Length == 3)
                    {
                        Logger.Debug("打印当前屏幕");
                    }
                    else
                    {
                        switch (cmd)
                        {
                            case "5":
                                if (PrintFileName==""){
                                    PrintFileName = Environment.CurrentDirectory + "\\print.txt";
                                }
                                OnBeginPrint(new PrintEventArgs(PrintFileName));
                                IsPrinting = true;
                                Output = new System.IO.FileStream(PrintFileName, System.IO.FileMode.Create);
                                TickCount = Environment.TickCount;
                                Logger.Warn(string.Format("开始输出打印数据({0})", PrintFileName));
                                break;
                            case "4":
                                Logger.Warn("输出打印数据完成");
                                IsPrinting = false;
                                Output.Flush();
                                Output.Close();
                                Output = null;
                                OnEndPrint(new PrintEventArgs(PrintFileName));
                                Logger.Info("共耗时: " + (Environment.TickCount - TickCount) / 1000.0F + " 秒");
                                break;
                        }
                    }
                    break;
                default:
                    Logger.Error("无法识别CSI指令: " + Encoding.ASCII.GetString(cmdbytes));
                    break;
            }

            return true;
        }

        /// <summary>
        /// 获取字符集
        /// </summary>
        string[] ParseCharsets(byte charsetType)
        {
            switch (charsetType)
            {
                case 0x42://B ASCII
                    return Charsets.ASCII;
                case 0x3C://< DEC supplemental(VT200 Only)
                    return null;
                case 0x30://0 DEC special Character
                    return Charsets.DEC_SPECIAL;
                case 0x41://A British
                    return null;
                case 0x34://4 Dutch
                    return null;
                case 0x43://C
                case 0x35://5 Finnish
                    return null;
                case 0x52://R French
                    return null;
                case 0x51://Q French Canadian
                    return null;
                case 0x4B://K German
                    return null;
                case 0x59://Y Italian
                    return null;
                case 0x45://E
                case 0x36://6 Norwegian/Danish
                    return null;
                case 0x5A://Z Spanish
                    return null;
                case 0x48://H
                case 0x37://7 Swedish
                    return null;
                case 0x3D://= Swiss
                    return null;
            }
            return null;
        }
        #endregion

        #region 用户操作控制方法

        public string GetString(int row, int col, int len)
        {
            return Screen.GetString(row, col, len);
        }

        public bool SendData(byte[] bs)
        {
            if (m_userQueue.Available > bs.Length)
            {
                m_userQueue.PushValues(bs);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 发送按键(主要用于特殊按键的发送)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool SendKey(Keys key)
        {
            //检查发送队列可用容量
            if (m_userQueue.Available < 5) return false;
            switch (key)
            {
                case Keys.F1:
                    m_userQueue.PushValues(new byte[] { ESC, 0x4F, 0x50 });
                    break;
                case Keys.F2:
                    m_userQueue.PushValues(new byte[] { ESC, 0x4F, 0x51 });
                    break;
                case Keys.F3:
                    m_userQueue.PushValues(new byte[] { ESC, 0x4F, 0x52 });
                    break;
                case Keys.F4:
                    m_userQueue.PushValues(new byte[] { ESC, 0x4F, 0x53 });
                    break;
                case Keys.F5:
                    m_userQueue.PushValues(new byte[] { ESC, 0x4F, 0x74 });
                    break;
                case Keys.F6:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x31, 0x37, 0x7E });
                    break;
                case Keys.F7:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x31, 0x38, 0x7E });
                    break;
                case Keys.F8:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x31, 0x39, 0x7E });
                    break;
                case Keys.F9:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x32, 0x30, 0x7E });
                    break;
                case Keys.F10:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x32, 0x31, 0x7E });
                    break;
                case Keys.F11:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x32, 0x33, 0x7E });
                    break;
                case Keys.F12:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x32, 0x34, 0x7E });
                    break;
                case Keys.Up:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x41 });
                    break;
                case Keys.Down:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x42 });
                    break;
                case Keys.Right:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x43 });
                    break;
                case Keys.Left:
                    m_userQueue.PushValues(new byte[] { ESC, 0x5B, 0x44 });
                    break;
                default:
                    //Console.WriteLine("sendkey failed invalid key press "+ key.ToString());
                    break;
            }
            return true;
        }

        public bool SetOutputFileName(string filename)
        {
            PrintFileName = filename;
            return true;
        }

        public int CursorX
        {
            get { return Screen.CursorPos.X; }
        }

        public int CursorY
        {
            get { return Screen.CursorPos.Y; }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    Disconnect();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~TelnetHelper() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
