using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    /// <summary>
    /// 人类
    /// </summary>
    class Professor
    {
        //访问修饰符public(公共的)private(私有的默认)
        public string Name
        {
            get { return _name; }//读访问器
            set { _name = value; }//写访问器
        }

        ///<summary>
        ///性别
        ///</summary>
        public string Sex
        {
            get { return sex; }
        }
        ///<summary>
        ///年龄
        ///</summary>
        public int Age
        {
            get { return age; }
            set
            {
                if(value>=3 && value <=7)
                    age=value;
            }
        }
        /// <summary>
        /// 体重
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        //封装的快捷键:Ctrl+R+E
        private string _name = "牛掰";//姓名
        private string sex = "男";//性别
        private int age=30;//年龄
        private int height=180;//身高
        private int weight = 70;//体重

        ///<summary>
        ///属性输出
        ///</summary>
        public void Show()//方法声明
        {
            //方法体 方法的实现
            Console.WriteLine("姓名：" + _name);
            Console.WriteLine("性别：" + sex);
            Console.WriteLine("年龄："+age);
            Console.WriteLine("身高："+height+"cm");
            Console.WriteLine("体重："+weight+"Kg");
            Console.WriteLine("我是清华大学自动化学院一名教授！");
        }
    }
}

