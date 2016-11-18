using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程池同步操作Demo
{
    class Program
    {
        static private ManualResetEvent finish = new ManualResetEvent(false); 

        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始执行...");

            //开启子线程
            new Thread(() => {
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine("子线程正在执行第{0}阶段的任务",i);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                Console.WriteLine("子线程完成了所有的任务");
                finish.Set();
                Console.WriteLine("子线程告诉主线程已经完成了工作");

            }).Start();

            //主线程等待子线程执行完成
            finish.WaitOne();

            Console.WriteLine("主线程执行完毕!");
            Console.ReadKey();
        }

    }
}
