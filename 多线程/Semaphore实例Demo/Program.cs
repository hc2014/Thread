using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore实例Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            mythread mythrd1 = new mythread("Thrd #1");

            mythread mythrd2 = new mythread("Thrd #2");

            mythread mythrd3 = new mythread("Thrd #3");

            mythread mythrd4 = new mythread("Thrd #4");

            mythrd1.thrd.Join();

            mythrd2.thrd.Join();

            mythrd3.thrd.Join();

            mythrd4.thrd.Join();
        }
    }


    /// <summary>
    /// 自定义类
    /// </summary>
    class mythread
    {

        public Thread thrd;

        //创建一个可授权2个许可证的信号量，且初始值为2

        static Semaphore sem = new Semaphore(2, 2);



        public mythread(string name)
        {

            thrd = new Thread(this.run);

            thrd.Name = name;

            thrd.Start();

        }

        void run()
        {

            Console.WriteLine(thrd.Name + "正在等待一个许可证……");

            //申请一个许可证

            sem.WaitOne();

            Console.WriteLine(thrd.Name + "申请到许可证……");

            for (int i = 0; i < 4; i++)
            {

                Console.WriteLine(thrd.Name + "： " + i);

                Thread.Sleep(1000);

            }

            Console.WriteLine(thrd.Name + " 释放许可证……");

            //释放

            sem.Release();

        }
    }
}
