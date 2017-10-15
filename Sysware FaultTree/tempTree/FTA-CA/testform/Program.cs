using System;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BNWindowsInterface
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
