using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 并行编程求和Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(i);
            }

            int sum = GetSum(list);
            Console.WriteLine(sum);
            Console.ReadKey();
        }


        //Parallel 类通过局部值（local value）的概念来实现聚合，局部值就是只在并行循环内部存
        //在的变量。这意味着循环体中的代码可以直接访问值，不需要担心同步问题。循环中的代
        //码使用 LocalFinally 委托来对每个局部值进行聚合。需要注意的是， localFinally 委托需
        //要以同步的方式对存放结果的变量进行访问。下面是一个并行求累加和的例子：
        static int GetSum(IEnumerable<int> values)
        {
            int result = 0;
            object mutex = new object();
            Parallel.ForEach(
                source: values,
                localInit: () => 0,
                body: (item, state, localvalue) => localvalue + item,
                localFinally: localvalue =>
                {
                    lock (mutex)
                    {
                        result += localvalue;
                    }
                }
                );
            return result;
        }
    }
}
