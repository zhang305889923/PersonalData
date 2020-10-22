using System;
using System.Collections.Generic;
using System.Text;
using ExinScada.Aop.ExceptionIntercepter;
using log4net;
namespace ConsoleApplicationForTest
{
    [ExceptionIntercepterLogAttribute]
    class Class1 : ContextBoundObject
    {
        public void Add()
        {
            
            Console.WriteLine("你好ＡＯＰ加法！");
            throw new Exception("加法异常!");
        }
        public void  SUBSTRACT()
        {
            Console.WriteLine("你好ＡＯＰ减法！");
            throw new Exception("减法异常!");
        }
        public string Name
        {
            get;
            set;
        }
    }
}
