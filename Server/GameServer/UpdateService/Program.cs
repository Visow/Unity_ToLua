using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateService
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer curService = new HttpServer(6688);
            curService.Start();
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
