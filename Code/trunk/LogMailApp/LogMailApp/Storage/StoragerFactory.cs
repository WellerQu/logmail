using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Storage
{
    public class StoragerFactory
    {
        private const string STORAGE_KEY = "storage";

        public static IStorager Create()
        {
            string descripter = ConfigurationManager.AppSettings[STORAGE_KEY];
            Type type = Type.GetType(descripter);
            IStorager storager = Activator.CreateInstance(type) as IStorager;

            return storager;
        }
    }
}
