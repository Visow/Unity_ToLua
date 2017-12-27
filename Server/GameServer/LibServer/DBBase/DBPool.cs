using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;

namespace LibServer.DBBase
{
    public sealed class DBConnPool : LibServer.Common.SyncPool
    {
        private DBConnPool() { }
        public static readonly DBConnPool Instance = new DBConnPool();

        protected override object Create()
        {
            return new MySqlConnection();
        }

        /// <summary>
        /// 是否在工作状态
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected override bool Validate(object o)
        {
            try
            {
                MySqlConnection conn = (MySqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        protected override void Close(object o)
        {
            try {
                MySqlConnection conn = (MySqlConnection)o;
                if (!conn.State.Equals(ConnectionState.Closed))
                    conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }

        public MySqlConnection GetConn(string connstring = null)
        {
            try
            {
                MySqlConnection conn = base.Get() as MySqlConnection;
                conn.ConnectionString = connstring;
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public void ResetConn(MySqlConnection conn)
        {
            base.Reset(conn);
        }
    }
}
