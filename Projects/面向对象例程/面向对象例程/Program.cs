using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向对象例程
{
    class Program
    {
        static void Main(string[] args)
        {
            基类 a = new 加法();
            a.x = 100m;
            a.y = 101.001m;
            decimal result = a.GetResult();
            基类 b = new 减法();
            b.x = 100m;
            b.y = 101.001m;
            decimal result1 = b.GetResult();
            Console.WriteLine(result);
            Console.WriteLine(result1);
            Console.ReadLine();
        }
        public abstract class 基类
        {
            public decimal x { get; set; }
            public decimal y { get; set; }
            public abstract decimal GetResult();
        }
        public class 加法:基类
        {
            public override decimal GetResult()
            {
                return x + y;
            }
        }
        public class 减法:基类
        {
            public override decimal GetResult()
            {
                return x - y;
            }
        }
    }
}
