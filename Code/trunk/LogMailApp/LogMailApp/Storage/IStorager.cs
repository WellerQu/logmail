using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Storage
{
    public interface IStorager
    {
        /// <summary>
        /// 日志主键标识
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        string[] Content { get; set; }
        /// <summary>
        /// 归档
        /// </summary>
        void File();
        /// <summary>
        /// 加载
        /// </summary>
        void Load();
        /// <summary>
        /// 删除
        /// </summary>
        void Delete();
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
    }
}
