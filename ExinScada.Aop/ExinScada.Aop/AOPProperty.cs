using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

    public abstract class AOPProperty : IContextProperty, IContributeServerContextSink
    {
        private string m_AspectXml;
        public AOPProperty()
        {
            m_AspectXml = string.Empty;
        }
        public string AspectXml
        {
            set { m_AspectXml = value; }
        }
        protected abstract IMessageSink CreateAspect(IMessageSink nextSink);
        protected virtual string GetName()
        {
            return "AOP";
        }
        protected virtual void FreezeImpl(Context newContext)
        {
            return;
        }
        protected virtual bool CheckNewContext(Context newCtx)
        {
            return true;
        }

        #region IContextProperty Members
        public void Freeze(Context newContext)
        {
            FreezeImpl(newContext);
        }
        public bool IsNewContextOK(Context newCtx)
        {
            return CheckNewContext(newCtx);
        }
        public string Name
        {
            get { return GetName(); }
        }
        #endregion

        #region IContributeServerContextSink Members
        public IMessageSink GetServerContextSink(IMessageSink nextSink)
        {
            Aspect aspect = (Aspect)CreateAspect(nextSink);
            aspect.ReadAspect(m_AspectXml, Name);           
            return (IMessageSink)aspect;
        }
        #endregion


    }

