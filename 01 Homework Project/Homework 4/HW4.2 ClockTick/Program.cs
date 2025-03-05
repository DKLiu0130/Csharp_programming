using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HW4._2_ClockTick
{

    public class Clock
    {
        // 定义嘀嗒（Tick）事件
        public event EventHandler Tick;

        // 定义响铃（Alarm）事件
        public event EventHandler Alarm;

        private int alarmHour;
        private int alarmMinute;
        private bool alarmSet = false;

        // 设置闹钟时间
        public void SetAlarm(int hour, int minute)
        {
            alarmHour = hour;
            alarmMinute = minute;
            alarmSet = true;
            Console.WriteLine($"闹钟已设置：{hour:D2}:{minute:D2}");
        }

        // 启动闹钟
        public void Start()
        {
            while (true)
            {
                DateTime now = DateTime.Now;

                // 触发Tick事件
                Tick?.Invoke(this, EventArgs.Empty);
                Console.WriteLine($"嘀嗒... 当前时间: {now:HH:mm:ss}");

                // 检查是否到达闹钟时间
                if (alarmSet && now.Hour == alarmHour && now.Minute == alarmMinute)
                {
                    // 触发Alarm事件
                    Alarm?.Invoke(this, EventArgs.Empty);
                    Console.WriteLine("响铃啦！时间到了！");
                    alarmSet = false; // 解除闹钟，防止重复触发
                }

                // 每秒执行一次
                Thread.Sleep(1000);
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            Clock clock = new Clock();

            // 订阅 Tick 事件
            clock.Tick += (sender, e) =>
            {
                Console.WriteLine("滴答滴答...");
            };

            // 订阅 Alarm 事件
            clock.Alarm += (sender, e) =>
            {
                Console.WriteLine("铃铃铃！闹钟响了！");
            };

            // 设置闹钟（假设设置为当前分钟的下一分钟）
            DateTime now = DateTime.Now;
            clock.SetAlarm(now.Hour, (now.Minute + 1) % 60);
           
            // 启动闹钟
            clock.Start();
        }
    }

}
