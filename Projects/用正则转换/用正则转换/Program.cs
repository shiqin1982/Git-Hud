using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace 用正则转换
{
    class Program
    {
        //把阿拉伯数字的金额转换为中文大写数字
        static string ConvertToChinese(double x)
    {
        string s = 
            x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
        string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L/.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[/.]|$))))", "${b}${z}");
        return Regex.Replace(d, ".", delegate(Match m) { return "负元空零壹贰叄肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
    }
        static void Main(string[] args)
        {
            Random r = new Random();
            for(int i=0;i<10;i++)
            {
                double x = r.Next() / 100.0;
                Console.WriteLine("{0,14:N2}:{1}", x, ConvertToChinese(x));
            }
        }
    }
}
/*可能的输出：
 * 5,607,400.68：伍佰陆拾万柒仟肆佰元陆角捌分
 * 2,017,723.33：贰佰零壹万柒仟柒佰贰拾叁元叁角叁分
 *   751,181.17：柒拾伍万壹仟壹佰捌拾壹元壹角柒分
 * 7,849,851.53：柒佰捌拾肆万玖仟捌佰伍拾壹元伍角叁分
 * 2,629,143,90：贰佰陆拾贰万玖仟壹佰肆拾叁元陆角玖分
 * 13,461,629.68：壹仟叁佰肆拾陆万壹仟陆佰贰拾九元六角八分
 * 4,594,391.16：肆佰伍拾玖万肆仟叁佰玖拾壹元壹角陆分
 * 13,046,560.60：壹仟叁佰零肆万陆仟伍佰陆拾元陆角
 * 13,041,371.21：壹仟叁佰零肆万壹仟叁佰柒拾壹元贰角壹分
 * 20,639,609.44：二千零六拾叁万玖仟陆佰零玖元肆角肆分
*/
