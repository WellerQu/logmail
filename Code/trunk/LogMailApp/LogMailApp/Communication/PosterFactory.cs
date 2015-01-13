using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Communication
{
    class PosterFactory
    {
        static PosterFactory()
        {
            PosterDic = new Dictionary<string, IPoster>();
        }

        private static Dictionary<string, IPoster> PosterDic = null;

        public static PosterProduct CreateProduct(int keys)
        {
            PosterProduct product = null;
            int power = 0;
            int iKey = keys;

            while (iKey != 0)
            {
                if ((iKey & 1) == 1)
                {
                    string appSetting = ConfigurationManager.AppSettings[Math.Pow(2, power).ToString()];

                    IPoster poseter = null;
                    if (PosterDic.ContainsKey(appSetting))
                    {
                        poseter = PosterDic[appSetting];
                    }
                    else
                    {
                        Type type = Type.GetType(appSetting);
                        poseter = Activator.CreateInstance(type) as IPoster;

                        PosterDic.Add(appSetting, poseter);
                    }

                    if (product == null)
                    {
                        product = new PosterProduct(poseter);
                    }
                    else
                    {
                        product.Next = new PosterProduct(poseter);
                        product = product.Next;
                    }
                }

                iKey = iKey >> 1;
                power++;
            }


            return product;
        }
    }
}
