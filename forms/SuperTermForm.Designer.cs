namespace Spoonson.Apps.SuperTerm
{
    partial class SuperTermForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                //释放Telnet资源
                if (Controller != null) {
                    Controller.Dispose();
                }
                //释放用户线程
                if (ScriptProcess != null)
                {
                    ScriptProcess.Abort();
                    ScriptProcess.Join();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperTermForm));
            this.timerShowPos = new System.Windows.Forms.Timer(this.components);
            this.menuRoot = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaseSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDebugMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScreenPos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScriptEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRunScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStopScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyServer = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusRoot = new System.Windows.Forms.StatusStrip();
            this.sslblTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslblLight = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslblScript = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssProcess = new System.Windows.Forms.ToolStripProgressBar();
            this.sslblPrintFilepath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslblPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolRoot = new System.Windows.Forms.ToolStrip();
            this.toolHostlist = new System.Windows.Forms.ToolStripComboBox();
            this.toolConnect = new System.Windows.Forms.ToolStripButton();
            this.toolBreak = new System.Windows.Forms.ToolStripButton();
            this.toolHosts = new System.Windows.Forms.ToolStripButton();
            this.toolScript = new System.Windows.Forms.ToolStripButton();
            this.timerUpdatetime = new System.Windows.Forms.Timer(this.components);
            this.termBody = new Spoonson.Apps.SuperTerm.TermScreen();
            this.notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRoot.SuspendLayout();
            this.statusRoot.SuspendLayout();
            this.toolRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerShowPos
            // 
            this.timerShowPos.Interval = 300;
            this.timerShowPos.Tag = "timerShowPos";
            this.timerShowPos.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // menuRoot
            // 
            this.menuRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTool,
            this.menuAbout});
            this.menuRoot.Location = new System.Drawing.Point(0, 0);
            this.menuRoot.Name = "menuRoot";
            this.menuRoot.Size = new System.Drawing.Size(806, 24);
            this.menuRoot.TabIndex = 6;
            this.menuRoot.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddress,
            this.menuSetting,
            this.menuDebugMode,
            this.menuQuit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(57, 20);
            this.menuFile.Text = "文件(&F)";
            // 
            // menuAddress
            // 
            this.menuAddress.Name = "menuAddress";
            this.menuAddress.Size = new System.Drawing.Size(138, 22);
            this.menuAddress.Text = "地址簿(&A)";
            this.menuAddress.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuSetting
            // 
            this.menuSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBaseSetting,
            this.menuUserSetting});
            this.menuSetting.Name = "menuSetting";
            this.menuSetting.Size = new System.Drawing.Size(138, 22);
            this.menuSetting.Text = "设置(&S)";
            // 
            // menuBaseSetting
            // 
            this.menuBaseSetting.Name = "menuBaseSetting";
            this.menuBaseSetting.Size = new System.Drawing.Size(138, 22);
            this.menuBaseSetting.Text = "基本设置(&B)";
            this.menuBaseSetting.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuUserSetting
            // 
            this.menuUserSetting.Name = "menuUserSetting";
            this.menuUserSetting.Size = new System.Drawing.Size(138, 22);
            this.menuUserSetting.Text = "用户设置(&U)";
            this.menuUserSetting.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuDebugMode
            // 
            this.menuDebugMode.Name = "menuDebugMode";
            this.menuDebugMode.Size = new System.Drawing.Size(138, 22);
            this.menuDebugMode.Text = "调试模式(&D)";
            this.menuDebugMode.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(138, 22);
            this.menuQuit.Text = "退出(&Q)";
            this.menuQuit.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuTool
            // 
            this.menuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuScreenPos,
            this.menuScriptEditor,
            this.toolStripMenuItem2,
            this.menuRunScript,
            this.menuStopScript});
            this.menuTool.Name = "menuTool";
            this.menuTool.Size = new System.Drawing.Size(58, 20);
            this.menuTool.Text = "工具(&T)";
            // 
            // menuScreenPos
            // 
            this.menuScreenPos.Name = "menuScreenPos";
            this.menuScreenPos.Size = new System.Drawing.Size(148, 22);
            this.menuScreenPos.Text = "屏幕坐标(&P)";
            this.menuScreenPos.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuScriptEditor
            // 
            this.menuScriptEditor.Name = "menuScriptEditor";
            this.menuScriptEditor.Size = new System.Drawing.Size(148, 22);
            this.menuScriptEditor.Text = "脚本编辑器(&E)";
            this.menuScriptEditor.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // menuRunScript
            // 
            this.menuRunScript.Name = "menuRunScript";
            this.menuRunScript.Size = new System.Drawing.Size(148, 22);
            this.menuRunScript.Text = "运行脚本(&R)";
            this.menuRunScript.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuStopScript
            // 
            this.menuStopScript.Name = "menuStopScript";
            this.menuStopScript.Size = new System.Drawing.Size(148, 22);
            this.menuStopScript.Text = "停止脚本(&S)";
            this.menuStopScript.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(59, 20);
            this.menuAbout.Text = "关于(&A)";
            this.menuAbout.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // notifyServer
            // 
            this.notifyServer.BalloonTipText = "欢迎使用SuperTerm";
            this.notifyServer.BalloonTipTitle = "SuperTerm";
            this.notifyServer.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyServer.Icon")));
            this.notifyServer.Text = "SuperTerm";
            this.notifyServer.Visible = true;
            // 
            // statusRoot
            // 
            this.statusRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslblTip,
            this.sslblLight,
            this.sslblScript,
            this.ssProcess,
            this.sslblPrintFilepath,
            this.toolStripStatusLabel3,
            this.sslblPos,
            this.sslblTime});
            this.statusRoot.Location = new System.Drawing.Point(0, 434);
            this.statusRoot.Name = "statusRoot";
            this.statusRoot.Size = new System.Drawing.Size(806, 27);
            this.statusRoot.TabIndex = 7;
            this.statusRoot.Text = "statusStrip1";
            // 
            // sslblTip
            // 
            this.sslblTip.Name = "sslblTip";
            this.sslblTip.Size = new System.Drawing.Size(31, 22);
            this.sslblTip.Text = "状态";
            // 
            // sslblLight
            // 
            this.sslblLight.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sslblLight.ForeColor = System.Drawing.Color.DarkRed;
            this.sslblLight.Name = "sslblLight";
            this.sslblLight.Size = new System.Drawing.Size(20, 22);
            this.sslblLight.Text = "●";
            // 
            // sslblScript
            // 
            this.sslblScript.Name = "sslblScript";
            this.sslblScript.Size = new System.Drawing.Size(111, 22);
            this.sslblScript.Text = "正在运行[脚本名称]";
            this.sslblScript.Visible = false;
            // 
            // ssProcess
            // 
            this.ssProcess.Name = "ssProcess";
            this.ssProcess.Size = new System.Drawing.Size(100, 21);
            this.ssProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ssProcess.Visible = false;
            // 
            // sslblPrintFilepath
            // 
            this.sslblPrintFilepath.Name = "sslblPrintFilepath";
            this.sslblPrintFilepath.Size = new System.Drawing.Size(153, 22);
            this.sslblPrintFilepath.Text = "正在下载文件==>C:\\123.txt";
            this.sslblPrintFilepath.Visible = false;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(645, 22);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // sslblPos
            // 
            this.sslblPos.Name = "sslblPos";
            this.sslblPos.Size = new System.Drawing.Size(97, 22);
            this.sslblPos.Text = "行：10   列：23   ";
            this.sslblPos.Visible = false;
            // 
            // sslblTime
            // 
            this.sslblTime.Name = "sslblTime";
            this.sslblTime.Size = new System.Drawing.Size(95, 22);
            this.sslblTime.Text = "2018-11-24 17:40";
            // 
            // toolRoot
            // 
            this.toolRoot.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolRoot.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHostlist,
            this.toolConnect,
            this.toolBreak,
            this.toolHosts,
            this.toolScript});
            this.toolRoot.Location = new System.Drawing.Point(0, 24);
            this.toolRoot.Name = "toolRoot";
            this.toolRoot.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.toolRoot.Size = new System.Drawing.Size(806, 46);
            this.toolRoot.TabIndex = 8;
            this.toolRoot.Text = "toolStrip1";
            // 
            // toolHostlist
            // 
            this.toolHostlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolHostlist.Name = "toolHostlist";
            this.toolHostlist.Size = new System.Drawing.Size(121, 46);
            this.toolHostlist.Sorted = true;
            // 
            // toolConnect
            // 
            this.toolConnect.Image = global::Spoonson.Apps.SuperTerm.Properties.Resources.link;
            this.toolConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolConnect.Name = "toolConnect";
            this.toolConnect.Size = new System.Drawing.Size(35, 43);
            this.toolConnect.Text = "连接";
            this.toolConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolConnect.Click += new System.EventHandler(this.OnToolClick);
            // 
            // toolBreak
            // 
            this.toolBreak.Image = global::Spoonson.Apps.SuperTerm.Properties.Resources.unlink;
            this.toolBreak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBreak.Name = "toolBreak";
            this.toolBreak.Size = new System.Drawing.Size(35, 43);
            this.toolBreak.Text = "断开";
            this.toolBreak.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBreak.Click += new System.EventHandler(this.OnToolClick);
            // 
            // toolHosts
            // 
            this.toolHosts.Image = global::Spoonson.Apps.SuperTerm.Properties.Resources.book;
            this.toolHosts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHosts.Name = "toolHosts";
            this.toolHosts.Size = new System.Drawing.Size(35, 43);
            this.toolHosts.Text = "地址";
            this.toolHosts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolHosts.Click += new System.EventHandler(this.OnToolClick);
            // 
            // toolScript
            // 
            this.toolScript.Image = global::Spoonson.Apps.SuperTerm.Properties.Resources.script;
            this.toolScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolScript.Name = "toolScript";
            this.toolScript.Size = new System.Drawing.Size(35, 43);
            this.toolScript.Text = "脚本";
            this.toolScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolScript.Click += new System.EventHandler(this.OnToolClick);
            // 
            // timerUpdatetime
            // 
            this.timerUpdatetime.Enabled = true;
            this.timerUpdatetime.Interval = 500;
            this.timerUpdatetime.Tag = "timerUpdatetime";
            this.timerUpdatetime.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // termBody
            // 
            this.termBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.termBody.Charset = new string[] {
        "",
        "␁",
        "␂",
        "␃",
        "␄",
        "␅",
        "␆",
        "␇",
        "␈",
        "␉",
        "␊",
        "␋",
        "␌",
        "␍",
        "␎",
        "␏",
        "␐",
        "␑",
        "␒",
        "␓",
        "␔",
        "␕",
        "␖",
        "␗",
        "␘",
        "␙",
        "␚",
        "␛",
        "␜",
        "␝",
        "␞",
        "␟",
        " ",
        "!",
        "\"",
        "#",
        "$",
        " %",
        "&",
        "\'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "{",
        "|",
        "}",
        "~",
        "␡"};
            this.termBody.Charset0 = new string[] {
        "",
        "␁",
        "␂",
        "␃",
        "␄",
        "␅",
        "␆",
        "␇",
        "␈",
        "␉",
        "␊",
        "␋",
        "␌",
        "␍",
        "␎",
        "␏",
        "␐",
        "␑",
        "␒",
        "␓",
        "␔",
        "␕",
        "␖",
        "␗",
        "␘",
        "␙",
        "␚",
        "␛",
        "␜",
        "␝",
        "␞",
        "␟",
        " ",
        "!",
        "\"",
        "#",
        "$",
        " %",
        "&",
        "\'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "{",
        "|",
        "}",
        "~",
        "␡"};
            this.termBody.Charset1 = new string[] {
        "",
        "␁",
        "␂",
        "␃",
        "␄",
        "␅",
        "␆",
        "␇",
        "␈",
        "␉",
        "␊",
        "␋",
        "␌",
        "␍",
        "␎",
        "␏",
        "␐",
        "␑",
        "␒",
        "␓",
        "␔",
        "␕",
        "␖",
        "␗",
        "␘",
        "␙",
        "␚",
        "␛",
        "␜",
        "␝",
        "␞",
        "␟",
        " ",
        "!",
        "\"",
        "#",
        "$",
        " %",
        "&",
        "\'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "{",
        "|",
        "}",
        "~",
        "␡"};
            this.termBody.Charset2 = new string[] {
        "",
        "␁",
        "␂",
        "␃",
        "␄",
        "␅",
        "␆",
        "␇",
        "␈",
        "␉",
        "␊",
        "␋",
        "␌",
        "␍",
        "␎",
        "␏",
        "␐",
        "␑",
        "␒",
        "␓",
        "␔",
        "␕",
        "␖",
        "␗",
        "␘",
        "␙",
        "␚",
        "␛",
        "␜",
        "␝",
        "␞",
        "␟",
        " ",
        "!",
        "\"",
        "#",
        "$",
        " %",
        "&",
        "\'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "{",
        "|",
        "}",
        "~",
        "␡"};
            this.termBody.Charset3 = new string[] {
        "",
        "␁",
        "␂",
        "␃",
        "␄",
        "␅",
        "␆",
        "␇",
        "␈",
        "␉",
        "␊",
        "␋",
        "␌",
        "␍",
        "␎",
        "␏",
        "␐",
        "␑",
        "␒",
        "␓",
        "␔",
        "␕",
        "␖",
        "␗",
        "␘",
        "␙",
        "␚",
        "␛",
        "␜",
        "␝",
        "␞",
        "␟",
        " ",
        "!",
        "\"",
        "#",
        "$",
        " %",
        "&",
        "\'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "{",
        "|",
        "}",
        "~",
        "␡"};
            this.termBody.Columns = 80;
            this.termBody.CursorPos = new System.Drawing.Point(0, 0);
            this.termBody.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termBody.IsBlinking = false;
            this.termBody.IsBold = false;
            this.termBody.IsReverse = false;
            this.termBody.IsUnderline = false;
            this.termBody.Location = new System.Drawing.Point(0, 73);
            this.termBody.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.termBody.Name = "termBody";
            this.termBody.Rows = 24;
            this.termBody.Size = new System.Drawing.Size(806, 358);
            this.termBody.TabIndex = 0;
            // 
            // notify
            // 
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(61, 4);
            // 
            // SuperTermForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 461);
            this.Controls.Add(this.toolRoot);
            this.Controls.Add(this.statusRoot);
            this.Controls.Add(this.termBody);
            this.Controls.Add(this.menuRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuRoot;
            this.Name = "SuperTermForm";
            this.Text = "SuperTerm";
            this.menuRoot.ResumeLayout(false);
            this.menuRoot.PerformLayout();
            this.statusRoot.ResumeLayout(false);
            this.statusRoot.PerformLayout();
            this.toolRoot.ResumeLayout(false);
            this.toolRoot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TermScreen termBody;
        private System.Windows.Forms.Timer timerShowPos;
        private System.Windows.Forms.MenuStrip menuRoot;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripMenuItem menuDebugMode;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.ToolStripMenuItem menuTool;
        private System.Windows.Forms.ToolStripMenuItem menuScreenPos;
        private System.Windows.Forms.ToolStripMenuItem menuScriptEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuRunScript;
        private System.Windows.Forms.ToolStripMenuItem menuStopScript;
        private System.Windows.Forms.ToolStripMenuItem menuAddress;
        private System.Windows.Forms.NotifyIcon notifyServer;
        private System.Windows.Forms.ToolStripMenuItem menuSetting;
        private System.Windows.Forms.ToolStripMenuItem menuBaseSetting;
        private System.Windows.Forms.ToolStripMenuItem menuUserSetting;
        private System.Windows.Forms.StatusStrip statusRoot;
        private System.Windows.Forms.ToolStripStatusLabel sslblTip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel sslblTime;
        private System.Windows.Forms.ToolStrip toolRoot;
        private System.Windows.Forms.ToolStripButton toolConnect;
        private System.Windows.Forms.ToolStripButton toolBreak;
        private System.Windows.Forms.ToolStripButton toolHosts;
        private System.Windows.Forms.ToolStripButton toolScript;
        private System.Windows.Forms.Timer timerUpdatetime;
        private System.Windows.Forms.ToolStripProgressBar ssProcess;
        private System.Windows.Forms.ToolStripStatusLabel sslblPos;
        private System.Windows.Forms.ToolStripStatusLabel sslblPrintFilepath;
        private System.Windows.Forms.ToolStripStatusLabel sslblLight;
        private System.Windows.Forms.ToolStripStatusLabel sslblScript;
        private System.Windows.Forms.ToolStripComboBox toolHostlist;
        private System.Windows.Forms.ContextMenuStrip notify;
    }
}

