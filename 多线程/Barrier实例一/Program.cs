using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barrier实例一
{
    class Program
    {

        //假设有2个相同的Thread，每个Thread都有N个阶段(Phase)，当他们并发工作时，
        //只有当所有Thread的相同步骤都完成时，所有Thread才可以开始下一个步骤。
        //Barrier 只能在4.0以上的框架才能使用

        static Barrier myBarrier = new Barrier(2, b => Console.WriteLine("完成了第 {0}阶段", b.CurrentPhaseNumber));


        static void PlayMusic(string name, string msg, int seconds)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-".PadRight(50,'-'));
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} 开始唱 {1}",name,msg);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} 唱完了 {1}", name, msg);


                Console.WriteLine("{0} 的 {1} 到达设置屏障处,并且等待其他线程", name, msg);
                //在这里设置一个屏障，等待其他所有的参与者(线程)到达(执行完成)这里后,才可以继续下一步操作
                myBarrier.SignalAndWait();
            }
        }

        static void Main(string[] args)
        {
            Thread t = new Thread(()=>PlayMusic("周杰伦","七里香",2));
            t.Start();
           Thread t2 = new Thread(()=>PlayMusic("周杰伦","夜曲",4));
            t2.Start();
            Console.ReadKey();
        }
    }
}
