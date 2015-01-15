using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;

namespace LogMailApp.Storage
{
    class LogFileStorager : IStorager
    {
        private const string FILE_EXTEND = ".lma";

        #region IStorager 成员
        public string Key { get; set; } // 示例: 2015-01-13

        public string[] Content { get; set; }

        public void File()
        {
            string fileDirPath = string.Format("./{0}/",
                UserDefault.Instance.DirectoryPath.TrimEnd('/', '\\'));

            DirectoryInfo dir = new DirectoryInfo("./");
            FileInfo[] files = dir.GetFiles("*" + FILE_EXTEND, SearchOption.TopDirectoryOnly);
            if (files != null)
            {
                string fileName = null;
                string[] fileNameParts = null;
                string dirName = fileDirPath;   // 初始为 ./MyLogs/

                foreach (FileInfo f in files)
                {
                    fileName = f.Name.Replace(FILE_EXTEND, string.Empty);
                    // 按照约定, 文件名应该为 yyyy-MM-dd.lma
                    // 所以...
                    fileNameParts = fileName.Split('-');
                    if (fileNameParts != null && fileNameParts.Length == 3)
                    {
                        // 0 year
                        // 1 month
                        // 2 day
                        // 依此检查目录是否存在, 这和Java不一样, 需要手动检查
                        for (int i = 0; i < fileNameParts.Length; i++)
                        {
                            dirName = Path.Combine(dirName, fileNameParts[i]);
                            if (Directory.Exists(dirName))
                            {
                                // 如果目录不存在, 则创建目录
                                Directory.CreateDirectory(dirName);
                            }
                        }

                        // 移动文件到目标位置, 完成归档, 示例: ./MyLogs/2015/01/15.lma
                        f.MoveTo(Path.Combine(dirName, fileNameParts[2] + FILE_EXTEND));
                    }
                }
            }
        }

        public void Load()
        {
            FileInfo file = this.Parse(this.Key);
            if (file.Exists)
                this.Content = System.IO.File.ReadAllLines(file.FullName, Encoding.UTF8);
        }

        public void Delete()
        {
            FileInfo file = this.Parse(this.Key);
            if (file.Exists)
                file.Delete();
        }

        public void Save()
        {
            FileInfo file = this.Parse(this.Key);
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = file.Open(FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                if (this.Content != null)
                {
                    foreach (var line in this.Content)
                    {
                        sw.WriteLine(line);
                    }
                }
                sw.Flush();
                fs.Flush();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion

        private FileInfo Parse(string fileName)
        {
            FileInfo file = null;
            string filePath = string.Format("./{0}", fileName);
            if (System.IO.File.Exists(filePath))
            {
                file = new FileInfo(filePath);
            }
            else
            {
                filePath = string.Format("./{0}/",
                    UserDefault.Instance.DirectoryPath.TrimEnd('/', '\\'));
                string[] fileNamePart = fileName.Split('-');

                for (int i = 0; i < fileNamePart.Length; i++)
                {
                    filePath = Path.Combine(filePath, fileNamePart[i]);
                }

                file = new FileInfo(filePath + FILE_EXTEND);
            }
            return file;
        }
    }
}
