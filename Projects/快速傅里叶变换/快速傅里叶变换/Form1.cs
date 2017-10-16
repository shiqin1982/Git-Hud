using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 快速傅里叶变换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>  
        /// 一维频率抽取基2快速傅里叶变换  
        /// 频率抽取：输入为自然顺序，输出为码位倒置顺序  
        /// 基2：待变换的序列长度必须为2的整数次幂  
        /// </summary>  
        /// <param name="sourceData">待变换的序列(复数数组)</param>  
        /// <param name="countN">序列长度,可以指定[0,sourceData.Length-1]区间内的任意数值</param>  
        /// <returns>返回变换后的序列（复数数组）</returns>  
        private Complex[] fft_frequency(Complex[] sourceData, int countN)
        {
            //2的r次幂为N，求出r.r能代表fft算法的迭代次数  
            int r = Convert.ToInt32(Math.Log(countN, 2));
            //分别存储蝶形运算过程中左右两列的结果  
            Complex[] interVar1 = new Complex[countN];
            Complex[] interVar2 = new Complex[countN];
            interVar1 = (Complex[])sourceData.Clone();

            //w代表旋转因子  
            Complex[] w = new Complex[countN / 2];
            //为旋转因子赋值。（在蝶形运算中使用的旋转因子是已经确定的，提前求出以便调用）  
            //旋转因子公式 \  /\  /k __  
            //              \/  \/N  --  exp(-j*2πk/N)  
            //这里还用到了欧拉公式  
            for (int i = 0; i < countN / 2; i++)
            {
                double angle = -i * Math.PI * 2 / countN;
                w[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            //蝶形运算  
            for (int i = 0; i < r; i++)
            {
                //i代表当前的迭代次数，r代表总共的迭代次数.  
                //i记录着迭代的重要信息.通过i可以算出当前迭代共有几个分组，每个分组的长度  

                //interval记录当前有几个组  
                // <<是左移操作符，左移一位相当于*2  
                //多使用位运算符可以人为提高算法速率^_^  
                int interval = 1 << i;

                //halfN记录当前循环每个组的长度N  
                int halfN = 1 << (r - i);

                //循环，依次对每个组进行蝶形运算  
                for (int j = 0; j < interval; j++)
                {
                    //j代表第j个组  

                    //gap=j*每组长度，代表着当前第j组的首元素的下标索引  
                    int gap = j * halfN;

                    //进行蝶形运算  
                    for (int k = 0; k < halfN / 2; k++)
                    {
                        interVar2[k + gap] = interVar1[k + gap] + interVar1[k + gap + halfN / 2];
                        interVar2[k + halfN / 2 + gap] = (interVar1[k + gap] - interVar1[k + gap + halfN / 2]) * w[k * interval];
                    }
                }

                //将结果拷贝到输入端，为下次迭代做好准备  
                interVar1 = (Complex[])interVar2.Clone();
            }

            //将输出码位倒置  
            for (uint j = 0; j < countN; j++)
            {
                //j代表自然顺序的数组元素的下标索引  

                //用rev记录j码位倒置后的结果  
                uint rev = 0;
                //num作为中间变量  
                uint num = j;

                //码位倒置（通过将j的最右端一位最先放入rev右端，然后左移，然后将j的次右端一位放入rev右端，然后左移...）  
                //由于2的r次幂=N，所以任何j可由r位二进制数组表示，循环r次即可  
                for (int i = 0; i < r; i++)
                {
                    rev <<= 1;
                    rev |= num & 1;
                    num >>= 1;
                }
                interVar2[rev] = interVar1[j];
            }
            return interVar2;

        }
        /// <summary>  
        /// 一维频率抽取基2快速傅里叶逆变换  
        /// </summary>  
        /// <param name="sourceData">待反变换的序列（复数数组）</param>  
        /// <param name="countN">序列长度,可以指定[0,sourceData.Length-1]区间内的任意数值</param>  
        /// <returns>返回逆变换后的序列（复数数组）</returns>  
        private Complex[] ifft_frequency(Complex[] sourceData, int countN)
        {
            //将待逆变换序列取共轭，再调用正变换得到结果，对结果统一再除以变换序列的长度N  

            for (int i = 0; i < countN; i++)
            {
                sourceData[i] = sourceData[i].Conjugate();
            }

            Complex[] interVar = new Complex[countN];

            interVar = fft_frequency(sourceData, countN);

            for (int i = 0; i < countN; i++)
            {
                interVar[i] = new Complex(interVar[i].Real / countN, -interVar[i].Imaginary / countN);
            }

            return interVar;
        }
        /// <summary>  
        /// 对给定的序列进行指定长度的离散傅里叶变换DFT  
        /// 内部将使用快速傅里叶变换FFT  
        /// </summary>  
        /// <param name="sourceData">待变换的序列</param>  
        /// <param name="countN">变换的长度N</param>  
        /// <returns>返回变换后的结果（复数数组）</returns>  
        public Complex[] DFT(Complex[] sourceData, int countN)
        {
            if (countN > sourceData.Length || countN < 0)
                throw new Exception("指定的傅立叶变换长度越界！");

            //求出r,2的r次幂为N  
            double dr = Math.Log(countN, 2);
            int r = Convert.ToInt32(dr);//获取整数部分  

            //初始化存储变换结果的数组  
            Complex[] result = new Complex[countN];

            //判断选择合适的算法进行快速傅里叶变换FFT  
            if ((dr - r) != 0)
            {
                //待变换序列长度不是基2的  
            }
            else
            {
                //待变换序列长度是基2的  
                //使用一维频率抽取基2快速傅里叶变换  
                result = fft_frequency(sourceData, countN);
            }

            return result;

        }  
    }
}
