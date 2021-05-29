using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1._Chronometer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string command;

            var chronometer = new Chronometer();

            while (true)
            {
                command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                }

                switch (command)
                {
                    case "start":
                        new Task(() => chronometer.Start()).Start();

                        //Thread t = new Thread(new ThreadStart(chronometer.Start));
                        //t.Start();

                        break;

                    case "stop":
                        chronometer.Stop();
                        break;

                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;

                    case "laps":

                        var sb = new StringBuilder();

                        if (chronometer.Laps.Count == 0)
                        {
                            sb.AppendLine("Laps: no laps");
                        }
                        else
                        {
                            for (int i = 0; i < chronometer.Laps.Count; i++)
                            {
                                sb.AppendLine($"{i}. {chronometer.Laps[i]}");
                            }
                        }

                        Console.WriteLine(sb.ToString());

                        break;

                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;

                    case "reset":
                        chronometer.Reset();
                        break;
                }

            }
        }
    }
}

//start – starts counting time in milliseconds, seconds and minutes.
//stop – stops the process of counting time, but the counted time remains.
//lap – creates a lap at the current time.
//laps – returns all of the currently recorded laps.
//time – returns the currently recorded time.
//reset – stops the Chronometer, resets the currently recorded time and deletes all of the currently recoded laps.
//exit – stops and exits the program.

