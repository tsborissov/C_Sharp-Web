using System.Collections.Generic;

namespace _1._Chronometer.Contracts
{
    public interface IChronometer
    {
        string GetTime { get; }

        List<string> Laps { get; }

        void Start();

        void Stop();

        string Lap();

        void Reset();

    }
}
