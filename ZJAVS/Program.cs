using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch =true)]
namespace ZJAVS
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.LogInfo("Entering Appliction.");
            Logger.LogHardware(" Reader  : testing. #1 ");
            Logger.LogHardware(" Indicator : testing.");
            Logger.LogHardware(" Controller  : testing. #2 ");
            Logger.LogHardware(" Reader  : testing. #3 ");

      //      AVSConfiguration.GetAVSConfiguration().EnableLoggingReader(0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Logger.LogInfo("Exiting Application.");

        }
    }
}
