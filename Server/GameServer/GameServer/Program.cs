using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var appServer = new AppServer();

            //Setup the appServer  
            if (!appServer.Setup(2012)) //Setup with listening port  
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            appServer.NewSessionConnected += appServer_NewSessionConnected;
            appServer.SessionClosed += appServer_SessionClosed;
            appServer.NewRequestReceived += appServer_NewRequestReceived;  

            //Try to start the appServer  
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer  
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        static void appServer_NewRequestReceived(AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            Console.WriteLine("appServer_NewRequestReceived" + session.SessionID);
        }


        static void appServer_SessionClosed(AppSession session, CloseReason value)
        {
            Console.WriteLine("appServer_SessionClosed" + session.SessionID);
        }


        static void appServer_NewSessionConnected(AppSession session)
        {
            Console.WriteLine("appServer_NewSessionConnected" + session.SessionID);
        }  
    }
}
