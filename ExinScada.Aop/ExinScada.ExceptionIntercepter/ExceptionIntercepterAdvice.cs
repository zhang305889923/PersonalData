using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using ExinScada.Aop;
using ExinScada.Aop.LogWriter;


namespace ExinScada.Aop.ExceptionIntercepter
{
    public class ExceptionIntercepterAdvice :IBeforeAdvice,IAfterAdvice
    {
        private static readonly ExceptionIntercepterAdvice instance = new ExceptionIntercepterAdvice();
        public static ExceptionIntercepterAdvice Instance { get { return instance; } }
        string logFormat = "{0}\r\n{1}";
        public  ExceptionIntercepterAdvice()
        {

        }
        public void AfterAdvice(IMethodReturnMessage returnMsg, Dictionary<string, object> paramDic)
        {
            if (returnMsg == null)
            {
                return;
            }
            else if (returnMsg.Exception != null)
            {
              
                ExceptionIntercepterLogWriter.Instance.LogFormat(logFormat, new string[2] { "错误信息:"+returnMsg.Exception.Message,"错误的代码位置:"+returnMsg.Exception.StackTrace.ToString()});
            }
        }
        Dictionary<string, object> IBeforeAdvice.BeforeAdvice(IMethodCallMessage callMsg)
        {
            return null;
        } 
       
    }
}
