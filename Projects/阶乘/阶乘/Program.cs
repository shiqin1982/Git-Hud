﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 阶乘
{
    //public class Factorial
    //{
    //    public long factorial(long x)
    //    {
    //        return((x<=1)?1:x*(factorial(x-1)));
    //    }
    //}
    //public class FactorialSum
    //{
    //    public static void Main()
    //    {
    //        long temp=0;
    //        long sum=0;
    //        Console.WriteLine("输入一个数:");
    //        int x=int.Parse(Console.ReadLine());
    //        for(int i=1;i<=x;i++)
    //        {
    //            Factorial f1=new Factorial();
    //            temp +=f1.factorial(i);
    //        }
    //        sum+=temp;
    //        Console.WriteLine("1!+2!+31+...+a!="+sum);
    //    }
    //}
    public class Factorial
    {
        public long factorial(long x)
        {
            return((x <= 1) ? 1 : x * (factorial(x - 1)));
        }
    }
    public class FactorialSum
    {
        public static void Main()
        {
            long temp = 0;
            long sum = 0;
            Console.WriteLine("输入一个数");
            int x = int.Parse(Console.ReadLine());
            for(int i=1;i<=x;i++)
            {
                Factorial f1 = new Factorial();
                temp += f1.factorial(i);
            }
            sum += temp;
            Console.WriteLine("1!+2!+3!+...+a!="+sum);
        }
    }
}
