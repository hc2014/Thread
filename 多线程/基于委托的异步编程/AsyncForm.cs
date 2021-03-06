﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 基于委托的异步编程
{
    public partial class AsyncForm : Form
    {
        public AsyncForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "正在计算...";
            
            //异步执行计算，但是主线程依旧需要等待执行结果
            //Func<int, int> result = new Func<int, int>(Async);
            //IAsyncResult ires = result.BeginInvoke(10, null, null);
            //label2.Text = Sync(10).ToString();
            //label1.Text = result.EndInvoke(ires).ToString();

            //BeginInvoke 启用了一个线程去执行异步代码,单是这里写了EndInvoke，虽然计算的代码会异步执行，
            //单是对于主线程来说还是要去等待这个异步代码执行完毕,然后给lable赋值.
            //为什么说主线程等待呢?最好的验证方法就是,代码执行后,发现窗体无法拖动.窗体其他控件无法点击
            //如果想不等待执行结果的话(窗体控件可以继续操作) 那可以这样写

            //异步计算，主线程不等待执行结果
            Func<int, int> result = new Func<int, int>(Async2);
            result.BeginInvoke(10, null, null);
            label2.Text = Sync(10).ToString();
            
        }

        private int Sync(int num)
        {
            return num * num;
        }
        private int Async(int num)
        {
            Thread.Sleep(5000);
            return num * num;
        }

        private int Async2(int num)
        {
            Thread.Sleep(5000);
            if (label1.InvokeRequired)
            {
                label1.Invoke(new Action<string>(s => label1.Text = s), (num * num).ToString());
            }
            return num * num;
        }


        private int CallbackTest(int num1, int num2)
        {
            Thread.Sleep(num2);
            return num1 * num1;
        }

        
        //声明委托
        Func<int, int, int> result=null;

        private void button2_Click(object sender, EventArgs e)
        {
            //实例委托
            result = new Func<int, int, int>(CallbackTest);

            //创建10个任务
            for (int i = 1; i < 11; i++)
            {
                result.BeginInvoke(10 * i, 1000 * i, MyCallBack, i);
            }
        }

        private void MyCallBack(IAsyncResult irs)
        {
            int res = result.EndInvoke(irs);
            Console.WriteLine("当前执行第{0}个任务,执行结果是:{1}",irs.AsyncState.ToString(),res);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ThreadTest));
            t.IsBackground = true;
            t.Start();
        }

        void ThreadTest()
        {
            int a = 0;
            for (int i = 0; i < 100; i++)
            {
                a += i;
                if (label1.InvokeRequired)
                {
                    label1.Invoke(new Action<string>(s=>label1.Text=s),a.ToString());

                }
                //如果直接这样写会报错
                //label1.Text=a.ToString();
                Thread.Sleep(100);
            }
        }
    }
}
