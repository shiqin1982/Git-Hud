using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{

    class Program
    {
        static void Main(string[] args)
        {
            /*Child xiaoMing = new Child();//实例化了Child类的对象
            xiaoMing.Name = "马小明";//为字段赋值
            //xiaoMing.Sex = "男";
            xiaoMing.Age = 6;
            xiaoMing.Age = 7;
            //xiaoMing.Height = 120;
            Console.WriteLine("我今年" + xiaoMing.Age + "岁。");
            //Console.WriteLine("我叫" + xiaoMing.Name + ",今年" + xiaoMing.Age + "岁。");
            //xiaoMing.PlayBall();//调用踢球方法*/
            Child child = new Child();//声明和实例化
            child.PlayBall();//调用方法
            child.EatSugar("榴莲糖");//实参 sugar="榴莲糖"
            child.EatSugar("牛奶糖");
            child.EatSugar(4);
            child.EatSugar("水果糖", 5);
        }
    }
}
