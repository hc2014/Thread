using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 有返回值的多线程
{
    class Program
    {

       delegate string LongCalculationDelegate(int count);
       delegate int MyDelegate(int data, int ms);

        static void Main(string[] args)
        {
            MyDelegate myFun = TaskWhile;
            IAsyncResult result = myFun.BeginInvoke(1, 3000, null, null);
            while (!result.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(50);
            }
            int i = myFun.EndInvoke(result);
            Console.WriteLine("异步委托返回结果是:{0}", i);



            MyDelegate myFun1 = TaskWhile;
            IAsyncResult result1 = myFun1.BeginInvoke(1, 2000, null, null);
            while (!result1.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(50);
            }
            int j = myFun1.EndInvoke(result1);
            Console.WriteLine("异步委托返回结果是:{0}", j);

        }

        static int TaskWhile(int data, int ms)
        {
            Console.WriteLine("异步委托开始执行！");
            Thread.Sleep(ms);
            Console.WriteLine("异步委托结束执行！");
            return ++data;
        }
    }
}
