using System;
using Model;
using Controller;
using System.Threading;

namespace Console_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            Console.WriteLine(Data.CurrentRace.Track.name);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
