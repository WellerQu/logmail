using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;
using LogMailApp.Exception;
using LogMailApp.Properties;

namespace LogMailApp.Communication
{
    /// <summary>
    /// 仅支持文字内容的电子邮件的通信发送类
    /// </summary>
    class EmailPoster : IPoster
    {
        public EmailPoster()
        {
            this.Client = new SmtpClient(UserDefault.Instance.SMTP);
            this.Client.UseDefaultCredentials = false;

            this.Message = new MailMessage(UserDefault.Instance.Email, UserDefault.Instance.To);
        }

        private SmtpClient Client = null;
        private MailMessage Message = null;

        private const string USER_DATA_CONTENT_KEY = "Mail.Content";
        private const string USER_DATA_EMAIL_KEY = "Mail.Address";
        private const string USER_DATA_PWD_KEY = "Mail.Password";

        #region IPoster 成员

        public bool WillStopOnError
        {
            get { return false; }
        }

        public virtual void Post(UserData userData)
        {
            string[] content = userData[USER_DATA_CONTENT_KEY] as string[];
            
            if (content == null)
            {
                throw new EmailContentNullException(Resources.NULL_EXCEPTION_MESSAGE);
            }

            StringBuilder sbContent = new StringBuilder();
            foreach (var line in content)
            {
                sbContent.AppendLine(line);
            }
            sbContent.Append(UserDefault.Instance.Footer);

            this.Message.Subject = UserDefault.Instance.Subject;
            this.Message.Body = sbContent.ToString();

            string userName = userData[USER_DATA_EMAIL_KEY] as string;
            string password = userData[USER_DATA_PWD_KEY] as string;

            this.Client.Credentials = new System.Net.NetworkCredential(userName, password);
            this.Client.Send(this.Message);
        }

        #endregion
    }
}
