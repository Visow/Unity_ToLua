using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace LibServer.GlobalServer
{
    public class GameSession : AppSession<GameSession, BinaryRequestInfo>
    {

        protected override void OnSessionStarted()
        {

        }

        protected override void OnSessionClosed(CloseReason reason)
        {
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
