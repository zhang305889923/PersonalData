using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
public interface IAfterAdvice
{
    void AfterAdvice(IMethodReturnMessage returnMsg, Dictionary<string, object> beforeResult);
}

