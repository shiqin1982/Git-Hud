using MyAbstract.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbstract.Service
{
    public class iPad : AbstractPhone, IExtend, IExtendGame
    {
        public void Music()
        {
            Console.WriteLine("Use {0} Music", this.GetType().Name);
        }

        public override void System()
        {
            Console.WriteLine("IOS");
        }

        public string Remark
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event Action DoEvent;



        //public int Id { get; set; }
        //public string Branch { get; set; }

        //public void System()
        //{
        //    Console.WriteLine("IOS");
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
