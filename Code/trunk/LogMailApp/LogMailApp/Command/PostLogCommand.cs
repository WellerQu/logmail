using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Communication;
using LogMailApp.Exception;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    class PostLogCommand : CommandBase
    {
        private const string APPSETTINGS_KEY = "communication";

        public const string USER_DATA_CONTENT_KEY = "Post.Content";

        private UserData UserData;

        public PostLogCommand(UserData userData)
        {
            this.UserData = userData ?? new UserData();
        }

        public override void Execute(object parameter)
        {
            string keysAppSetting = ConfigurationManager.AppSettings[APPSETTINGS_KEY];
            string error = string.Empty;

            int keys = int.Parse(keysAppSetting);

            LogDocument doc = new LogDocument();
            this.UserData[PostLogCommand.USER_DATA_CONTENT_KEY] = doc.GetUnFileDocument();

            PosterProduct product = PosterFactory.CreateProduct(keys);
            product.Run(this.UserData, ref error);

            if (!string.IsNullOrEmpty(error))
            {
                throw new PostErrorException("发送信息过程存在错误: " + error.TrimEnd(';'));
            }
        }
    }
}
