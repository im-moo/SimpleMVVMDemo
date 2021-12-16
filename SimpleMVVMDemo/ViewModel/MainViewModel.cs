using SimpleMVVM;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMVVMDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _time = "00:00:00.0000000";
        private bool _clockIsRunning;

        public string Time
        {
            get
            {
                return _time;
            }
            set => SetProperty(ref _time, value);
        }

        public async Task StartClock()
        {
            ClockIsRunning = true;

            await Task.Run(async () =>
             {
                 while (ClockIsRunning)
                 {
                     await Task.Delay(1);

                     if (ClockIsRunning)
                     {
                         Time = DateTime.Now.ToString("HH:mm:ss.fffffff");
                     }
                 }
             });
        }

        public void StopClock()
        {
            ClockIsRunning = false;
        }

        private SimpleCommand? start;
        public ICommand Start => start ??= new SimpleCommand(async () => { await PerformStart(); });

        private async Task PerformStart()
        {
            await StartClock();
        }

        private SimpleCommand? stop;
        public ICommand Stop => stop ??= new SimpleCommand(() => PerformStop(new object()));

        public bool ClockIsRunning
        {
            get => _clockIsRunning;
            set
            {
                _clockIsRunning = value;
                OnPropertyChanged(nameof(ClockIsRunning));
            }
        }

        private void PerformStop(object obj)
        {
            StopClock();
        }

        private string nowTime = DateTime.Now.ToLongTimeString();

        public string NowTime
        {
            get
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        NowTime ="当前时间是："+ DateTime.Now.ToLongTimeString();
                        await Task.Delay(1000);
                    }
                });
                return nowTime;
            }
            set => SetProperty(ref nowTime, value);
        }
    }
}