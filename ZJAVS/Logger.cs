using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;


namespace ZJAVS
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger("SystemLog");
        private static readonly ILog logHardware = LogManager.GetLogger("HardwareLog");

        public static void LogError(string message)
        {
            log.Error(message);
        }

        public static void LogInfo(string message)
        {
            log.Info(message);
        }

        public static void LogHardware(string message)
        {
            logHardware.Debug(message);
        }
    }
}
