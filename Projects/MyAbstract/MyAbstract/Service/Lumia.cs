using MyAbstract.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbstract.Service
{
    /// <summary>
    /// 封装
    /// </summary>
    public class Lumia : AbstractPhone//, IExtend
    {
        public void Music()
        {
            Console.WriteLine("Use {0} Music", this.GetType().Name);
        }
        public override void System()
        {
            Console.WriteLine("Android");
        }

        //public int Id { get; set; }
        //public string Branch { get; set; }

        //public void System()
        //{
        //    Console.WriteLine("Android");
        //}

        //public void Call()
        //{
        //    Console.WriteLine("Use {0} Call", this.GetType().Name);
        //}

        //public void Text()
        //{
        //    Console.WriteLine("Use {0} Text", this.GetType().Name);
        //}
    }
}
