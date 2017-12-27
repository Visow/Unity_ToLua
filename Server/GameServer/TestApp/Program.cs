using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using LibServer.DBBase;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
namespace TestApp
{
    class Program
    {

        static DBThread DBhandler = new DBThread();
        static DBThread DBhandler1 = new DBThread();
        static DBThread DBhandler2 = new DBThread();

        static int actionCount = 0;
        static int actionCount2 = 0;

        static void Main(string[] args)
        {
            #region
            /*
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DBhandler.addTask("select * from t_users where userid = 10", (MySqlDataReader reader, Object o) =>
                        {
                            while (reader.Read())
                            {
                                int excuteIndex = (int)o;
                                string str = "";
                                for (int j = 0; j < reader.FieldCount; j++)
                                {
                                    str += "-" + reader[j].ToString();
                                }

                                Console.WriteLine(excuteIndex + " =====>" + str);
                            }
                        }, actionCount++);
                    }
                    Thread.Sleep(100);
                }
            });
            thread.Start();

            Thread thread2 = new Thread(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DBhandler1.addTask("select * from t_users where userid = 9", (MySqlDataReader reader, Object o) =>
                        {
                            while (reader.Read())
                            {
                                int excuteIndex = (int)o;
                                string str = "";
                                for (int j = 0; j < reader.FieldCount; j++)
                                {
                                    str += "-" + reader[j].ToString();
                                }

                                Console.WriteLine(excuteIndex + " =====>" + str);
                            }
                        }, actionCount++);
                    }
                    Thread.Sleep(100);
                }
            });
            thread2.Start();

            Thread thread3 = new Thread(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DBhandler2.addTask("select * from t_users where userid = 9", (MySqlDataReader reader, Object o) =>
                        {
                            while (reader.Read())
                            {
                                int excuteIndex = (int)o;
                                string str = "";
                                for (int j = 0; j < reader.FieldCount; j++)
                                {
                                    str += "-" + reader[j].ToString();
                                }

                                Console.WriteLine(excuteIndex + " =====>" + str);
                            }
                        }, actionCount++);
                    }
                    Thread.Sleep(100);
                }
            });
            thread3.Start();
             */
            #endregion

            #region
            MySqlConnection conn = DBConnPool.Instance.GetConn("Database=nodejs;Data Source=127.0.0.1;Port=3306;User Id=root;Password=000000;Charset=utf8;TreatTinyAsBoolean=false;");
            conn.Open();
            DBStoredProcedCmd cmd = new DBStoredProcedCmd("select_users", conn);
            cmd.AddParam("user_id", 9);
            cmd.Execute();

            for (int i = 0; i < cmd.DataResult().Rows.Count; i++)
            {
                Console.WriteLine("columsindex = " + i + "values ==>");
                string value = String.Empty;
                value += cmd.DataResult().Rows[i]["userid"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["account"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["name"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["sex"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["headimg"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["lv"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["exp"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["coins"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["gems"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["roomid"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["name"].ToString() + '\t';
                value += cmd.DataResult().Rows[i]["history"].ToString() + '\t';
                Console.WriteLine(value);
            }
            

            #endregion

            Console.ReadKey();
        }
    }
}
