using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using ExinScada.Aop;
namespace ExinScada.Aop.ExceptionIntercepter
{
    public class ExceptionIntercepterProperty : AOPProperty
    {
        protected override IMessageSink CreateAspect(IMessageSink nextSink)
        {
            return new ExceptionIntercepterAspect(nextSink);
        }
        protected override string GetName()
        {
            return "ExceptionIntercepterAOP";
        }
    }
}
