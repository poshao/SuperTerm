using System;
using System.Text;

namespace UserSpace
{
    public class UserCode : MarshalByRefObject
    {
        public UserCode(object a)
        {
            //var telnet = (ITelnetControl)a;
            Console.WriteLine("screen Data : " + telnet.GetString(1, 1, 20));
            //Console.WriteLine(a.GetType().ToString());
            //sendMessage(a,"byrong\r");
            //System.Threading.Thread.Sleep(1000);
            //sendMessage(a, "qwe123qwe\r1\r");
            //System.Threading.Thread.Sleep(1000);
            //sendMessage(a, "\thello128\r\n");
        }

        public void sendMessage(object a, string cmd)
        {
            a.GetType().InvokeMember("SendCommands", System.Reflection.BindingFlags.InvokeMethod, null, a, new object[] { Encoding.ASCII.GetBytes(cmd) });
        }

        public void run()
        {
            //m_tel.Sleep(3000);
            //m_tel.SendCommands(Encoding.ASCII.GetBytes("byrong"));
            Console.WriteLine("Hello World!");
            AppDomain.Unload(AppDomain.CurrentDomain);
        }
    }
}
