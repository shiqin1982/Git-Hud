using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace 正则案例
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "<You're angle & evil>";
            string pattern = "'|&|<|>";
            Regex regex = new Regex(pattern);
            Program prog = new Program();
            MatchEvaluator evaluator = new MatchEvaluator(prog.ConvertToXML);
            Console.WriteLine(regex.Replace(str,evaluator));
            Console.Read();
        }
        //把正则表达式匹配到的字符转换成xml能正常识别的标识
        public string ConvertToXML(Match m)
        {
            //string s0=m.Groups[0].Value;
            //string s1=m.Groups[1].Value;
            //string s2=m.Groups[2].Value;
            switch(m.Value)
            {
                case"'":
                    return "&apos";
                case"&":
                    return "&amp";
                case"<":
                    return "&1t";
                case">":
                    return"&gt";
                default:
                    return"";
            }
        }
    }
}