using System;
using System.Collections.Generic;
using System.Text;
using LibServer.Common;
using LibServer.Message;
using LibServer.Service;
using LibServer.Timer;
namespace LibServer.Utility {
    class ServerUtil {
        static ServerUtil server;
        private HttpServer http;
        private ConfigTimer config;
        public static ServerUtil instance {
            get {
                if (server == null)
                    server = new ServerUtil();
                return server;
            }
        }

        public ServerUtil() { 
        }

        /// <summary>
        /// 服务器初始化
        /// </summary>
        public void Init() {
            config = new ConfigTimer(); config.Start();
            //http = new HttpServer(7077); http.Start();

            Const.users = new Dictionary<long, ClientSession>();
            //var v = RedisUtil.Get("aaa");
            //Console.WriteLine(v);
        }

        /// <summary>
        /// 服务器关闭
        /// </summary>
        public void Close() {
            config.Stop(); config = null;
            //http.Stop(); http = null;
        }
    }
}
