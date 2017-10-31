using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 公式解析
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Expression
    {
        private StackSimple<int> theStack;
        public StringBuilder suffix = new StringBuilder();
        /*
         * 检查该字符是否为括号
        */
        private bool isParen(char c)
        {
            if (c == '{' || c == '[' || c == '(' || c == ')' || c == ']' || c == '}')
                return true;
            else
                return false;
        }
        /*
         * 检查是否为左括号
        */
        private bool isLeftParen(char c)
        {
            if (c == '{' || c == '[' || c == '(')
                return true;
            else
                return false;
        }
        /*
         * 判断制定的操作符是否为‘+’或者‘-’
         */
        private bool isAddOrSub(int oper)
        {
            return oper == '+' || oper == '-';
        }
        /*
         * 检查是否为数字
        */
        private bool isNumber(int ch)
        {
            return ch >= 48 && ch < 57;
        }
        /*
         * 检查表达式是否合法
        */
        private bool checkExp(String exp)
        {
            char[] ch = exp.ToCharArray();
            StackSimple<int> theStackXInt = new StackSimple<int>(exp.Length / 2);
            /*
             * 遍历表达式，只处理括号部分
            */
            for (int i = 0; i < ch.Length; i++)
            {
                char c = ch[i];
                if (isParen(c))
                {
                    /*
                     * 左括号进栈
                    */
                    if (isLeftParen(c))
                    {
                        theStackXInt.Push(c);
                    }
                    else
                    {
                        if (theStackXInt.IsEmpty())
                        {
                            return false;
                        }
                        int left = theStackXInt.Pop();
                        switch (c)
                        {
                            case '}':
                                if (left != '{')
                                    return false;
                                break;
                            case ']':
                                if (left != '[')
                                    return false;
                                break;
                            case ')':
                                if (left != '(')
                                    return false;
                                break;
                        }
                    }
                }
            }
            if (theStackXInt.IsEmpty())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /*
         * 根据当前操作符处理之前在栈中的操作符，当前操作符暂不处理，放入栈中
        */
        private void processOper(char oper)
        {
            while (!theStack.IsEmpty())
            {
                int currTop = theStack.Pop();
                /*
                 * 如果是左括号，则不予处理,将括号返回进栈
                 * 
                 * 若得到的操作符，则进一步判断
                */
                if (currTop == '(')
                {
                    theStack.Push(currTop);
                    break;
                }
                else
                {
                    if (this.isAddOrSub(currTop))
                    {
                        if (this.isAddOrSub(oper))
                        {
                            this.suffix.Append((char)currTop);
                        }
                        else
                        {
                            this.theStack.Push(currTop);
                            break;
                        }
                    }
                    else
                    {
                        this.suffix.Append((char)currTop);
                        break;
                    }
                }
            }
            /*
             * 当前操作符进栈
            */
            theStack.Push(oper);
        }
        /*
         * 处理括弧
         * 
         * 如果为左括弧，直接进栈
         * 
         * 否则为右括弧,先将这一对括号中的操作符优先处理完
        */
        private void processParen(char paren)
        {
            if (this.isLeftParen(paren))
            {
                this.theStack.Push(paren);
            }
            else
            {
                while (!theStack.IsEmpty())
                {
                    int chx = theStack.Pop();
                    if (chx == '(')
                        break;
                    else
                        suffix.Append((char)chx);
                }
            }
        }
        /*
         * 将正确的的中缀表达式转为后缀表达式
        */
        private void doTrans(String exp)
        {
            theStack = new StackSimple<int>(exp.Length);
            if (this.checkExp(exp))
            {
                char[] ch = exp.ToCharArray();
                for (int i = 0; i < ch.Length; i++)
                {
                    char c = ch[i];
                    switch (c)
                    {
                        case '+':
                        case '-':
                        case '*':
                        case '/':
                            this.processOper(c);
                            break;
                        case '(':
                        case ')':
                            this.processParen(c);
                            break;
                        default:
                            this.suffix.Append(c);
                            break;

                    }
                }
                while (!this.theStack.IsEmpty())
                {
                    this.suffix.Append((char)this.theStack.Pop());
                }

            }

        }
        /*
         * 计算表达式
        */
        public int clac(String exp)
        {
            this.doTrans(exp);
            String suff = this.suffix.ToString();
            StackSimple<int> stack = new StackSimple<int>(suff.Length);
            if (suff == null || suff.Length == 0)
            {

            }
            else
            {
                char[] ch = suff.ToCharArray();
                for (int i = 0; i < ch.Length; i++)
                {
                    char c = ch[i];
                    if (this.isNumber(c))
                    {
                        stack.Push(c - 48);
                    }
                    else
                    {
                        int operand2 = stack.Pop();
                        int operand1 = stack.Pop();
                        switch (c)
                        {
                            case '+':
                                stack.Push(operand1 + operand2);
                                break;
                            case '-':
                                stack.Push(operand1 - operand2);
                                break;
                            case '*':
                                stack.Push(operand1 * operand2);
                                break;
                            case '/':
                                stack.Push(operand1 / operand2);
                                break;
                        }
                    }
                }
            }

            return stack.Peep();
        }
    }
    ///<summary>
    ///用于演示栈(LIFO)的内部实现机制
    ///
    ///xfeng/2010-05-07
    ///</summary>
    public class StackSimple<T>
    {
        //栈默认的处世容量大小
        private const int DEFAULT_SIZE=10;
        private T[]array;
        private int index;
        ///<summary>
        ///初始化 Stack类的新实例，该实例为空并且具有默认初始容量。
        ///</summary>
        public StackSimple()
            :this(DEFAULT_SIZE)
            {
            }
        ///<summary>
        ///初始化Stack类的新实例，该实例为空并且具有指定的初始容量。
        ///</summary>
        //////<param name="size"></param>
        public StackSimple(int size)
        {
            index=-1;
            this.array=new T[size];
        }
        private int _count;
        ///<summary>
        ///获取Stack中包含的元素数。
        ///</summary>
        public int Count
        {
            get { return _count; }
        }
        ///<summary>
        ///清空栈
        ///</summary>
        public void Clear()
        {
            for (int i=0;i<array.Length;i++)
            {
                array[i]=default(T);
                this.index=-1;
                this._count=0;
            }
        }
        public Boolean IsEmpty()
        {
            return this._count==0;
        }
        ///<summary>
        ///返回位于Stack定部的对象但不将其移除。
        ///</summary>
        ///<returns>位于Stack定部的Object。</returns>
        public T Peep()
        {
            return this.index<0? default(T):this.array[this.index];
        }
        ///<summary>
        ///移除并返回位于Stack顶部的对象。
        ///</summary>
        ///<return>从Stack的顶部移除的Object。</returns>
        public T Pop()
        {
            T obj=this .index<0 ?default(T):this.array[this.index];
            if(null !=obj)
            {
                this.index--;
                this._count--;
                //this.array[this.index]=default(T);
            }
            return obj;
        }
        ///<summary>
        ///将对象插入Stack的顶部。
        ///</summary>
        public void Push(T obj)
        {
            if(null !=obj)
            {
                if(this.index >=this.array.Length-1)
                {
                    T[]temp=new T[this.array.Length*2];
                    int count=0;
                    foreach(T item in array)
                    {
                        temp[count++]=item;
                    }
                    this.array=temp;
                }
                array[++index]=obj;
                this._count++;
            }
        }
    }
}
