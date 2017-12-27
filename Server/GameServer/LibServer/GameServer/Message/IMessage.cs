using System;

using LibServer.Common;
using LibServer.DBBase;
using MySql.Data.MySqlClient;
using LibServer.GameServer.Utility;

namespace LibServer.GameServer.Message {
    public interface IMessage {
        void OnMessage(ClientSession session, ByteBuffer buffer);
        //DBThread.DBHandlerDelegate OnDBTaskSink = (MySqlConnection conn, CustomArgs args) =>
        //{
        //    this.OnDBTask(conn, args);
        //};

        void OnDBTask(MySqlConnection conn, CustomArgs args);
    }
}
