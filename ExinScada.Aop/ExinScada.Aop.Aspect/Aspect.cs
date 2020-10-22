using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Collections;
public abstract class Aspect : IMessageSink
{
    private SortedList m_BeforeAdvices;
    private SortedList m_AfterAdvices;
    private IMessageSink m_NextSink;

    public abstract IBeforeAdvice before { get; }

    public abstract IAfterAdvice after { get; }

    static Dictionary<string, Dictionary<string, string>> aspectInMethodDic = new Dictionary<string, Dictionary<string, string>>();

    public Aspect(IMessageSink nextSink)
    {
        m_NextSink = nextSink;
        m_BeforeAdvices = new SortedList();
        m_AfterAdvices = new SortedList();
    }
    public IMessageSink NextSink
    {
        get { return m_NextSink; }
    }
    public IMessage SyncProcessMessage(IMessage msg)
    {
        IMethodCallMessage call = msg as IMethodCallMessage;
        string methodName = call.MethodName.ToUpper();
        IBeforeAdvice before = FindBeforeAdvice(methodName);
        Dictionary<string, object> result = new Dictionary<string, object>();
        if (before != null)
        {
            result = before.BeforeAdvice(call);
        }
        IMessage retMsg = m_NextSink.SyncProcessMessage(msg);
        IMethodReturnMessage reply = retMsg as IMethodReturnMessage;
        IAfterAdvice after = FindAfterAdvice(methodName);
        if (after != null)
        {
            after.AfterAdvice(reply, result);
        }
        return retMsg;

    }
    public  virtual void ReadAspect(string aspectXml, string aspectName)
    {
           
    }
    protected virtual void AddBeforeAdvice(string methodName, IBeforeAdvice before)
    {
        lock (this.m_BeforeAdvices)
        {
            if (!m_BeforeAdvices.Contains(methodName))
            {
                m_BeforeAdvices.Add(methodName, before);
            }
        }
    }
    protected virtual void AddAfterAdvice(string methodName, IAfterAdvice after)
    {
        lock (this.m_AfterAdvices)
        {
            if (!m_AfterAdvices.Contains(methodName))
            {
                m_AfterAdvices.Add(methodName, after);
            }
        }
    }


    public IBeforeAdvice FindBeforeAdvice(string methodName)
    {
        IBeforeAdvice before;
        lock (this.m_BeforeAdvices)
        {
            before = (IBeforeAdvice)m_BeforeAdvices[methodName];

        }
        return before;
    }
    public IAfterAdvice FindAfterAdvice(string methodName)
    {
        IAfterAdvice after;
        lock (this.m_AfterAdvices)
        {
            after = (IAfterAdvice)m_AfterAdvices[methodName];
        }
        return after;
    }



    public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {

        return null;
    }
}


