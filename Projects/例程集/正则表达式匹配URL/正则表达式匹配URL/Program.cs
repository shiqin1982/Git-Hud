using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;


namespace 正则表达式匹配URL
{
    class Program
    {
        static void Main(string[] args)
        {
            string Pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
            Regex r = new Regex(Pattern);
            string source = "http://www.jb51.net";
            Match m = r.Match(source);
            if(m.Success)
            {
                Console.WriteLine("URL验证成功！");
            }
            else
            {
                Console.WriteLine("URL验证失败！");
            }
            Console.ReadLine();
        }
    }
}
