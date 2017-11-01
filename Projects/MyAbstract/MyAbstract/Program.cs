using MyAbstract.Abstract;
using MyAbstract.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbstract
{
    /// <summary>
    /// 1 抽象类&&接口，是什么
    /// 2 依赖抽象，为什么用
    /// 3 二者的区别与选择,怎么用
    /// 
    /// 抽象类：是个类，加上约束，还可以包含已实现的东西
    ///   接口：东西少，纯粹的约束，不能有任何已实现的东西
    ///   
    /// 抽象类==类+约束   接口==约束    抽象类  强大
    ///   
    /// 单继承   多实现                接口    灵活
    /// 
    /// 抽象类：适用于包含具体实现的，为了代码重用，同时也有约束的
    ///        is a  手机必备的东西，才放在这里的
    /// 接口： can do  可以横跨任何产品
    /// 
    /// 最佳实践：
    ///    接口优先，除非代码重用，才用抽象类
    ///    
    /// 
    /// 门：开门  关门   材质   警报  猫眼。。。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班公开课之核心语法特训，今天是Eelven老师为大家带来的接口抽象类");
                {
                    iPhone phone = new iPhone();
                    phone.System();
                    phone.Call();
                    phone.Text();
                    phone.Music();
                }
                {
                    AbstractPhone phone = new iPhone();
                    phone.System();
                    phone.Call();
                    phone.Text();
                    //phone.Music();
                }
                {
                    AbstractPhone phone = new Lumia();
                    phone.System();
                    phone.Call();
                    phone.Text();
                }
                {
                    AbstractPhone phone = new P10();
                    phone.System();
                    phone.Call();
                    phone.Text();
                }
                {
                    IExtend phone = new iPhone();
                    //phone.System();
                    //phone.Call();
                    //phone.Text();
                    phone.Music();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
