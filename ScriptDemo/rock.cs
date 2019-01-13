using System;
using System.Text;
using System.Windows.Forms;

namespace UserSpace
{
    public partial class UserCode
    {
        /// <summary>
        /// 用户代码逻辑
        /// 改卡板操作
        /// </summary>
        public void UserScript()
        {
            using (var ofn=new OpenFileDialog())
            {
                ofn.Title = "请选择该卡板文件";
                if (ofn.ShowDialog() != DialogResult.OK) return;
                var lines = System.IO.File.ReadAllLines(ofn.FileName);
                string strRockNumber = lines[0];
                for(int i = 1; i < lines.Length; i++)
                {
                    for (int t = 0; t < 3; t++){
                        if (ChangeLoc(lines[i], strRockNumber))
                        {
                            break;
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine(string.Format("卡板[{0}]修改失败",lines[i]));
                        }
                    }
                }
            }
        }

        public bool ChangeLoc(string cartonNumber,string rockNumber)
        {
            if (!WaitString(3, 29, "MOVING CARTON", 2000) && CursorX==44 && CursorY==5)
            {
                Console.WriteLine("初始界面异常");
                SendKey(Keys.F4);
                SendKey(Keys.F4);
                SendKey(Keys.F4);
                SendKey(Keys.F4);
                if (!WaitString(3, 35, "Main Menu", 2000)) return false;
                SendData("OLXC");
                return false;
            }
            SendData(cartonNumber);
            if (!WaitString(5, 44, cartonNumber, 2000)) return false;
            SendData("\r");

            if (!WaitString(21, 14, "not FOUND", 2000))
            {
                SendData(" ");
                Console.WriteLine("找不到箱号" + cartonNumber);
                return false;//找不到箱号
            }
            SendData(rockNumber);
            if (!WaitString(9, 44, cartonNumber, 2000)) return false;
            SendData("Y\r\r");
            return true;
        }
    }
}