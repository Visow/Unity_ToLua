﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

using LibServer.Utility;

namespace LibServer
{
    public class ClientSession : AppSession<ClientSession, BinaryRequestInfo>
    {
        public int roomId = -1; //房间ID，或者是地图ID
        public long uid = 0;    //用户ID

        protected override void OnSessionStarted()
        {
            uid = 0;
            roomId = -1;
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            uid = 0;
            roomId = -1;
            UserUtil.Quit(uid);
            base.OnSessionClosed(reason);
        }

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        protected override void HandleUnknownRequest(BinaryRequestInfo requestInfo)
        {
            this.Send("Unknow request");
        }
    }
}
