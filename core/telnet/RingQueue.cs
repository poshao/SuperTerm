/**
 * 说明: 环形队列
 * 作者: Byron Gong
 * 时间: 2018/10/14 20:34
 */
using System;

namespace Spoonson.Apps.SuperTerm
{
    /// <summary>
    /// 环形队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class RingQueue<T>
    {
        private int _capacity;//容量
        private int _count=0;//元素数量

        public ValueClass<T> Origin = null;//环起点

        public ValueClass<T> HEAD=null;//指向队列头部
        public ValueClass<T> FOOT = null;//指向队列尾部
        public ValueClass<T> Current = null;//指向当前选中元素

        /// <summary>
        /// 队列容量
        /// </summary>
        public int RingCapacity { get => _capacity; }
        
        /// <summary>
        /// 可用数量
        /// </summary>
        public int Available { get => _capacity-_count; }
        
        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count { get => _count; set => _count = value; }

        /// <summary>
        /// 初始化队列
        /// </summary>
        /// <param name="capacity"></param>
        public RingQueue(int capacity)
        {
            _capacity = capacity;
            Origin = new ValueClass<T>();
            ValueClass<T> p=Origin;
            for(int i = 1; i < capacity; i++)
            {
                var tmp = new ValueClass<T>();
                tmp.prev = p;
                p.next = tmp;
                p = tmp;
            }
            Origin.prev = p;
            p.next = Origin;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Count = 0;
            HEAD = null;
            FOOT = null;
            Current = null;
        }

        /// <summary>
        /// 尾部添加新元素
        /// </summary>
        /// <param name="v"></param>
        public void Push(T v)
        {
            if (_count == _capacity)
            {
                throw new Exception("元素溢出");
            }
            if (HEAD == null)
            {
                HEAD = Origin;
                FOOT = Origin;
                HEAD.Value = v;
            }
            else
            {
                FOOT.next.Value = v;
                FOOT = FOOT.next;
            }
            _count++;
        }

        /// <summary>
        /// 头部添加新元素
        /// </summary>
        /// <param name="v"></param>
        public void Unshift(T v)
        {
            if (_count == _capacity)
            {
                throw new Exception("元素溢出");
            }
            if (HEAD == null)
            {
                HEAD = Origin;
                FOOT = Origin;
                HEAD.Value = v;
            }
            else
            {
                HEAD.prev.Value = v;
                HEAD = HEAD.prev;
            }
            _count++;
        }

        /// <summary>
        /// 从尾部弹出一个结果
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T v;
            if (_count == 0)
            {
                throw new Exception("队列内无元素");
            }
            v = FOOT.Value;
            if (_count == 1)
            {
                HEAD = null;
                FOOT = null;
                Current = null;
            }
            else
            {
                FOOT = FOOT.prev;
            }

            _count--;
            return v;
        }

        /// <summary>
        /// 从头部弹出一个元素
        /// </summary>
        /// <returns></returns>
        public T Shift()
        {
            T v;
            if (_count == 0)
            {
                throw new Exception("队列内无元素");
            }
            v = HEAD.Value;
            if (_count == 1)
            {
                HEAD = null;
                FOOT = null;
                Current = null;
            }
            else
            {
                HEAD = HEAD.next;
            }

            _count--;
            return v;
        }

        public void PushValues(T[] vs)
        {
            //if (Available < vs.Length) return;
            foreach(T v in vs)
            {
                Push(v);
            }
        }

        public void UnshiftValues(T[] vs)
        {
            foreach (T v in vs)
            {
                Unshift(v);
            }
        }

        public T[] PopValues(int count)
        {
            if (count > _count)
            {
                throw new Exception("元素数量不足");
            }
            var rs = new T[count];
            for(int i = 0; i < count; i++)
            {
                rs[i] = Pop();
            }
            return rs;
        }

        public T[] ShiftValues(int count)
        {
            if (count > _count)
            {
                throw new Exception("元素数量不足");
            }
            var rs = new T[count];
            for (int i = 0; i < count; i++)
            {
                rs[i] = Shift();
            }
            return rs;
        }
        #region 游标
        public T Value
        {
            get
            {
                if (Current == null) return default(T);
                return Current.Value;
            }
        }

        public bool MoveFirst()
        {
            Current = HEAD;
            return (Current!=null);
        }

        public bool MoveNext()
        {
            if (Current != null && Current != FOOT)
            {
                Current = Current.next;
                return true;
            }
            return false;
        }

        public bool MovePrev()
        {
            if(Current!=null && Current != HEAD)
            {
                Current = Current.prev;
                return true;
            }
            return false;
        }

        public T GetFirstValue()
        {
            if (MoveFirst())
            {
                return Current.Value;
            }
            return default(T);
        }

        public T GetNextValue()
        {
            if (MoveNext())
            {
                return Current.Value;
            }
            return default(T);
        }

        /// <summary>
        /// 移除Current左边的元素(包括Current)
        /// </summary>
        public object[] LTrim()
        {
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            while(Current!=null && Current != HEAD)
            {
                al.Add(Shift());
            }
            if (Current != null)
            {
                al.Add(Shift());
                Current = null;
            }
            return al.ToArray();
        }
        /// <summary>
        /// 移除Current右边的元素(包括Current)
        /// </summary>
        public object[] RTrim()
        {
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            while (Current != null && Current != FOOT)
            {
                al.Add(Pop());
            }
            if (Current != null)
            {
                al.Add(Pop());
                Current = null;
            }
            return al.ToArray();
        }

        #endregion

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] v = new T[Count];
            ValueClass<T> p=HEAD;
            for(int i = 0; i < Count; i++)
            {
                v[i] = p.Value;
                p = p.next;
            }
            return v;
        }
    }

    /// <summary>
    /// 元素定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ValueClass<T>
    {
        public ValueClass<T> next=null;
        public ValueClass<T> prev = null;
        public T Value;

        public ValueClass()
        {
            ;
        }
        public ValueClass(T v)
        {
            Value = v;
        }
    }
}
