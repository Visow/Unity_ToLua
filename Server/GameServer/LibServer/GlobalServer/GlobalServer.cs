using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

using LibServer.Utility;

namespace LibServer.GlobalServer
{
    class GlobalServer : AppServer<GameSession, BinaryRequestInfo>
    {
        public GlobalServer()
            : base(new DefaultReceiveFilterFactory<GameReceiveFilter, BinaryRequestInfo>())
        {
            
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            this.NewSessionConnected += new SessionHandler<GameSession>(OnSessionConnected);
            this.NewRequestReceived += new RequestHandler<GameSession, BinaryRequestInfo>(OnRequestReceived);
            this.SessionClosed += new SessionHandler<GameSession, CloseReason>(OnSessionClose);
        }

        protected override void OnStopped()
        {
            base.OnStopped();

            this.NewSessionConnected -= new SessionHandler<GameSession>(OnSessionConnected);
            this.NewRequestReceived -= new RequestHandler<GameSession, BinaryRequestInfo>(OnRequestReceived);
            this.SessionClosed -= new SessionHandler<GameSession, CloseReason>(OnSessionClose);
        }

        /// <summary>
        /// Session连接
        /// </summary>
        void OnSessionConnected(GameSession session)
        {
            

        }

        /// <summary>
        /// 数据接收
        /// </summary>
        void OnRequestReceived(GameSession session, BinaryRequestInfo requestInfo)
        {
            
        }

        void OnSessionClose(GameSession session, CloseReason closeReason)
        {

        }
    }
}
