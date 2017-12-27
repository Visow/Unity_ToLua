using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SqlCmd
{
    static string sql_get_account = @" select * from t_accounts where account={0} and password='{1}'";
    public static string get_account_info(string account, string password)
    {
        return string.Format(sql_get_account, account, password);
    }

}

