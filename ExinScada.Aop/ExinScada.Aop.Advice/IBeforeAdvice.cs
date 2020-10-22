using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
public interface IBeforeAdvice
{
    Dictionary<string, object> BeforeAdvice(IMethodCallMessage callMsg);
}

