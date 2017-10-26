using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{
    /// <summary>
    /// 小朋友
    /// </summary>
    class Child
    {
        //访问修饰符public(公共的),private(私有的默认)
        private string _name;//姓名
        public string Name
        {
            get { return _name; }//读访问器
            set { _name = value; }//写访问器
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                return sex;
            }

            //set
            //{
            //    sex = value;
            //}
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                if (value >= 3 && value <= 7)
                    age = value;
            }
        }
        /// <summary>
        /// 身高
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        //封装的快捷键:Ctrl+R+E  
        private string sex = "男";//性别
        private int age;//年龄
        private int height;//身高

        /// <summary>
        /// 踢足球
        /// </summary>
        public void PlayBall()//方法的声明
        {
            //方法体 方法的实现
            Console.WriteLine("耶！我是小小C罗！");
        }
        /// <summary>
        /// 吃糖
        /// </summary>
        /// <param name="sugar">糖的类型(形参)</param>
        public void EatSugar(string sugar)
        {
            if (sugar == "榴莲糖")
                Console.Write("呀！我最怕榴莲的味道了！");
            else
                Console.WriteLine("我最喜欢糖糖了！");
        }
        /// <summary>
        /// 吃糖 同一个类中，多个方法名字相同单参数不同
        /// </summary>
        /// <param name="Count">糖的数量</param>
        public void EatSugar(int Count)
        {
            if (Count > 3)
                Console.WriteLine("吃糖太多对牙齿不好！");
            else
                Console.WriteLine("吃糖糖吧！");
        }
        /// <summary>
        /// 吃糖
        /// </summary>
        /// <param name="sugar">糖的类型</param>
        /// <param name="count">糖的数量</param>
        public void EatSugar(string sugar,int count)
        {
            if (sugar == "牛奶糖" && count > 2)
                Console.WriteLine("牛奶糖不能吃太多哦！");
            else
                Console.WriteLine("吃糖糖吧！");
        }

    }
}
