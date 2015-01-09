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

            this.VMWarningWindow = this.Resources["VMWarningWindowDataSource"] as VMWarningWindow;
            //this.VMWarningWindow.Text = "Here is Warning";
        }

        public WarningWindow(string text)
            : this()
        {
            this.WarningText = text;
        }

        private VMWarningWindow VMWarningWindow = null;

        public string WarningText
        {
            set
            {
                this.VMWarningWindow.Text = value;
            }
        }

        private void Grid_Title_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown(0);
        }
    }
}
