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
        string PrimaryKey { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 是否已归档
        /// </summary>
        bool IsFiled { get; set; }
        /// <summary>
        /// 归档: 归档后的日志不会再被发送
        /// </summary>
        void File();
        /// <summary>
        /// 回档: 回档后的日志可再次发送
        /// </summary>
        void Back();
        /// <summary>
        /// 决定未归档的日志如何处理
        /// action将得到name文件名和content内容
        /// </summary>
        /// <param name="action">处理过程</param>
        void UnFile(Action<string, string> action);
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
