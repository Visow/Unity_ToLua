using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibServer.Common;
using LibServer.Utility;

namespace LibServer.Message
{
    class Login :  IMessage
    {
        public void OnMessage(ClientSession session, ByteBuffer buffer)
        {
            byte b = buffer.ReadByte();
            ProtocalType type = (ProtocalType)b;    //协议类型
            switch (type)
            {
                case ProtocalType.BINARY:
                    OnBinaryMessage(session, buffer);
                    break;
                case ProtocalType.PB_LUA:
                    OnPbLuaMessage(session, buffer);
                    break;
                case ProtocalType.PBC:
                    OnPbcMessage(session, buffer);
                    break;
                case ProtocalType.SPROTO:
                    OnSprotoMessage(session, buffer);
                    break;
            }
        }

        /// <summary>
        /// 二进制消息
        /// </summary>
        void OnBinaryMessage(ClientSession session, ByteBuffer buffer)
        {
            session.uid = buffer.ReadInt();
            string str = buffer.ReadString();
            
            ByteBuffer newBuffer = new ByteBuffer();
            newBuffer.WriteByte((byte)ProtocalType.BINARY);
            newBuffer.WriteString(str);
            SocketUtil.SendMessage(session, Protocal.Login, newBuffer);

            UserUtil.Add(session.uid, session);
            Console.WriteLine("OnBinaryMessage--->>>" + str + session.uid);
        }

        /// <summary>
        /// pblua消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="buffer"></param>
        void OnPbLuaMessage(ClientSession session, ByteBuffer buffer)
        {

        }

        /// <summary>
        /// pbc消息
        /// </summary>
        void OnPbcMessage(ClientSession session, ByteBuffer buffer)
        {

        }

        /// <summary>
        /// sproto消息
        /// </summary>
        void OnSprotoMessage(ClientSession session, ByteBuffer buffer)
        {

        }
    }
}
