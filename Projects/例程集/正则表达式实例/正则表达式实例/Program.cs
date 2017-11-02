using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace 正则表达式实例
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex("China", RegexOptions.IgnoreCase);
            //使用Match方法
            string source = "China is my MotherLand,my MotherLand is China!";
            Match m = regex.Match(source);
            if (m.Success)
            {
                Console.WriteLine("找到第一个匹配");
            }
            Console.WriteLine(new string('-', 9));
            //演示使用Matches方法进行匹配
            MatchCollection matches = regex.Matches(source);
            foreach (Match s in matches)
            {
                if (s.Success)
                    Console.WriteLine("找到了一个匹配");
            }
            Console.ReadLine();
        }
    }
}
