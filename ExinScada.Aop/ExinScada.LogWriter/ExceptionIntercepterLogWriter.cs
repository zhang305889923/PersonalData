using System;
using System.Collections.Generic;
using System.Text;
using log4net;
namespace ExinScada.Aop.LogWriter
{
    public class ExceptionIntercepterLogWriter:LogWriter
    {
       private static readonly ExceptionIntercepterLogWriter instance = new ExceptionIntercepterLogWriter();
       ILog performanceLog;
       public ExceptionIntercepterLogWriter()
       {
           performanceLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   
       }
       public static ExceptionIntercepterLogWriter Instance
       {
           get { return instance; }
       }
       internal override log4net.ILog Log
       {
           get { return performanceLog; }
       }
    }
}
