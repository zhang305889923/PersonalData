using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using ExinScada.Aop;
namespace ExinScada.Aop.ExceptionIntercepter
{
    /// <summary>
    /// 读取方面的Xml文件配置内容类
    /// </summary>
    public class ConfigurationXml
    {
        public ConfigurationXml()
        {
         
        }
        /// <summary>
        /// 获得通知
        /// </summary>
        /// <param name="aspectXml">方面xml文件路径</param>
        /// <param name="aspectName">方面名称</param>
        /// <param name="type">通知类型</param>
        /// <returns>返回指定的通知</returns>
        public static object GetAdvice(string aspectXml, string aspectName, AdviceType type)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(aspectXml);
            XmlNode xNode;
            XmlNodeList  xNodeAspectlist;
            xNode = xDoc.SelectSingleNode("//aop");
            xNodeAspectlist = xNode.SelectNodes("//aop//aspect[@value='" + aspectName + "']");
            foreach (XmlNode xAspectNode in xNodeAspectlist)
            {

                XmlNodeList xNodeAdviceList = xAspectNode.SelectNodes("//aop//aspect[@value='" + aspectName + "']//advice");
                foreach (XmlNode xAdviceNode in xNodeAdviceList)
                {
                    XmlElement xElem = (XmlElement)xAdviceNode;
                    string typeName = xElem.GetAttribute("type");
                    string assemblyName = xElem.GetAttribute("assembly");
                    string classname = xElem.GetAttribute("class");
                    if (typeName == type.ToString())
                    {
                        Assembly ass = Assembly.Load(assemblyName);
                        if (type == AdviceType.Before)
                        {
                            IBeforeAdvice p = (ExceptionIntercepterAdvice)ass.CreateInstance(classname);

                            return p;
                        }
                        else if (type == AdviceType.After)
                        {
                            IAfterAdvice p = (ExceptionIntercepterAdvice)ass.CreateInstance(classname);
                            return p;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获得方面相连接的切入点方法名称
        /// </summary>
        /// <param name="aspectXml">方面xml文件路径</param>
        /// <param name="aspectName">方面名称</param>
        /// <param name="type">通知类型</param>
        /// <returns>返回切入点方法名称</returns>
        public static string[] GetNames(string aspectXml, string aspectName, AdviceType type)
        {
            List<string> Names = new List<string>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(aspectXml);
            XmlNode xNode;
            XmlNodeList xNodePointCutlist;
            xNode = xDoc.SelectSingleNode("//aop");
            xNodePointCutlist = xNode.SelectNodes("//aop//aspect[@value='" + aspectName + "']//advice[@type='" + type.ToString() + "']//pointcut");
            foreach (XmlNode xPointCutNode in xNodePointCutlist)
            {

                XmlElement xElement = (XmlElement)xPointCutNode;
                Names.Add(xElement.InnerText);
            }
           
            return Names.ToArray();
        }
    }
}
