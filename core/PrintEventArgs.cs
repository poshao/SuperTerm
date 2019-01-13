using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spoonson.Apps.SuperTerm
{
    public class PrintEventArgs :EventArgs
    {
        /// <summary>
        /// 输出文件路径
        /// </summary>
        public string FileName { get; set; } = "";

        public PrintEventArgs() { }

        public PrintEventArgs(string filename)
        {
            FileName = filename;
        }
    }
}
