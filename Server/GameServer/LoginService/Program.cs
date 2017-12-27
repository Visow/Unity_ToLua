using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginService curService = new LoginService(6689);
            curService.Start();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("--------------------Loginservice----------------------------");
            Console.WriteLine("--------------------Listening Port: 6689--------------------");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer  
            curService.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }
    }
}
