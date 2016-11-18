using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程等待实例二
{
    class Program
    {
        static AutoResetEvent[] resets = new AutoResetEvent[2];
        static void Main(string[] args)
        {

            for (int i = 0; i < resets.Length; i++)
            {
                resets[i] = new AutoResetEvent(false);
            }

            Console.WriteLine("主线程开始工作...");
            Thread t = new Thread(work1);
            t.Start();

            Thread t2 = new Thread(new ParameterizedThreadStart(work2));
            t2.Start("work2正在工作...");


            WaitHandle.WaitAll(resets);
            Console.WriteLine("所有子线程工作完成...");
            Console.ReadKey();
        }

        static void work1()
        {
            Console.WriteLine("work1正在工作...");
            Thread.Sleep(1000);
            Console.WriteLine("work1 工作了1000毫秒");
            Console.WriteLine("work1 工作了完成");

            resets[0].Set();
        }

        static void work2(object msg)
        {
            Console.WriteLine(msg);
            Thread.Sleep(2000);
            Console.WriteLine("work2 工作了2000毫秒");
            Console.WriteLine("work2 工作了完成");

            resets[1].Set();
        }

    }
}
