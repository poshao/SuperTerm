using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Spoonson.Apps.SuperTerm
{
    public partial class TermScreen : UserControl
    {

        /// <summary>
        /// 显示数据
        /// </summary>
        //private CharItem[,] m_viewData = null;
        private ScreenData m_data = null;

        /// <summary>
        /// 缓存图像
        /// </summary>
        private Bitmap m_image = null;
        /// <summary>
        /// 绘图对象
        /// </summary>
        private Graphics m_graphics = null;

        private int m_rows = 24;
        private int m_columns = 80;

        private Point m_cursorPos = new Point(0, 0);

        #region 属性

        /// <summary>
        /// 列数
        /// </summary>
        public int Columns {
            get { return m_columns; }
            set {
                if (m_columns != value)
                {
                    m_columns=value;
                    InitData();
                }
            }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows
        {
            get { return m_rows; }
            set
            {
                if (m_rows != value)
                {
                    m_rows = value;
                    InitData();
                }
            }
        }

        /// <summary>
        /// 设置光标位置
        /// </summary>
        public Point CursorPos {
            get { return m_cursorPos; }
            set
            {
                if (m_cursorPos != value)
                {
                    m_cursorPos=value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 字符样式
        /// </summary>
        public bool IsBold { get; set; } = false;
        public bool IsUnderline { get; set; } = false;
        public bool IsBlinking { get; set; } = false;
        public bool IsReverse { get; set; } = false;

        /// <summary>
        /// 字符集设定
        /// </summary>
        public string[] Charset0 { get; set; } = Charsets.ASCII;
        public string[] Charset1 { get; set; } = Charsets.ASCII;
        public string[] Charset2 { get; set; } = Charsets.ASCII;
        public string[] Charset3 { get; set; } = Charsets.ASCII;
        /// <summary>
        /// 当前使用的字符集
        /// </summary>
        public string[] Charset { get; set; } = Charsets.ASCII;
        #endregion

        public TermScreen()
        {
            InitializeComponent();
            InitData();
            this.SetStyle(ControlStyles.Selectable, true);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            m_data = new ScreenData();
            m_data.Data = new CharItem[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    m_data.Data[i, j] = new CharItem();
                }
            PrePaint();
        }

        /// <summary>
        /// 全屏上移
        /// </summary>
        public void ScreenMoveUp()
        {
            m_data.ScreenMoveUpLines(1);
            //for (int i = 0; i < Rows - 1; i++)
            //    for (int j = 0; j < Columns; j++)
            //    {
            //        m_viewData[i, j].Value = m_viewData[i + 1, j].Value;
            //        m_viewData[i, j].Bold = m_viewData[i + 1, j].Bold;
            //        m_viewData[i, j].Underline = m_viewData[i + 1, j].Underline;
            //        m_viewData[i, j].Charset = m_viewData[i + 1, j].Charset;
                    
            //    }
            //LineClear(Rows - 1);
        }

        #region 控件事件
        /// <summary>
        /// 控件绘制事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (m_image != null)
            {
                e.Graphics.DrawImage(m_image, 0, 0);
                //绘制光标
                float itemWidth = Width / 80.0F;
                float itemHeight = Height / 24.0F;
                e.Graphics.DrawRectangle(Pens.Blue, new Rectangle((int)(itemWidth * CursorPos.X), (int)(itemHeight * CursorPos.Y), (int)itemWidth, (int)itemHeight));
            }
        }

        /// <summary>
        /// 更新绘图
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (m_data==null || m_data.Data == null) return;
            if (m_graphics != null) m_graphics.Dispose();
            if (m_image != null) m_image.Dispose();

            //初始化缓存图像
            m_image = new Bitmap(Width + 1, Height + 1);
            m_graphics = Graphics.FromImage(m_image);
            for(int i=0;i<Rows;i++)
                for(int j = 0; j < Columns; j++)
                {
                    m_data.Data[i, j].Updated = false;
                }
            PrePaint();
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);

        //    Console.WriteLine(string.Format("code:{0}\n data: {1} \n value: {2}\n", e.KeyCode.ToString(), e.KeyData.ToString(), e.KeyValue.ToString()));
        //    //System.Diagnostics.Debug.WriteLine(Console.CapsLock);
        //    //Console.WriteLine(e.KeyValue);
        //    //WriteChars(new char[] { Convert.ToChar(e.KeyValue) });
        //}

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            Console.WriteLine("Press " + e.KeyChar.ToString());
            /**
             * Ctrl + a~z ==> 1~26
             * 
             */
            //Console.WriteLine(e.KeyChar);
            //WriteChars(new char[] { e.KeyChar });
        }

        /// <summary>
        /// 判断是否为可输入的按键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            //return base.IsInputKey(keyData);
            return true;
        }
        #endregion

        #region 图像绘制
        /// <summary>
        /// 预绘制
        /// </summary>
        public void PrePaint()
        {
            if (m_data.Data == null || m_graphics == null) return;

            float itemWidth = Width / 80.0F;
            float itemHeight = Height / 24.0F;
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                {
                    var item = m_data.Data[r, c];
                    if (!item.Updated)
                    {
                        var charRect = new RectangleF
                        {
                            X = c * itemWidth,
                            Y = r * itemHeight,
                            Width = itemWidth,
                            Height = itemHeight
                        };

                        m_graphics.FillRectangle(new SolidBrush(IsReverse?ForeColor:BackColor), charRect);

                        //绘制边框

                        //m_graphics.DrawRectangle(Pens.Pink, new Rectangle((int)charRect.X, (int)charRect.Y, (int)charRect.Width, (int)charRect.Height));

                        //绘制文字
                        FontStyle fontStyle = FontStyle.Regular;
                        if (item.Bold) fontStyle |= FontStyle.Bold;
                        //if (item.Underline) fontStyle |= FontStyle.Underline;

                        var font = new Font(Font, fontStyle);
                        //m_graphics.DrawString(item.Value.ToString(), font, Brushes.Black, charRect);
                        //var sf = new StringFormat();
                        //sf.LineAlignment = StringAlignment.Center;
                        //m_graphics.DrawString(item.Value.ToString(), font, new SolidBrush(IsReverse ? BackColor : ForeColor), charRect);
                        //m_graphics.DrawString(Charset[item.Value], font, new SolidBrush(IsReverse ? BackColor : ForeColor), charRect);
                        m_graphics.DrawString(item.Display, font, new SolidBrush(IsReverse ? BackColor : ForeColor), charRect);

                        //绘制下划线
                        if (item.Underline)
                        {
                            m_graphics.DrawLine(new Pen(IsReverse ? BackColor : ForeColor), charRect.X, charRect.Bottom-1, charRect.Right, charRect.Bottom-1);
                        }
                        font.Dispose();
                        item.Updated = true;
                    }
                }
            Invalidate();
        }
        #endregion

        #region 字符输出
        /// <summary>
        /// 输出字节
        /// </summary>
        /// <param name="b"></param>
        public void WriteByte(byte b)
        {
            WriteBytes(new byte[] { b });
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteString(string value)
        {
            WriteChars(value.ToCharArray());
        }

        /// <summary>
        /// 输出字节流
        /// </summary>
        /// <param name="bytes"></param>
        public void WriteBytes(byte[] bytes)
        {
            WriteChars(bytes.Select(o=>Convert.ToChar(o)).ToArray());
        }

        /// <summary>
        /// 输出字符流
        /// </summary>
        /// <param name="chars"></param>
        public void WriteChars(char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                var b = chars[i];
                if (b == 13) continue;
                if (b == 10 || b == 13)
                {
                    CursorMoveDownFirst();
                }
                else
                {
                    var item=m_data.Data[CursorPos.Y, CursorPos.X];
                    item.Value = b;
                    item.Bold = IsBold;
                    item.Underline = IsUnderline;
                    item.IsBlinking = IsBlinking;
                    item.IsReverse = IsReverse;
                    item.Charset=Charset;

                    if (CursorPos.X == Columns - 1)
                    {
                        CursorMoveDownFirst();
                    }
                    else
                    {
                        CursorMoveRightColumns(1);
                    }
                }
               
                if(b==13 && i + 1<chars.Length && chars[i + 1] == 10)
                {
                    i++;
                }
            }
            PrePaint();
        }
        #endregion

        #region 清除类指令
        /// <summary>
        /// 清除光标所在行,光标之前的内容(包括光标),光标位置不变
        /// </summary>
        public void LineClearBeforeCursor()
        {
            for (int i = CursorPos.X; i >= 0; i--)
            {
                m_data.Data[CursorPos.Y, i].ToDefault();
            }
            PrePaint();
        }

        /// <summary>
        /// 清除光标所在行,光标之后的内容(包括光标),光标位置不变
        /// </summary>
        public void LineClearAfterCursor()
        {
            m_data.LineClearAfterPos(CursorPos.X, CursorPos.Y);
            //for (int i = CursorPos.X; i < Columns; i++)
            //{
            //    m_data.Data[CursorPos.Y, i].ToDefault();
            //}
            PrePaint();
        }

        /// <summary>
        /// 清除光标所在行
        /// </summary>
        public void LineClear()
        {
            m_data.LineClear(CursorPos.Y);
            //LineClear(CursorPos.Y);
        }

        /// <summary>
        /// 清空指定行
        /// </summary>
        /// <param name="row"></param>
        public void LineClear(int row)
        {
            m_data.LineClear(row);
            //for (int i = 0; i < Columns; i++)
            //{
            //    m_data.Data[row, i].ToDefault();
            //}
            PrePaint();
        }

        /// <summary>
        /// 清除光标之前的所有内容(包括光标)
        /// </summary>
        public void ScreenClearBeforeCursor()
        {
            m_data.ScreenClearBeforePos(CursorPos.X, CursorPos.Y);
            //for (int i = 0; i < CursorPos.Y; i++)
            //{
            //    LineClear(i);
            //}
            //LineClearBeforeCursor();
        }

        /// <summary>
        /// 清除光标之后的所有内容(包括光标)
        /// </summary>
        public void ScreenClearAfterCursor()
        {
            m_data.ScreenClearAfterPos(CursorPos.X, CursorPos.Y);
            //for (int i = CursorPos.Y + 1; i < Rows; i++)
            //{
            //    LineClear(i);
            //}
            //LineClearAfterCursor();
        }

        /// <summary>
        /// 清空屏幕
        /// </summary>
        public void ScreenClear()
        {
            m_data.ScreenClear();
            //for (int i = 0; i < Rows; i++)
            //    for (int j = 0; j < Columns; j++)
            //    {
            //        m_data.Data[i, j].ToDefault();
            //    }
            CursorSetPos(0, 0);
            PrePaint();
        }
        #endregion

        #region 光标移动指令
        /// <summary>
        /// 光标向上移动一行
        /// </summary>
        public void CursorMoveUp()
        {
            if(CursorPos.Y>0)
                CursorPos = new Point(CursorPos.X, CursorPos.Y - 1);
        }

        /// <summary>
        /// 光标向下移动一行
        /// </summary>
        public void CursorMoveDown()
        {
            if (CursorPos.Y == Rows - 1)
            {
                ScreenMoveUp();
            }
            else
            {
                CursorPos = new Point(CursorPos.X, CursorPos.Y + 1);
            }
        }

        /// <summary>
        /// 光标移动到下一行首位
        /// </summary>
        public void CursorMoveDownFirst()
        {
            CursorMoveDown();
            CursorPos = new Point(0, CursorPos.Y);
        }

        /// <summary>
        /// 光标向上移动指定行
        /// </summary>
        /// <param name="rows"></param>
        public void CursorMoveUpRows(int rows)
        {
            while (rows-- > 0)
            {
                CursorMoveUp();
            }
        }

        /// <summary>
        /// 光标向下移动指定行
        /// </summary>
        /// <param name="rows"></param>
        public void CursorMoveDownRows(int rows)
        {
            while (rows-- > 0)
            {
                CursorMoveDown();
            }
        }

        /// <summary>
        /// 光标向左移动指定列
        /// </summary>
        /// <param name="columns"></param>
        public void CursorMoveLeftColumns(int columns)
        {
            CursorPos = new Point(CursorPos.X-columns>0? CursorPos.X - columns:0, CursorPos.Y);
        }

        /// <summary>
        /// 光标向右移动指定列
        /// </summary>
        /// <param name="columns"></param>
        public void CursorMoveRightColumns(int columns)
        {
            CursorPos = new Point(CursorPos.X + columns >= Columns ? Rows-1 : CursorPos.X + columns, CursorPos.Y);
        }

        /// <summary>
        /// 设置光标位置
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void CursorSetPos(int row, int column)
        {
            CursorPos = new Point(column, row);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string GetString(int row,int col,int len)
        {
            string result = "";
            while (len-- > 0)
            {
                if(col<Columns && row < Rows)
                {
                    result += m_data.Data[row, col].Display;
                    col++;
                    if (col == Columns)
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 计算行号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point GetPos(int x,int y)
        {
            float itemWidth = Width / 80.0F;
            float itemHeight = Height / 24.0F;
            var pos = PointToClient(new Point(x, y));
            x = pos.X;
            y = pos.Y;
            return new Point((int)(x / itemWidth), (int)(y / itemHeight));
        }
        #endregion
    }
}
