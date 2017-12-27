using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibServer.Common;
using MySql.Data.MySqlClient;
using LibServer.GameServer.Utility;
namespace LibServer.GameServer.Message
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
            string token = buffer.ReadString();

            CustomArgs args = new CustomArgs();
            args.AddParam("token", token);
            args.AddParam("session", session);
            DBUtil.Instance.RequestDB(this, args);
        }

        public void OnDBTask(MySqlConnection conn, CustomArgs args)
        {
            string token = args.GetParam("token") as string;
            ClientSession session = args.GetParam("session") as ClientSession;
            DBBase.DBStoredProcedCmd cmd = new DBBase.DBStoredProcedCmd("PRO_CHECK_LOGIN", conn);
            cmd.AddParamVChar("token", token, token.Length);
            int bSuc = cmd.Execute();
            if (bSuc == 0)
            {
                string account = cmd.GetValue(0, "account") as string;
                int nGold = (int)cmd.GetValue(0, "gold");
                ByteBuffer buffer = new ByteBuffer();
                buffer.WriteString(account);
                buffer.WriteInt(nGold);
                UserUtil.Add(session.uid, session);
                SocketUtil.SendMessage(session, Protocal.Login, buffer);
                return;
            }

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
