using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbstract.Abstract
{
    public interface IExtend
    {
        void Music();//默认public  不能带访问修饰符

        string Remark { get; set; }
        //string Description;
        //delegate void DoNthing();
        event Action DoEvent;
    }

    public interface IExtendGame
    {
        void Game();
    }
}
