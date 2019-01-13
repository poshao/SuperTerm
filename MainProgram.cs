using System;
using System.Windows.Forms;
using Spoonson.Common;

namespace Spoonson.Apps.SuperTerm
{
    static class MainProgram
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Info("加载配置文件");
            Spoonson.Common.Config.Load();

            Logger.Info("加载主界面");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SuperTermForm());
        }
    }
}
