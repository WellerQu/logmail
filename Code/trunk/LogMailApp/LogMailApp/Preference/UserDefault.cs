using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LogMailApp.Preference
{
    /// <summary>
    /// 用户首选项
    /// </summary>
    class UserDefault
    {
        static UserDefault()
        {
            if (null == Instance)
                Instance = new UserDefault();

            if (null == Instance.PreferenceDoc)
                Instance.PreferenceDoc = new XmlDocument();

            Instance.PreferenceDoc.Load(Path.Combine(Environment.CurrentDirectory, PREFERENCEXML_NAME));
        }

        protected UserDefault()
        {
        }

        private const string PREFERENCEXML_NAME = "Preference/Preference.plist";

        #region 首选项节点路径
        private const string ISFIRSTUSING_NODE_PATH = "preference/isFirstUsing";
        private const string EMAIL_NODE_PATH = "preference/email";
        private const string TO_NODE_PATH = "preference/to";
        private const string WITH_NODE_PATH = "preference/with";
        private const string WHEN_NODE_PATH = "preference/when";
        private const string REPORT_NODE_PATH = "preference/report";
        private const string DIRECTORY_NODE_PATH = "preference/directory";
        private const string README_NODE_PATH = "preference/readme";
        private const string EMPTY_NODE_PATH = "preference/empty";
        #endregion

        private XmlDocument PreferenceDoc = null;
        public static UserDefault Instance { get; protected set; }

        public bool IsFirstUsing
        {
            get
            {
                return bool.Parse(this.PreferenceDoc.SelectSingleNode(ISFIRSTUSING_NODE_PATH).InnerText);
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(ISFIRSTUSING_NODE_PATH).InnerText = value.ToString();
                this.Save();
            }
        }

        public string Email
        {
            get
            {
                return this.PreferenceDoc.SelectSingleNode(EMAIL_NODE_PATH).InnerText;
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(EMAIL_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        public string To
        {
            get
            {
                return this.PreferenceDoc.SelectSingleNode(TO_NODE_PATH).InnerText;
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(TO_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        public string With
        {
            get
            {
                return this.PreferenceDoc.SelectSingleNode(WITH_NODE_PATH).InnerText;
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(WITH_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        /// <summary>
        /// 获取或设置是否生成周报内容
        /// </summary>
        public bool NeedReport
        {
            get
            {
                return bool.Parse(this.PreferenceDoc.SelectSingleNode(REPORT_NODE_PATH).InnerText);
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(REPORT_NODE_PATH).InnerText = value.ToString();
                this.Save();
            }
        }

        /// <summary>
        /// 获取或设置存放日志文档的目录
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return this.PreferenceDoc.SelectSingleNode(DIRECTORY_NODE_PATH).InnerText;
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(DIRECTORY_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        /// <summary>
        /// 获取或设置当日志内容为空时, 将要发送的默认内容
        /// </summary>
        public string DefaultEmpty
        {
            get
            {
                return this.PreferenceDoc.SelectSingleNode(EMPTY_NODE_PATH).InnerText;
            }
            set
            {
                this.PreferenceDoc.SelectSingleNode(EMPTY_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        /// <summary>
        /// 获取README.md文件的内容
        /// </summary>
        public string[] Readme
        {
            get
            {
                string path = this.PreferenceDoc.SelectSingleNode(README_NODE_PATH).InnerText;
                string[] content = File.ReadAllLines(path, Encoding.UTF8);

                return content;
            }
        }

        /// <summary>
        /// 保存首选项数据
        /// </summary>
        protected void Save()
        {
            this.PreferenceDoc.Save(PREFERENCEXML_NAME);
        }
    }
}
