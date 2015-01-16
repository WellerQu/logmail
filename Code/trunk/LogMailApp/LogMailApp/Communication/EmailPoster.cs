using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;
using LogMailApp.Exception;
using LogMailApp.Properties;
using LogMailApp.Command;
using LogMailApp.Storage;

namespace LogMailApp.Communication
{
    /// <summary>
    /// 仅支持文字内容的电子邮件的通信发送类
    /// </summary>
    class EmailPoster : IPoster
    {
        public EmailPoster()
        {
        }

        private SmtpClient Client = null;

        #region IPoster 成员

        public bool WillStopOnError
        {
            get { return false; }
        }

        public virtual void Ready(UserData userData)
        {
            string userName = UserDefault.Instance.Email;
            string password = UserDefault.Instance.Password;

            if (string.IsNullOrEmpty(userName))
            {
                throw new UserInformationDeficiencyException("电子邮件用户名缺失");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new UserInformationDeficiencyException("电子邮件密码缺失");
            }

            this.Client = new SmtpClient(UserDefault.Instance.SMTP);
            this.Client.UseDefaultCredentials = false;
            this.Client.Credentials = new System.Net.NetworkCredential(userName, password);
        }

        public virtual void Post(UserData userData)
        {
            LogDocument.Doc[] doc = userData[PostLogCommand.USER_DATA_CONTENT_KEY] as LogDocument.Doc[];

            if (doc == null || doc.Length == 0)
            {
                throw new EmailContentNullException(Resources.NULL_EXCEPTION_MESSAGE);
            }

            string[] withPeoples = UserDefault.Instance.With.Split(';');    // 抄送目标
            string[] toPeoples = UserDefault.Instance.To.Split(';');        // 发送目标

            StringBuilder sbContent = new StringBuilder();

            foreach (var msg in doc)
            {
                sbContent.AppendLine(msg.PrimaryKey);
                sbContent.AppendLine(msg.Content);
                sbContent.AppendLine(); // 换行
                sbContent.AppendLine(); // 换行
            }

            sbContent.AppendLine(UserDefault.Instance.Footer);  // 添加小尾巴

            MailMessage message = message = new MailMessage();
            message.From = new MailAddress(UserDefault.Instance.Email);

            foreach (var to in toPeoples)
            {
                if (!string.IsNullOrEmpty(to.Trim()))
                    message.To.Add(to);
            }

            foreach (var cc in withPeoples)
            {
                if (!string.IsNullOrEmpty(cc.Trim()))
                    message.CC.Add(cc);
            }

            message.Subject = UserDefault.Instance.Subject;
            message.Body = sbContent.ToString();

            this.Client.Send(message);
        }

        #endregion
    }
}
