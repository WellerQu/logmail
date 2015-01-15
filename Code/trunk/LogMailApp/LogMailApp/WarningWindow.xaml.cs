using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogMailApp
{
    using System.Windows.Media.Animation;
    using VM;
    /// <summary>
    /// WarningWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WarningWindow : Window
    {
        public WarningWindow()
        {
            InitializeComponent();

            #region 窗口动画
            double heigth = SystemParameters.PrimaryScreenHeight;
            double width = SystemParameters.PrimaryScreenWidth;

            this.Top = (heigth - this.Height) / 2;
            this.Left = (width - this.Width) / 2;

            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard sb = new Storyboard();

            DoubleAnimation toMove = new DoubleAnimation();
            toMove.Duration = duration;
            toMove.From = this.Top - 30;
            toMove.To = this.Top;
            Storyboard.SetTargetProperty(toMove, new PropertyPath("Top"));

            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 0;
            toOpacity.To = 1;
            Storyboard.SetTargetProperty(toOpacity, new PropertyPath("Opacity"));

            sb.Children.Add(toMove);
            sb.Children.Add(toOpacity);

            this.BeginStoryboard(sb);
            #endregion

            this.WarningText = "Here is warning text";
        }

        public WarningWindow(string text)
            : this()
        {
            this.WarningText = text;
        }

        public string WarningText
        {
            set
            {
                (this.DataContext as VMWarningWindow).Text = value;
            }
        }

        private void Grid_Title_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_Close_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region 窗口退出动画
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard sb = new Storyboard();

            DoubleAnimation toMove = new DoubleAnimation();
            toMove.Duration = duration;
            toMove.From = this.Top;
            toMove.To = this.Top - 20;
            Storyboard.SetTargetProperty(toMove, new PropertyPath("Top"));

            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 1;
            toOpacity.To = 0;
            Storyboard.SetTargetProperty(toOpacity, new PropertyPath("Opacity"));

            sb.Children.Add(toMove);
            sb.Children.Add(toOpacity);
            sb.Completed += (object window, EventArgs evt) =>
            {
                this.Close();
            };

            this.BeginStoryboard(sb);
            #endregion
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Image_Close_PreviewMouseLeftButtonDown(sender, null);
        }
    }
}
