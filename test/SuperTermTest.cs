using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spoonson.Apps.SuperTerm.test
{
    class SuperTermTest : MarshalByRefObject,ISuperTerm
    {
        public string GetString(int row, int column, int len)
        {
            throw new NotImplementedException();
        }

        public bool SendCommands(byte[] commands)
        {
            Console.WriteLine(Encoding.ASCII.GetString(commands));
            return true;
            //throw new NotImplementedException();
        }

        public void SetOutputPath(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Sleep(int milliseconds)
        {
            Console.WriteLine("Sleep()");
            //throw new NotImplementedException();
        }

        public bool WaitString(int row, int column, int len, string target, int milliseconds)
        {
            throw new NotImplementedException();
        }
    }
}
