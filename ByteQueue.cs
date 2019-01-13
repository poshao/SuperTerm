using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpoonTerm
{
    /// <summary>
    /// 字节序列
    /// </summary>
    class ByteQueue
    {
        public ByteClass Head;
        public ByteClass Foot;
        public ByteClass Current;
        private int _count;

        public int Count => _count;

        public ByteQueue()
        {

        }

        /// <summary>
        /// 在队列尾部添加一个元素
        /// </summary>
        /// <param name="b"></param>
        public void push(byte b)
        {
            var nb = new ByteClass(b);
            if (Count == 0)
            {
                Head = nb;
                Current = nb;
                Foot = nb;
            }
            else
            {
                nb.prev = Foot;
                Foot.next = nb;
                Foot = nb;
            }
            _count++;
        }

        /// <summary>
        /// 从尾部弹出一个元素
        /// </summary>
        /// <returns></returns>
        public byte pop()
        {
            if (Count == 0)
            {
                throw new Exception("队列内无元素");
            }
            byte p = Foot.Value;
            if (Current == Foot)
            {
                Current = null;
            }
            if (_count==1)
            {
                Head = null;
                Foot = null;
                Current = null;
            }
            else
            {
                Foot = Foot.prev;
                Foot.next = null;
            }
            _count--;
            return p;
        }

        //在头部添加一个元素
        public void unshift(byte b)
        {
            ByteClass nb = new ByteClass(b);
            if (Count == 0)
            {
                Head = nb;
                Current = nb;
                Foot = nb;
            }
            else
            {
                nb.next = Head;
                Head.prev = nb;
                Head = nb;
            }
            _count++;
        }

        /// <summary>
        /// 从头部弹出一个元素
        /// </summary>
        /// <returns></returns>
        public byte shift()
        {
            if (Count == 0)
            {
                throw new Exception("队列内无元素");
            }
            byte p = Head.Value;
            if (Current == Head)
            {
                Current = null;
            }
            if (_count == 1)
            {
                Head = null;
                Foot = null;
                Current = null;
            }
            else
            {
                Head = Head.next;
                Head.prev = null;
            }
            _count--;
            return p;
        }

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] toArray()
        {
            byte[] rst = new byte[Count];
            ByteClass p = Head;

            for (int i = 0; i < Count; i++)
            {
                rst[i] = p.Value;
                p = p.next;
            }
            return rst;
        }
    }

    /// <summary>
    /// 字节节点
    /// </summary>
    class ByteClass{
        public byte Value { get; set; }
        public ByteClass next=null;
        public ByteClass prev=null;

        public ByteClass(byte b)
        {
            Value = b;
        }
    }
}
