using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace SpoonTerm
{
    class SpoonTerm
    {
        public void Test()
        {
            string strPhxHost = "shk.ap.averydennison.net";
            System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(strPhxHost, 23);
            tcpClient.ReceiveTimeout = 600;
            Debug.WriteLine(tcpClient.Connected.ToString());
            
            var ns = tcpClient.GetStream();
            System.Threading.Thread.Sleep(1000);
            while (ns.DataAvailable)
            {
                System.Diagnostics.Debug.WriteLine(ns.ReadByte().ToString());
                ns.Position--;
            }


            //var buffer = new Byte[256];
            //int readCount= ns.Read(buffer, 0, buffer.Length);

            //var response=System.Text.Encoding.ASCII.GetString(buffer);
            //System.Diagnostics.Debug.Write(response);
            //while (ns.CanRead)
            //{
            //    System.Diagnostics.Debug.Write(ns.ReadByte().ToString());
            //}
        }

        public void Test2()
        {
            var th = new TelnetHelper("shk.ap.averydennison.net",23,null);
            th.Process();
        }
    }
}
