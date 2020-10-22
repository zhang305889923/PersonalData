using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExinScada.Aop;
using System.Runtime.Remoting.Messaging;
namespace ExinScada.Aop.ExceptionIntercepter
{
    public class ExceptionIntercepterAspect : Aspect
    {
        public ExceptionIntercepterAspect(IMessageSink nextSink)
            : base(nextSink)
        {

        }
        public override IBeforeAdvice before
        {
            get
            {
                return null;
            }

        }
        public override IAfterAdvice after
        {
            get
            {
                return ExceptionIntercepterAdvice.Instance;
            }

        }
        public   override void ReadAspect(string aspectXml, string aspectName)
        {
            IBeforeAdvice before = (IBeforeAdvice)ConfigurationXml.GetAdvice(aspectXml, aspectName, AdviceType.Before);
            string[] methodNames = ConfigurationXml.GetNames(aspectXml, aspectName, AdviceType.Before);
            foreach (string name in methodNames)
            {
                AddBeforeAdvice(name, before);
            }
            IAfterAdvice after = (IAfterAdvice)ConfigurationXml.GetAdvice(aspectXml, aspectName, AdviceType.After);
            methodNames = ConfigurationXml.GetNames(aspectXml, aspectName, AdviceType.After);
            foreach (string name in methodNames)
            {
                AddAfterAdvice(name, after);
            }
        }
    }
}
