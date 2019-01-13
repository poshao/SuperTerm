using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spoonson.Apps.SuperTerm {
    /// <summary>
    /// 屏幕数据
    /// </summary>
    class ScreenData:MarshalByRefObject
    {
        private CharItem[,] m_viewData = null;

        private int Rows { get; set; } = 0;
        private int Columns { get; set; } = 0;

        #region 属性
        public CharItem[,] Data
        {
            get { return m_viewData; }
            set {
                m_viewData = value;
                Rows = m_viewData.GetLength(0);
                Columns = m_viewData.GetLength(1);
            }
        }

        #endregion

        /// <summary>
        /// 获取指定位置的内容
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CharItem GetItem(int x, int y)
        {
            return m_viewData[x, y];
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="item"></param>
        public void SetItem(int x, int y, CharItem item)
        {
            m_viewData[x, y] = item;
        }

        /// <summary>
        /// 获取文字
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string GetString(int x, int y)
        {
            return GetItem(x, y).Display;
        }

        /// <summary>
        /// 屏幕移动行数
        /// </summary>
        /// <param name="offset">负数为下移</param>
        public void ScreenMoveUpLines(int offset=1)
        {
            offset = offset % Rows;
            if (offset > 0)
            {
                for(int i = 0; i < Rows - offset; i++)
                    for(int j = 0; j < Columns; j++)
                    {
                        m_viewData[i, j].Copy(m_viewData[i+offset,j]);
                    }
                for(int i = 0; i < offset; i++)
                {
                    LineClear(Rows - 1 - i);
                }
            }
            else
            {
                offset = offset * -1;
                for (int i = Rows-1; i > offset-1; i--)
                    for (int j = 0; j < Columns; j++)
                    {
                        m_viewData[i, j].Copy(m_viewData[i - offset, j]);
                    }
                for (int i = 0; i < offset; i++)
                {
                    LineClear(i);
                }
            }
        }

        /// <summary>
        /// 清除行
        /// </summary>
        /// <param name="line"></param>
        public void LineClear(int line)
        {
            for (int j = 0; j < Columns; j++)
            {
                m_viewData[line, j].ToDefault();
            }
        }

        /// <summary>
        /// 清除行指定位置后面的数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void LineClearAfterPos(int x,int y)
        {
            for (int i =x; i < Columns; i++)
            {
                m_viewData[y, i].ToDefault();
            }
        }

        /// <summary>
        /// 清除行指定位置前面的数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void LineClearBeforePos(int x,int y)
        {
            for (int i = x; i >= 0; i--)
            {
                m_viewData[y, i].ToDefault();
            }
        }

        /// <summary>
        /// 清除屏幕
        /// </summary>
        public void ScreenClear()
        {
            for(int i=0;i<Rows;i++)
                for(int j = 0; j < Columns; j++)
                {
                    Data[i, j].ToDefault();
                }
        }

        /// <summary>
        /// 清除屏幕指定位置前的数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ScreenClearBeforePos(int x,int y)
        {
            for (int i = 0; i < y; i++)
            {
                LineClear(i);
            }
            LineClearBeforePos(x, y);
        }

        /// <summary>
        /// 清除屏幕指定位置后的数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ScreenClearAfterPos(int x,int y)
        {
            for (int i = y + 1; i < Rows; i++)
            {
                LineClear(i);
            }
            LineClearAfterPos(x, y);
        }
    }
}
