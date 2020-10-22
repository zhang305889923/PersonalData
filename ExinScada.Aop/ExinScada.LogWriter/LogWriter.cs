using System;
using System.Collections.Generic;
using System.Text;
using log4net;
namespace ExinScada.Aop.LogWriter
{
    public abstract class LogWriter
    {

        static LogWriter()
        {
           
        }
        public LogWriter()
        {
            log4net.Config.XmlConfigurator.Configure();   


        }
        internal abstract ILog Log
        {
            get;
        }

        public virtual void LogFormat(string format, params object[] args)
        {
            //if (Log.IsInfoEnabled)
            {
                var message = string.Format(format, args);
                Log.Info (message);
            }
        }



    }
}
