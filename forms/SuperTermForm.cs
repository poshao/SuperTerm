using System;
using System.Reflection;
using System.Windows.Forms;
using Spoonson.Common;

namespace Spoonson.Apps.SuperTerm
{
    public partial class SuperTermForm : Form
    {
        /// <summary>
        /// 终端控制器
        /// </summary>
        private TelnetHelper m_telnet = null;

        /// <summary>
        /// 用户脚本线程
        /// </summary>
        private System.Threading.Thread m_userProcess = null;

        #region 属性
        /// <summary>
        /// 终端控制器
        /// </summary>
        public TelnetHelper Controller {
            get { return m_telnet; }
            set { m_telnet=value; }
        }

        /// <summary>
        /// 脚本线程
        /// </summary>
        public System.Threading.Thread ScriptProcess
        {
            get { return m_userProcess; }
            set { m_userProcess=value; }
        }

        #endregion

        public SuperTermForm()
        {
            InitializeComponent();
            InitHostlist();
        }

        #region 内部方法
        /// <summary>
        /// 初始化主机列表
        /// </summary>
        private void InitHostlist()
        {
            toolHostlist.Items.Clear();
            foreach (Newtonsoft.Json.Linq.JProperty item in Config.GetValue("hostlist"))
            {
                toolHostlist.Items.Add(item.Name);
            }
            toolHostlist.SelectedIndex = 0;
        }

        /// <summary>
        /// 连接
        /// </summary>
        private void Connect()
        {
            Controller = new TelnetHelper(termBody)
            {
                Host = Config.GetValue("hostlist")[toolHostlist.Text]["url"].ToString(),
                Port = int.Parse(Config.GetValue("hostlist")[toolHostlist.Text]["port"].ToString())
            };
            Controller.ConnectionClosed += new ConnectionClosedEventHandler(
                delegate (object sender, EventArgs e)
                {
                    Disconnect();
                }
            );
            Controller.BeginPrintEvent += new BeginPrintEventHandler(
                delegate(object sender, PrintEventArgs e)
                {
                    sslblPrintFilepath.Text = string.Format("正在传输 ==> {0}",e.FileName);
                    sslblPrintFilepath.Visible = true;
                }    
            );
            Controller.EndPrintEvent += new EndPrintEventHandler(
                delegate(object sender,PrintEventArgs e)
                {
                    sslblPrintFilepath.Text = "";
                    sslblPrintFilepath.Visible = false;
                }
            );
            Controller.SetOutputFileName(Config.GetValue("baseSetting")["printfilename"].ToString());
            Controller.Connect();
        }

        /// <summary>
        /// 断开
        /// </summary>
        private void Disconnect()
        {
            if (Controller != null)
            {
                Controller.Screen.ScreenClear();
                Controller.Disconnect();
                Controller = null;
            }
        }

        /// <summary>
        /// 加载运行脚本
        /// </summary>
        /// <param name="strCode"></param>
        private void StartScript(string strCode)
        {
            //创建应用域
            var appSetup = new AppDomainSetup();
            appSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            var appdomain = AppDomain.CreateDomain("ClientSpace", null, appSetup);

            //创建编译器
            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            //配置编译参数
            var compilerParameters = new System.CodeDom.Compiler.CompilerParameters();
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            compilerParameters.GenerateInMemory = false;
            compilerParameters.OutputAssembly = "client.dll";

            //读取代码编译
            var usercore = Spoonson.Apps.SuperTerm.Properties.Resources.usercore;
            var compilerResults = provider.CompileAssemblyFromSource(compilerParameters, usercore, strCode);

            if (compilerResults.Errors.HasErrors)
            {
                string strErrorMessage = "";
                for (int i = 0; i < compilerResults.Errors.Count; i++)
                {
                    strErrorMessage += "\r\nline: " + compilerResults.Errors[i].Line.ToString() + " " + compilerResults.Errors[i].ErrorText + Environment.NewLine;
                }
                Logger.Error("编译失败 " + compilerResults.Errors.Count);
                Logger.Error(strErrorMessage);

                return;
            }
            compilerResults = null;

            //创建用户线程
            StopScript();

            ScriptProcess = new System.Threading.Thread(new System.Threading.ThreadStart(delegate ()
            {
                try
                {
                    var bfi = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
                    var remoteLoader = appdomain.CreateInstanceFrom("RemoteLoader.dll", "Spoonson.Common.RemoteLoader", false, bfi, null, null, null, null).Unwrap() as Spoonson.Common.RemoteLoader;
                    var plugin = remoteLoader.Create("client.dll", "UserSpace.UserCode", new object[] { Controller });
                    plugin = null;
                }
                catch (System.Threading.ThreadAbortException e)
                {
                    //处理线程终止过程
                    Logger.Error(e.ToString());
                    Logger.Warn("线程被用户终止");
                }
                finally
                {
                    AppDomain.Unload(appdomain);
                    appdomain = null;
                    System.IO.File.Delete("client.dll");
                }
            }));
            ScriptProcess.Start();
        }

        /// <summary>
        /// 从文件加载脚本运行
        /// </summary>
        /// <param name="strFile"></param>
        private void StartScriptFile(string strFile)
        {
            StartScript(System.IO.File.ReadAllText(strFile));
        }

        /// <summary>
        /// 加载运行脚本
        /// </summary>
        private void LoadAndRunScriptFile()
        {
            using (var ofn = new OpenFileDialog())
            {
                ofn.InitialDirectory = Environment.CurrentDirectory;
                if (ofn.ShowDialog() == DialogResult.OK)
                {
                    StartScriptFile(ofn.FileName);
                    sslblScript.Text = string.Format("正在运行代码【{0}】", System.IO.Path.GetFileNameWithoutExtension(ofn.FileName));
                }
            }
        }

        /// <summary>
        /// 终止脚本
        /// </summary>
        private void StopScript()
        {
            if (ScriptProcess != null)
            {
                ScriptProcess.Abort();
                ScriptProcess.Join();
            }
        }
        
        #endregion


        #region 控件事件响应

        /// <summary>
        /// 菜单按钮事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuClick(object sender, EventArgs e)
        { 
            var menu = sender as ToolStripMenuItem;
            switch (menu.Name) {
                //文件
                case "menuAddress"://地址栏
                    new HostListForm().ShowDialog(this);
                    InitHostlist();
                    break;
                case "menuDebugMode"://调试模式
                    menu.Checked = !menu.Checked;
                    break;
                case "menuBaseSetting"://基础设置
                    new BaseSettingForm().ShowDialog(this);
                    break;
                case "menuUserSetting"://用户设置

                    break;
                case "menuQuit"://退出
                    this.Close();
                    break;
                //工具
                case "menuScreenPos"://屏幕坐标
                    if (Controller != null)
                    {
                        menu.Checked = !menu.Checked;
                        timerShowPos.Enabled = menu.Checked;
                        sslblPos.Visible = menu.Checked;
                    }
                    else
                    {
                        if (!menu.Checked)
                        {
                            MessageBox.Show(this,"请先连接主机","提示");
                        }
                        menu.Checked = sslblPos.Visible = timerShowPos.Enabled= false;
                    }
                    break;
                case "menuScriptEditor"://编辑器
                    MessageBox.Show("尚未支持功能");
                    break;
                case "menuRunScript"://运行脚本
                    LoadAndRunScriptFile();
                    break;
                case "menuStopScript"://停止脚本
                    StopScript();
                    break;
                //关于
                case "menuAbout":
                    new AboutForm().ShowDialog(this);
                    break;
            }
        }

        /// <summary>
        /// 工具栏按钮事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolClick(object sender, EventArgs e)
        {
            var tool = sender as ToolStripItem;
            switch (tool.Name)
            {
                case "toolConnect"://连接
                    this.Connect();
                    break;
                case "toolBreak":  //断开连接
                    this.Disconnect();
                    break;
                case "toolHosts":  //地址簿
                    new HostListForm().ShowDialog(this);
                    InitHostlist();
                    break;
                case "toolScript": //运行脚本
                    LoadAndRunScriptFile();
                    break;
            }
        }

        /// <summary>
        /// 定时器事件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            var timer = sender as Timer;

            switch (timer.Tag)
            {
                case "timerShowPos":
                    if (Controller != null)
                    {
                        var pos = Controller.Screen.GetPos(MousePosition.X, MousePosition.Y);
                        sslblPos.Text = string.Format("行：{0}   列：{1}   ", pos.Y, pos.X);
                    }
                    break;
                case "timerUpdatetime":
                    sslblTime.Text = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                    sslblScript.Visible = ScriptProcess != null && ScriptProcess.IsAlive;
                    sslblLight.ForeColor = Controller != null && Controller.TcpClient.Connected ? System.Drawing.Color.Green : System.Drawing.Color.DarkRed;
                    break;
            }
        }
        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (Controller != null) Controller.Disconnect();
        }
    }
}
