using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LogMailApp
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

            Instance.PreferenceDoc.Load(PREFERENCEXML_NAME);
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
        #endregion

        private XmlDocument PreferenceDoc = null;
        public static UserDefault Instance { get; protected set; }

        public bool IsFirstUsing
        {
            get
           {
                return bool.Parse(Instance.PreferenceDoc.SelectSingleNode(ISFIRSTUSING_NODE_PATH).InnerText);
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(ISFIRSTUSING_NODE_PATH).InnerText = value.ToString();
                this.Save();
            }
        }

        public string Email
        {
            get
            {
                return Instance.PreferenceDoc.SelectSingleNode(EMAIL_NODE_PATH).InnerText;
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(EMAIL_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        public string To
        {
            get
            {
                return Instance.PreferenceDoc.SelectSingleNode(TO_NODE_PATH).InnerText;
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(TO_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        public string With
        {
            get
            {
                return Instance.PreferenceDoc.SelectSingleNode(WITH_NODE_PATH).InnerText;
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(WITH_NODE_PATH).InnerText = value;
                this.Save();
            }
        }

        public bool NeedReport
        {
            get
            {
                return bool.Parse(Instance.PreferenceDoc.SelectSingleNode(REPORT_NODE_PATH).InnerText);
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(REPORT_NODE_PATH).InnerText = value.ToString();
                this.Save();
            }
        }

        public string DirectoryPath
        {
            get
            {
                return Instance.PreferenceDoc.SelectSingleNode(DIRECTORY_NODE_PATH).InnerText;
            }
            set
            {
                Instance.PreferenceDoc.SelectSingleNode(DIRECTORY_NODE_PATH).InnerText = value;
                this.Save();
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
