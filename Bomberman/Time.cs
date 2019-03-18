using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Time
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public Timer t;
        public Session Caller;

        public Time(int minutes, int seconds, Session obj)
        {
            Minutes = minutes;
            Seconds = seconds;
            Caller = obj;
            t = new Timer(1000);
        }

        public void DisplayTimer(Object sender, ElapsedEventArgs e)
        {
            Caller.LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            if (Minutes < 10)
            {
                Console.SetCursorPosition(Caller.matrix.Columns + 3, 0);
                Console.Write($"Time: 0{Minutes}:");
            }
            else
            {
                Console.SetCursorPosition(Caller.matrix.Columns + 3, 0);
                Console.Write($"Time: {Minutes}:");
            }

            if (Seconds < 10)
                Console.Write($"0{Seconds}");
            else
                Console.Write($"{Seconds}");

            Caller.UnlockConsole();

            if (Seconds == 0)
            {
                if (Minutes == 0)
                    Caller.End();
                else
                {
                    Minutes--;
                    Seconds = 59;
                }
            }
            else
                Seconds--;
        }
    }   
 
}
