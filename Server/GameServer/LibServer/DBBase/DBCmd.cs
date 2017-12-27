using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

using MySql.Data.MySqlClient;

namespace LibServer.DBBase
{
    public class DBStoredProcedCmd
    {
        string _storedProcedName;
        MySqlCommand _MySqlCmd;
        bool bExecute = false;

        DataTable _dataResult = new DataTable();
        public DBStoredProcedCmd(string storedProcedName, MySqlConnection conn)
        {
            _MySqlCmd = new MySqlCommand(storedProcedName, conn);
            _MySqlCmd.CommandType = CommandType.StoredProcedure;            
        }

        public void AddParam(string paramName, sbyte value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.Byte);
            param.Value = value;
        }

        public void AddParam(string paramName, byte value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.UByte);
            param.Value = value;
        }


        public void AddParam(string paramName, short value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.Int16);
            param.Value = value;
        }

        public void AddParam(string paramName, ushort value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.UInt16);
            
            param.Value = value;
        }

        public void AddParam(string paramName, int value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.Int32);
            param.Value = value;
        }

        public void AddParam(string paramName, uint value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.UInt32);
            param.Value = value;
        }

        public void AddParam(string paramName, long value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.Int64);
            param.Value = value;
        }

        public void AddParam(string paramName, ulong value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.UInt64);
            param.Value = value;
        }

        public void AddParamVChar(string paramName, string value, int nSize)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.VarChar, nSize);
            param.Value = value;
        }

        public void AddParamVString(string paramName, string value, int nSize)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.VarString, nSize);
            param.Value = value;
        }

        public void AddParamString(string paramName, string value, int nSize)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.String, nSize);
            param.Value = value;
        }

        public void AddParamSystemTime(string paramName, System.DateTime value)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.DateTime);
            param.Value = value;
        }

        public void AddOutParamText(string paramName, int nSize)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.String);
            param.Direction = System.Data.ParameterDirection.Output;
        }

        public void AddOutParamInt(string paramName)
        {
            MySqlParameter param = _MySqlCmd.Parameters.Add("@" + paramName, MySqlDbType.Int32);
            param.Direction = System.Data.ParameterDirection.Output;
            param.Value = 0;
        }

        public MySqlParameterCollection Parameters
        {
            get { return _MySqlCmd.Parameters; }
        }



        public int Execute()
        {
            if (_MySqlCmd == null)
                return -1;
            bExecute = true;
            _dataResult.Clear();

            try
            {
                _MySqlCmd.ExecuteNonQuery();
                using (MySqlDataAdapter mysqlAdapter = new MySqlDataAdapter(_MySqlCmd))
                {
                    mysqlAdapter.Fill(_dataResult);
                }
                return 0;
            }
            catch (Exception e)
            {
                Utility.Debuger.Error(e);
                return -1;
            }
        }

        public System.Data.DataTable DataResult()
        {
            return _dataResult;
        }

        public DataColumnCollection Columns()
        {
            return _dataResult.Columns;
        }

        public int ColumnsCount()
        {
            return _dataResult.Columns.Count;
        }

        public int RowCount()
        {
            return _dataResult.Rows.Count;
        }

        public Object GetValue(int rowIndex, string column)
        {
            return _dataResult.Rows[rowIndex][column];
        }

        public MySqlParameter GetParam(string paramName)
        {
            return _MySqlCmd.Parameters["@" + paramName];
        }
    }
}
