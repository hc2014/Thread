using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlim实例2Demo
{
    class Program
    {

        //SemaphoreSlim 跟Semaphore 不同,msdn的解释是：对可同时访问资源或资源池的线程数加以限制的 Semaphore 的轻量替代。
        static SemaphoreSlim ssm = new SemaphoreSlim(4,4);

        static void Main(string[] args)
        {
            for (int i = 0; i < 6; i++)
            {
                string threadname = "Thread"+i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDataBase(threadname, secondsToWait));
                t.Start();
            }
            Console.WriteLine("主线程执行完毕!");
        }

        static void AccessDataBase(string name, int seconds)
        {
            Console.WriteLine("{0}等待授权方法DataBase",name);
            ssm.Wait();
            Console.WriteLine("{0}开始访问数据库",name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0}访问数据库完成,释放资源", name);
            ssm.Release();
        }
    }
}
