using System;
using System.Text;
using System.Windows.Forms;
namespace UserSpace
{
    public partial class UserCode
    {
        /// <summary>
        /// �û������߼�
        /// </summary>
        public void UserScript()
        {
            //���Login����
            if (!WaitString(23, 0, "login", 2000)) return;

            Console.WriteLine("Input Login Name");
            SendData("byrong\r");
            if (!WaitString(23, 9, "Password", 2000)) return;

            Console.WriteLine("input Password");
            SendData("qwe123qwe\r1\r");

            if (!WaitString(15, 32, "Password", 2000)) return;
            SendData("\thello128\r\n");

            //���������
            if (!WaitString(3, 35, "Main Menu", 2000)) return;
        }
    }
}