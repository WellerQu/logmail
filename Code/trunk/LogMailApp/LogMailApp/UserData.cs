using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp
{
    class UserData
    {
        public UserData()
        {
            Data = new Dictionary<string, object>();
        }

        private Dictionary<string, object> Data = null;

        public object this[string key]
        {
            get
            {
                object value = null;
                if (Data.ContainsKey(key))
                {
                    value = Data[key];
                }

                return value;
            }
            set
            {
                if (!Data.ContainsKey(key))
                {
                    Data.Add(key, value);
                }
                else
                {
                    Data[key] = value;
                }
            }
        }
    }
}
