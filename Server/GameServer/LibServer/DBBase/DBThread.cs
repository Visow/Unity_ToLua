using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace LibServer.DBBase
{
    public class DBThread : LibServer.Common.BaseThread
    {
        public delegate void DBHandlerDelegate(MySqlConnection conn, LibServer.Common.CustomArgs other = null);
        class TaskCmd
        {
            public DBHandlerDelegate handler;
            public LibServer.Common.CustomArgs args;
            public TaskCmd(DBHandlerDelegate handler, LibServer.Common.CustomArgs args)
            {
                this.handler = handler;
                this.args = args;
            }
        }


        public string conn_string = "Database=nodejs;Data Source=127.0.0.1;Port=3306;User Id=root;Password=000000;Charset=utf8;TreatTinyAsBoolean=false;";

        public void setConnString(string str)
        {
            conn_string = str;
        }



        Queue<TaskCmd> _taskQueue = new Queue<TaskCmd>();



        public void addTask(DBHandlerDelegate handler, LibServer.Common.CustomArgs args = null)
        {
            lock (_taskQueue)
            {
                try
                {
                    _taskQueue.Enqueue(new TaskCmd(handler, args));
                    Start();
                }
                catch (Exception e)
                {
                    Utility.Debuger.Error(e);
                    return;
                }
            }
        }

        public override void run()
        {
            while (DoThreadFunc())
            {

            }
            IsRuning = false;
        }

        private bool DoThreadFunc()
        {
            try
            {
                TaskCmd taskCmd = null;
                lock (_taskQueue)
                {
                    if (_taskQueue.Count == 0)
                        return false;
                    taskCmd = _taskQueue.Dequeue();
                }
                if (taskCmd == null)
                    return false;
                MySqlConnection conn = DBConnPool.Instance.GetConn(conn_string);
                conn.Open();
                taskCmd.handler(conn, taskCmd.args);
                Console.WriteLine("---------------" + DBConnPool.Instance.Count + "---------------");
                return true;
            }
            catch (Exception e)
            {
                Utility.Debuger.Error(e);
                return false;
            }
        }
    }
}
