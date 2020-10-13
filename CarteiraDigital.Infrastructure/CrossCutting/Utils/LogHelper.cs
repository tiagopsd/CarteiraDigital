using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure.CrossCutting.Utils
{
    public static class LogHelper
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Error(string error)
        {
            _log.Error(error);
        }

    }
}
