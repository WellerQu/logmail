using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Storage
{
    public class LogDocument
    {
        public LogDocument()
        {
            this.Storager = StoragerFactory.Create();
        }

        protected IStorager Storager { get; set; }

        public void File()
        {
            this.Storager.File();
        }

        public void Save(string name, string[] content)
        {
            this.Storager.Key = name;
            this.Storager.Content = content;

            this.Storager.Save();   // 保存
        }

        public void Delete(string name)
        {
            this.Storager.Key = name;
            this.Storager.Delete();
        }

        public string[] Load(string name)
        {
            this.Storager.Key = name;
            this.Storager.Load();

            return this.Storager.Content;
        }
    }
}
