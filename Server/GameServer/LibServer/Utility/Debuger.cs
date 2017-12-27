using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServer.Utility
{
    public static class Debuger
    {
        public static void Log(string info)
        { 
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(info);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(e.ToString());
            Console.ForegroundColor = ConsoleColor.White;    
        }

        public static void Error(string info)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(info);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.ToString());
            Console.ForegroundColor = ConsoleColor.White;        
        }

        public static void Wran(string info)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(info);
            Console.ForegroundColor = ConsoleColor.White;      
        }

        public static void Warn(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(e.ToString());
            Console.ForegroundColor = ConsoleColor.White;          
        }
    }
}
