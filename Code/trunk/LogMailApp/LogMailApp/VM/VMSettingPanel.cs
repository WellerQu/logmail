using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMSettingPanel : ViewModel
    {
        public VMSettingPanel()
        {
            this.SettingButtonText = Resources.SettingButtonText;

            this.EmailTitleText = Resources.EmailTitleText;
            this.PasswordTitleText = Resources.PasswordTitleText;
            this.ToTitleText = Resources.ToTitleText;
            this.WithTitleText = Resources.WithTitleText;
            this.EmptyTitleText = Resources.EmptyTitleText;
            this.DirTitleText = Resources.DirTitleText;
            this.SmtpTitleText = Resources.SmtpTitleText;
        }

        public string SettingButtonText { get; private set; }
        public string EmailTitleText { get; private set; }
        public string PasswordTitleText { get; private set; }
        public string SmtpTitleText { get; private set; }
        public string ToTitleText { get; private set; }
        public string WithTitleText { get; private set; }
        public string EmptyTitleText { get; private set; }
        public string DirTitleText { get; private set; }

        public string Email
        {
            get { return UserDefault.Instance.Email; }
            set
            {
                UserDefault.Instance.Email = value;
                this.OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get
            {
                return UserDefault.Instance.Password;
            }
            set
            {
                UserDefault.Instance.Password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public string Smtp
        {
            get
            {
                return UserDefault.Instance.SMTP;
            }
            set
            {
                UserDefault.Instance.SMTP = value;
                this.OnPropertyChanged("Smtp");
            }
        }

        public string To
        {
            get { return UserDefault.Instance.To; }
            set
            {
                UserDefault.Instance.To = value;
                this.OnPropertyChanged("To");
            }
        }

        public string With
        {
            get { return UserDefault.Instance.With; }
            set
            {
                UserDefault.Instance.With = value;
                this.OnPropertyChanged("With");
            }
        }

        public string Empty
        {
            get { return UserDefault.Instance.DefaultEmpty; }
            set
            {
                UserDefault.Instance.DefaultEmpty = value;
                this.OnPropertyChanged("Empty");
            }
        }

        public string Dir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UserDefault.Instance.DirectoryPath);
            }
            set
            {
                UserDefault.Instance.DirectoryPath = value;
                this.OnPropertyChanged("Dir");
            }
        }
    }
}
