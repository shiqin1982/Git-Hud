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
    /// 学生
    /// 依赖抽象，而不是依赖细节
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long QQ { get; set; }

        public void Study()
        {
            Console.WriteLine("{0} 跟着Eelven老师学习.net高级开发");
        }

        public void PlayIPhone(AbstractPhone phone)
        {
            Console.WriteLine("This is {0}", this.Name);
            Console.WriteLine("{0} {1}", phone.Id, phone.Branch);
            phone.System();
        }

        /// <summary>
        /// 面向细节
        /// </summary>
        /// <param name="phone"></param>
        public void PlayIPhone(iPhone phone)
        {
            Console.WriteLine("This is {0}", this.Name);
            Console.WriteLine("{0} {1}", phone.Id, phone.Branch);
            phone.System();
        }

        public void PlayP10(P10 phone)
        {
            Console.WriteLine("This is {0}", this.Name);
            Console.WriteLine("{0} {1}", phone.Id, phone.Branch);
            phone.System();
        }
        public void PlayLumia(Lumia phone)
        {
            Console.WriteLine("This is {0}", this.Name);
            Console.WriteLine("{0} {1}", phone.Id, phone.Branch);
            phone.System();
        }

    }
}
