using System;

namespace LibServer.Common {
    public interface IMessage {
        void OnMessage(ClientSession session, ByteBuffer buffer);
    }
}
