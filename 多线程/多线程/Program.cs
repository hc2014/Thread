using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace 多线程
{
    class Program
    {

        //可以认为AutoResetEvent就是一个公共的变量（尽管它是一个事件），创建的时候可以设置为false，
        //然后在要等待的线程使用它的WaitOne方法，那么线程就一直会处于等待状态，
        //只有这个AutoResetEvent被别的线程使用了Set方法，也就是要发通知的线程使用了它的Set方法，
        //那么等待的线程就会往下执行了，Set就是发信号，WaitOne是等待信号，只有发了信号，
        //等待的才会执行。如果不发的话，WaitOne后面的程序就永远不会执行

        static AutoResetEvent mEvent = new AutoResetEvent(false);

        static void DoWorkThread()
        {
            Console.WriteLine(DateTime.Now + "  :工作线程开始工作,并且等待主线程完成的标志...");
            mEvent.WaitOne();
            Console.WriteLine(DateTime.Now + "  :收到主线程工作完成的标志,工作线程退出工作...");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始启动工作线程...");
            Thread t = new Thread(DoWorkThread); 
            t.Start();  
  
            Console.WriteLine("主线程睡眠5秒...");  
            Thread.Sleep(5000);  
  
            Console.WriteLine("主线程工作完成,开始设置修改子线程的等待标志...");  
            mEvent.Set();
            Console.ReadKey();
        }
    }
}
