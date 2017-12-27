using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

using LibServer.GameServer.Utility;

namespace LibServer.GameServer
{
    class GameServer : AppServer<ClientSession, BinaryRequestInfo>
    {
        public GameServer()
            : base(new DefaultReceiveFilterFactory<ClientReceiveFilter, BinaryRequestInfo>())
        {
            
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            ServerUtil.instance.Init();

            this.NewSessionConnected += new SessionHandler<ClientSession>(OnSessionConnected);
            this.NewRequestReceived += new RequestHandler<ClientSession, BinaryRequestInfo>(OnRequestReceived);
            this.SessionClosed += new SessionHandler<ClientSession, CloseReason>(OnSessionClose);
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            ServerUtil.instance.Close();

            this.NewSessionConnected -= new SessionHandler<ClientSession>(OnSessionConnected);
            this.NewRequestReceived -= new RequestHandler<ClientSession, BinaryRequestInfo>(OnRequestReceived);
            this.SessionClosed -= new SessionHandler<ClientSession, CloseReason>(OnSessionClose);
        }

        /// <summary>
        /// Session连接
        /// </summary>
        void OnSessionConnected(ClientSession session)
        {
            SocketUtil.instance.OnSessionConnected(session);
        }

        /// <summary>
        /// 数据接收
        /// </summary>
        void OnRequestReceived(ClientSession session, BinaryRequestInfo requestInfo)
        {
            SocketUtil.instance.OnRequestReceived(session, requestInfo);
        }

        void OnSessionClose(ClientSession session, CloseReason closeReason)
        {
            SocketUtil.instance.OnSessionClose(session);
        }
    }
}
