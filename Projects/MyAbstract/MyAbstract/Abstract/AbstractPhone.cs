using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbstract.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractPhone
    {
        public int Id { get; set; }
        public string Branch { get; set; }

        /// <summary>
        /// 手机都有操作系统，  父类定义方法
        /// 但是又各不相同     父类并不实现，由不同的子类自行实现
        /// </summary>
        public abstract void System();

        public void Call()
        {
            Console.WriteLine("Use {0} Call", this.GetType().Name);
        }

        public void Text()
        {
            Console.WriteLine("Use {0} Text", this.GetType().Name);
        }
        //void Music();

        //string Remark { get; set; }
        //string Description;
        //delegate void DoNthing();
        //event Action DoEvent;
    }
}
