using System;
using System.Text;
using System.Windows.Forms;

namespace UserSpace
{
    public partial class UserCode
    {
        private bool IsOver = false;
        /// <summary>
        /// 用户代码逻辑
        /// </summary>
        public void UserScript()
        {
            //检查Login字样
            if (!WaitString(23, 0, "login", 2000)) return;

            Console.WriteLine("Input Login Name");
            SendData("byrong\r");
            if (!WaitString(23, 9, "Password", 2000)) return;

            Console.WriteLine("input Password");
            SendData("qwe123qwe\r1\r");

            if (!WaitString(15, 32, "Password", 2000)) return;
            SendData("\thello128\r\n");

            //检查主界面
            if (!WaitString(3, 35, "Main Menu", 2000)) return;
            Console.WriteLine("main menu");
            SendData("S95");
            if (!WaitString(2, 41, "Register", 2000)) return;
            Console.WriteLine("Register");
            SendData("\r\r\r\r\r\r\r\r\r\r\r\rlocal");
            if (!WaitString(18, 39, "local", 2000)) return;
            Console.WriteLine("local");
            SetOutputFileName(@"C:\phx.txt");
            SendData("\r");
            //等待报表完成
            while(!IsPrinting){
                Sleep(1000);
            }
            while (IsPrinting)
            {
                Sleep(1000);
            }
            Console.WriteLine("report finished!");
        }
    }
}