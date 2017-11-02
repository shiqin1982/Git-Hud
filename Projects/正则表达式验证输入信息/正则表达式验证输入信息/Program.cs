using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace 正则表达式验证输入信息
{
    class Program
    {
        string address = this.TextBoxAddr.Text.ToUpper();//正则表达式[abcd]|[ABCD][5-9]|1[012]|1[5-9]|2[0123]|2[5-9]|3[0][a-h]|[A-H]
        string RegexStr = @"A|B|C|D[5-9]|1[012]|1[5-9]|2[0123]|2[5-9]|30A|B|C|D|E|F|G|H[]";
        MessageBox.Show(address);
        MatchCollection mc=Regex.Matches(address,@"A|B|C|D[5-9]|1[012]|1[5-9]|2[0123]|2[5-9]|30A|B|C|D|E|F|G|H");
        foreach(Match item in mc)
        {
            MessageBox.Show(item.Value);
        }
        if(Regex.IsMatch(address,@"A|B|C|D[5-9]|1[012]|1[5-9]|2[0123]|2[5-9]|30A|B|C|D|E|F|G|H"))
{
    Message Box.Show(address+"匹配"+RegexStr);
}
    else
{
    MessageBox.Show(address+"不匹配"+RegexStr);
}
        static void Main(string[] args)
        {
        }
    }
}
