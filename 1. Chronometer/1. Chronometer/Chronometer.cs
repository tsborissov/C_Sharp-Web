using _1._Chronometer.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;

namespace _1._Chronometer
{
    public class Chronometer : IChronometer
    {
        private DateTime recordedTime = new DateTime();
        private bool stopCounting;
        private string timeFormat = "mm:ss.ffff";

        public Chronometer()
        {
            this.Laps = new List<string>();
        }

        public string GetTime => recordedTime.ToString(timeFormat);

        public List<string> Laps { get; }

        public string Lap()
        {
            this.Laps.Add(this.GetTime);

            return this.GetTime;
        }

        public void Reset()
        {
            this.stopCounting = true;
            this.recordedTime = DateTime.MinValue;
            this.Laps.Clear();

        }

        public void Start()
        {
            stopCounting = false;

            while (true)
            {
                Thread.Sleep(1);
                this.recordedTime = this.recordedTime.AddMilliseconds(1);

                if (stopCounting)
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            stopCounting = true;
        }
    }
}
