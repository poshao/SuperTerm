/**
 * 说明: 字符单元
 * 作者: Byron Gong
 * 时间: 2018/10/21 16:21
 */
namespace Spoonson.Apps.SuperTerm
{
    /// <summary>
    /// 字符单元
    /// </summary>
    class CharItem
    {
        private char m_value = '\0';
        private bool m_bold = false;
        private bool m_underline = false;
        private bool m_blink = false;
        private bool m_reverse = false;
        private string[] m_charset = Charsets.ASCII;

        /// <summary>
        /// 绘制文字
        /// </summary>
        public char Value
        {
            get { return m_value; }
            set
            {
                if (m_value != value)
                {
                    m_value = value;
                    Updated = false;
                }
            }
        }
        /// <summary>
        /// 显示文字
        /// </summary>
        public string[] Charset
        {
            get { return m_charset; }
            set
            {
                if (value == null)
                {
                    System.Diagnostics.Debug.Write("1");
                }
                if (m_charset != value)
                {
                    m_charset = value;
                    Updated = false;
                }
            }
        }
        /// <summary>
        /// 标识是否粗体
        /// </summary>
        public bool Bold
        {
            get { return m_bold; }
            set
            {
                if (m_bold != value)
                {
                    m_bold = value;
                    Updated = false;
                }
            }
        }
        /// <summary>
        /// 标识是否下划线
        /// </summary>
        public bool Underline
        {
            get { return m_underline; }
            set
            {
                if (m_underline != value)
                {
                    m_underline = value;
                    Updated = false;
                }
            }
        }
        /// <summary>
        /// 标识是否闪烁
        /// </summary>
        public bool IsBlinking
        {
            get { return m_blink; }
            set
            {
                if (m_blink != value)
                {
                    m_blink = value;
                    Updated = false;
                }
            }
        }

        /// <summary>
        /// 显示反转
        /// </summary>
        public bool IsReverse
        {
            get { return m_reverse; }
            set
            {
                if (m_reverse != value)
                {
                    m_reverse = value;
                    Updated = false;
                }
            }
        }
        /// <summary>
        /// 标识是否已经更新到界面
        /// </summary>
        public bool Updated { get; set; } = false;
        /// <summary>
        /// 显示字符
        /// </summary>
        public string Display
        {
            get { return Charset[Value]; }
        }

        public CharItem()
        {
        }

        /// <summary>
        /// 恢复初始状态
        /// </summary>
        public void ToDefault()
        {
            m_value = '\0';
            m_blink = false;
            m_bold = false;
            m_underline = false;
            m_reverse = false;
            m_charset = Charsets.ASCII;
            Updated = false;
        }

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="item"></param>
        public void Copy(CharItem item)
        {
            Value = item.Value;
            IsBlinking = item.IsBlinking;
            Bold = item.Bold;
            Underline = item.Underline;
            IsReverse = item.IsReverse;
            Charset = item.Charset;
        }
    }
}
