using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程池Demo
{
    class Program
    {
        //使用线程池有如下优点：
        //1、缩短应用程序的响应时间。因为在线程池中有线程的线程处于等待分配任务状态（只要没有超过线程池的最大上限），无需创建线程。
        //2、不必管理和维护生存周期短暂的线程，不用在创建时为其分配资源，在其执行完任务之后释放资源。
        //3、线程池会根据当前系统特点对池内的线程进行优化处理。

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Fun));
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.ReadKey();
        }

        static void Fun(object obj)
        {
            Console.WriteLine("当前执行的线程是:{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }


  
}
