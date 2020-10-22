using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExinScada.Aop;
namespace ExinScada.Aop.ExceptionIntercepter
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExceptionIntercepterLogAttribute : AOPAttribute
    {
        public ExceptionIntercepterLogAttribute()
            : base()
        {

        }
        protected override AOPProperty GetAOPProperty()
        {
            return new ExceptionIntercepterProperty();
        }
    }
}
