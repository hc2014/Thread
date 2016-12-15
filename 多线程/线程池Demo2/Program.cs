using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程池Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin in Main");

            ThreadPool.QueueUserWorkItem(ThreadInvoke);

            //将当前线程挂起200毫秒
            Thread.Sleep(200);
            Console.WriteLine("End in Main");

            //这里休眠是模拟主线程(前台线程)的执行时间要比线程池(后台线程)的执行时间要长,这样后台线程才能够执行完毕。不然在主线程执行完毕后,线程池会被关闭
            Thread.Sleep(3000);
        }

        static void ThreadInvoke(Object param)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Execute in ThreadInvoke");
                //每隔100毫秒，循环一次
                Thread.Sleep(500);
            }
        }
    }
}
