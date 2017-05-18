using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDit.Component.Tools
{
    public class LogHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Loggering");

        public void LogError(string message) {
            log.Error(message);
        }

    }
}