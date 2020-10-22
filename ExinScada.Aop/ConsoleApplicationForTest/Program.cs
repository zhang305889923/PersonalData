using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationForTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Class1 c = new Class1();
            try
            {
                c.Add();
            }
            catch (Exception ex)
            {
            }
            try
            {
                c.SUBSTRACT();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
