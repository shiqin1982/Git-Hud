using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace 正则表达式抓取网站信息
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        ///<summary>
        ///判断是否京东链接
        ///</summary>
        ///<param name="param"></param>
        ///<returns></returns>
        public bool ValidationUrl(string url)
        {
            bool result = false;
            if(!String.IsNullOrEmpty(url))
            {
                Regex regex = new Regex(@"^http://item.jd.com/\d+htmls$");
                Match match = regex.Match(url);
                if(match.Success)
                {
                    result=true;
                }
            }
            return result;
        }
        ///<summary>
        ///抓取京东信息
        ///</summary>
        ///<param name="param"></param>
        ///<returns></returns>
        public void GetInfo(string url)
        {
            if(ValidationUrl(url))
            {
                string htmlStr = WebHandler.GetHtmlStr(url, "Default");
                if(!String.IsNullOrEmpty(htmlStr))
                {
                    string pattern = ""; //正则表达式
                    string sourceWebID = "";//商品关键ID
                    string title = "";   //标题
                    decimal price = 0;   //价格
                    string picName = ""; //图片
                    //提取商品关键ID
                    pattern = @"http://item.jd.com/(?<Object>\d+).html";
                    sourceWebID = WebHandler.GetRegexText(url, pattern);
                    //提取标题
                    pattern = @"<div.*id=\""name\"".*>[\s\S]*<h1>(?<Object>.*?)<h1>";
                    title = WebHandler.GetRegexText(htmlStr, pattern);
                    //提取图片
                    int begin = htmlStr.IndexOf("<div id=\"spec-n1\"");
                    int end = htmlStr.IndexOf("</div id=\"spec-n1\"");
                    if(begin>0&&end>0)
                    {
                        string subPicHtml = htmlStr.Substring(begin, end - begin);
                        pattern = @"<img.*src=\""(?<Object>.*?)\"".*/>";
                        picName = WebHandler.GetRegexText(subPicHtml, pattern);
                    }
                    //提取价格
                    if(sourceWebID!="")
                    {
                        string priceUrl = @"http://p.3.cn/price/get?skuid=J_" + sourceWebID + "&type=1";
                        string priceJson = WebHandler.GetHtmlStrz(priceUrl, "Default");
                        pattern = @"\""p\"":\""(?<Object>\d+(\.\d{1,2})?)\""";
                        price = WebHandler.GetValidPrice(WebHandler.GetRegexText(priceJson, pattern));
                    }
                    Console.WriteLine("商品名称：{0}",title);
                    Console.WriteLine("图片：{0}",picName);
                    Console.WriteLine("价格：{0}", price);
                }
            }
            ///<summary>
            ///公共方法类
            ///</summary>
            public class WebHandler
            {
                ///<summary>
                ///获取网页的HTML码
                ///</summary>
                ///<param name="url">链接地址</param>
                ///<param name="encoding">编码类型</param>
                ///<returns></returns>
                public static string GetHtmlStr(string url,string encoding)
                {
                    string htmlStr=""
                    try
                    {


                    }
                }
                
            }
        }
    }
}
