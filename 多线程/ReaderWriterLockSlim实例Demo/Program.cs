using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLockSlim实例Demo
{
    class Program
    {

        //一旦该读写锁处在 UpgradeableRead 模式下，线程就能读取某些状态值来决定是否降级到 Read 模式或升级到 Write 模式。
        //注意应当尽可能快的作出这个决定：持有 UpgradeableRead 锁会强制任何新的读请求等待，尽管已存在的读取操作仍然活跃。
        //遗憾的是，CLR 团队移除了 DowngradeToRead 和 UpgradeToWrite 两个方法。如果要降级到读锁，
        //只要简单的在 ExitUpgradeableReadLock 方法后紧跟着调用 EnterReadLock 方法即可：这可以让其他的 Read 和 UpgradeableRead 
        //获得完成先前应当持有却被 UpgradeableRead 锁持有的操作。如果要升级到写锁，只要简单调用 EnterWriteLock 方法即可：这可能要等待，
        //直到不再有任何线程在 Read 模式下持有锁。不像降级到读锁，必须调用 ExitUpgradeableReadLock。
        //在 Write 模式下不必非得调用 ExitUpgradeableReadLock。但是为了形式统一，最好还是调用它

       static ReaderWriterLockSlim rwls = new ReaderWriterLockSlim();
       static Dictionary<int, int> dic = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(() => Write("线程一")) { IsBackground = true }.Start();
            new Thread(() => Write("线程二")) { IsBackground = true }.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(30));
            Console.ReadKey();
        }

        static void Read()
        {
            Console.WriteLine("开始读取字典数据...");
            while (true)
            {
                try
                {
                    //尝试进入读取模式
                    rwls.EnterReadLock();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine("集合中键的个数是{0}", dic.Count);
                }
                finally
                {
                    rwls.ExitReadLock();
                }
            }
        }

        static void Write(string name)
        {
            Console.WriteLine("开始写入字典数据...");
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);


                    //这里先判断是否存在指定的key(读取操作),如果没有就做写入操作。如果用writerlock跟writerlock分别来创建读锁跟写锁的话不仅会浪费资源
                    //而且在创建写锁的同时会阻塞Read()方法来读取数据，这样会浪费大量的时间。所以这个地方用升级的读锁EnterUpgradeableReadLock
                    //EnterUpgradeableReadLock表示,先用读锁获取数据,然后如果需要修改数据,直接用EnterWriteLock，将读锁升级到写锁就可以了
                    rwls.EnterUpgradeableReadLock();
                    if (!dic.ContainsKey(newKey))
                    {
                        try
                        {
                            dic[newKey] = 1;
                            rwls.EnterWriteLock();
                            Console.WriteLine("通过线程{0}向集合中添加了新的键", name, newKey);
                        }
                        finally {
                            rwls.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                finally
                {
                    rwls.ExitUpgradeableReadLock();
                }
                
            }
        }
    }
}
