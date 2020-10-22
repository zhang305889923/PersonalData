using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class AOPAttribute : Attribute, IContextAttribute
    {
        private string m_AspectXml;
        private const string CONFIGFILE = @"configuration\aspect.xml";
        public AOPAttribute()
        {
            m_AspectXml = CONFIGFILE;

        }
        /// <summary>
        /// 构造AOP特性时指定的方面内容存放的XML文件路径
        /// </summary>
        /// <param name="aspectXml">方面内容XML文件存放路径</param>
        public AOPAttribute(string aspectXml)
        {
            this.m_AspectXml = aspectXml;
        }  
        protected abstract AOPProperty GetAOPProperty();

        #region IContextAttribute Members
        public void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
        {
            AOPProperty property = GetAOPProperty();
            property.AspectXml = m_AspectXml;     
            ctorMsg.ContextProperties.Add(property);
        }
        public bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
        {
            return false;
        }
        #endregion
    }

