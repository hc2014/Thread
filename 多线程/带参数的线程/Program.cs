using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 带参数的线程
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = "工作好累啊...";

            //实现方式一
            Thread t = new Thread(new ParameterizedThreadStart(DoWorlThread));
            t.Start(msg);

            //实现方式二
            Thread t2 = new Thread(new ThreadStart(delegate {
                DoWorlThread(msg);
            }));
            t2.Start();

        }

        static void DoWorlThread(object msg)
        {
            Console.WriteLine(msg);
        }
    }
}
