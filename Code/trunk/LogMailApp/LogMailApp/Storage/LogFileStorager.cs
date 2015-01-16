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
        public LogFileStorager()
        {
            string currentDayLogKey = DateTime.Now.ToString("yyyy-MM-dd");

            FileInfo file = this.Parse(currentDayLogKey);
            if (!file.Exists)
            {
                using (FileStream fs = file.Create())
                {
                    fs.Close();
                }
            }
        }

        private const string FILE_EXTEND = ".lma";

        #region IStorager 成员
        public string PrimaryKey { get; set; } // 示例: 2015-01-13

        public string Content { get; set; }

        public void File()
        {
            string fileDirPath = Path.Combine(
                UserDefault.Instance.StartupPath,
                UserDefault.Instance.DirectoryPath.TrimEnd('/', '\\'));

            DirectoryInfo dir = new DirectoryInfo(UserDefault.Instance.StartupPath);

            FileInfo[] files = dir.GetFiles("*" + FILE_EXTEND, SearchOption.TopDirectoryOnly);
            if (files != null)
            {
                foreach (FileInfo f in files)
                {
                    string dirName = fileDirPath;   // 初始为 ./MyLogs/

                    string fileName = f.Name.Replace(FILE_EXTEND, string.Empty);
                    // 按照约定, 文件名应该为 yyyy-MM-dd.lma
                    // 所以...
                    string[] fileNameParts = fileName.Split('-');
                    if (fileNameParts != null && fileNameParts.Length == 3)
                    {
                        // 0 year
                        // 1 month
                        // 2 day
                        for (int i = 0; i < fileNameParts.Length - 1; i++)
                        {
                            dirName = Path.Combine(dirName, fileNameParts[i]);
                            if (!Directory.Exists(dirName))
                            {
                                // 如果目录不存在, 则创建目录
                                Directory.CreateDirectory(dirName);
                            }
                        }

                        string dest = Path.Combine(dirName, fileNameParts[2] + FILE_EXTEND);

                        if (System.IO.File.Exists(dest))
                            System.IO.File.Delete(dest);

                        // 移动文件到目标位置, 完成归档, 示例: ./MyLogs/2015/01/15.lma
                        f.MoveTo(dest);
                    }
                }
            }
        }

        public void UnFile(Action<string, string> action)
        {
            string[] files = Directory.GetFiles(UserDefault.Instance.StartupPath, "*.lma", SearchOption.TopDirectoryOnly);
            if (files != null && files.Length > 0)
            {
                foreach (string f in files)
                {
                    FileInfo file = new FileInfo(f);
                    using (FileStream fs = file.OpenRead())
                    {
                        StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                        string content = sr.ReadToEnd();
                        sr.Close();

                        if (string.IsNullOrEmpty(content))
                            content = UserDefault.Instance.DefaultEmpty;

                        action(file.Name.Replace(FILE_EXTEND, string.Empty), content);
                    }
                }
            }
        }

        public void Load()
        {
            FileInfo file = this.Parse(this.PrimaryKey);
            if (file.Exists)
                this.Content = System.IO.File.ReadAllText(file.FullName, Encoding.UTF8);
        }

        public void Delete()
        {
            FileInfo file = this.Parse(this.PrimaryKey);
            if (file.Exists)
                file.Delete();
        }

        public void Save()
        {
            FileInfo file = this.Parse(this.PrimaryKey);
            FileStream fs = null;

            try
            {
                using (fs = file.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                {
                    if (this.Content != null)
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(this.Content);
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private FileInfo Parse(string fileName)
        {
            FileInfo file = null;

            string filePath = string.Format("{0}{1}", fileName, FILE_EXTEND);
            string unFileLogPath = Path.Combine(UserDefault.Instance.StartupPath, filePath);
            if (System.IO.File.Exists(unFileLogPath))
            {
                // 解析未归档的日志
                file = new FileInfo(unFileLogPath);
            }
            else
            {
                // 仅解析已归档日志
                filePath = Path.Combine(
                    UserDefault.Instance.StartupPath,
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
