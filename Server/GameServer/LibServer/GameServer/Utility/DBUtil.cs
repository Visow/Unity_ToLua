using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibServer.DBBase;
namespace LibServer.GameServer.Utility
{
    public class DBUtil
    {
        DBThread _DBHandler = null;
        private static DBUtil instance = null;
        public static DBUtil Instance {
            get {
                if(instance == null)
                    instance = new DBUtil();
                return instance;
            }
        }

        private DBUtil()
        {
            if (_DBHandler == null)
            {
                _DBHandler = new DBThread();
            }
        }

        public void RequestDB(Message.IMessage msg, LibServer.Common.CustomArgs args)
        {
            if (_DBHandler == null) return;
            _DBHandler.addTask(msg.OnDBTask, args);
        }
    }
}
