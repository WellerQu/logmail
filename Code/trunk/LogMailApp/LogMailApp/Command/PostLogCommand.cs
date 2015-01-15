using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Communication;

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

            int keys = int.Parse(keysAppSetting);

            PosterProduct product = PosterFactory.CreateProduct(keys);
            product.Run(this.UserData);
        }
    }
}
