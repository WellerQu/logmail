using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Storage
{
    public class LogDocument
    {
        public class Doc
        {
            public string PrimaryKey { get; set; }
            public string Content { get; set; }
        }

        public LogDocument()
        {
            this.Storager = StoragerFactory.Create();
        }

        protected IStorager Storager { get; set; }

        public void File()
        {
            this.Storager.File();
        }

        public void Back(string name)
        {
            this.Storager.PrimaryKey = name;

            this.Storager.Back();
        }

        public bool IsFiled()
        {
            return this.Storager.IsFiled;
        }

        public void Save(string name, string content)
        {
            this.Storager.PrimaryKey = name;
            this.Storager.Content = content;

            this.Storager.Save();   // 保存
        }

        public void Delete(string name)
        {
            this.Storager.PrimaryKey = name;
            this.Storager.Delete();
        }

        public string Load(string name)
        {
            this.Storager.PrimaryKey = name;
            this.Storager.Load();

            return this.Storager.Content ?? string.Empty;
        }

        public Doc[] GetUnFileDocument()
        {
            List<Doc> list = new List<Doc>();
            this.Storager.UnFile((string name, string content) =>
            {
                list.Add(new Doc() { PrimaryKey = name, Content = content });
            });

            return list.ToArray();
        }
    }
}
