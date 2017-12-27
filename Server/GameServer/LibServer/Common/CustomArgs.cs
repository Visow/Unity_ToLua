using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServer.Common
{
    public class CustomArgs : EventArgs
    {
        public CustomArgs()
        {
            args = new Dictionary<string, object>();
        }

        Dictionary<string, object> args;

        public void ResetParam()
        {
            args.Clear();
        }

        public void AddParam(string key, object value)
        {
            args.Add(key, value);
        }

        public object GetParam(string key)
        {
            //object value = null;
            //args.TryGetValue(key, out value);
            //return value;
            return args[key];
        }
    }
}
