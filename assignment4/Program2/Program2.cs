using System;

namespace assignment4
{
    // 闹钟类
    public class AlarmClock
    {
        // 定义Tick和Alarm事件
        public event EventHandler Tick;
        public event EventHandler<DateTime> Alarm;

        private DateTime alarmTime;
        private bool isRunning = false;

        public AlarmClock(DateTime alarmTime)
        {
            this.alarmTime = alarmTime;
        }

        // 开始闹钟
        public void Start()
        {
            isRunning = true;
            while (isRunning)
            {
                System.Threading.Thread.Sleep(1000); // 等待一秒

                OnTick();

                if (DateTime.Now >= alarmTime && !isRunning) break;

                if (DateTime.Now >= alarmTime)
                {
                    OnAlarm(DateTime.Now);
                    break; // 停止闹钟
                }
            }
        }

        // 触发Tick事件
        protected virtual void OnTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        // 触发Alarm事件
        protected virtual void OnAlarm(DateTime time)
        {
            Alarm?.Invoke(this, time);
            isRunning = false; // 停止闹钟
        }

        // 设置闹钟停止
        public void Stop()
        {
            isRunning = false;
        }
    }

    class Program2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入闹钟时间（格式：HH:mm:ss）:");
            var timeStr = Console.ReadLine();
            var alarmTime = DateTime.Today.Add(TimeSpan.Parse(timeStr));

            var alarmClock = new AlarmClock(alarmTime);

            // 注册Tick事件处理函数
            alarmClock.Tick += (sender, e) =>
            {
                Console.WriteLine($"Tick: {DateTime.Now}");
            };

            // 注册Alarm事件处理函数
            alarmClock.Alarm += (sender, time) =>
            {
                Console.WriteLine($"Alarm! 时间是: {time}");
            };

            Console.WriteLine($"闹钟设置在 {alarmTime.ToShortTimeString()}");

            alarmClock.Start();
        }
    }
}