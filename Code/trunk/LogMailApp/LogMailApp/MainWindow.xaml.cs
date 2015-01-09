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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogMailApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.EditorPanel.Opacity = this.SettingPanel.Opacity = 0;

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
        }

        public MainWindow(bool onlySetting)
            : this()
        {
            if (onlySetting)
            {
                this.FocusBackgroundPanel.Margin = new Thickness(0, 40, 0, 0);
                this.SettingPanel.Opacity = 1;
            }
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_Close_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard sb = new Storyboard();

            DoubleAnimation toMove = new DoubleAnimation();
            toMove.Duration = duration;
            toMove.From = this.Top;
            toMove.To = this.Top - 30;
            Storyboard.SetTargetProperty(toMove, new PropertyPath("Top"));

            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 1;
            toOpacity.To = 0;
            Storyboard.SetTargetProperty(toOpacity, new PropertyPath("Opacity"));

            sb.Children.Add(toMove);
            sb.Children.Add(toOpacity);

            this.BeginStoryboard(sb);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown(0);
        }

        private void FunctionButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            int tag = Convert.ToInt16(btn.Tag);

            Duration duration = new Duration(TimeSpan.FromMilliseconds(100));

            ThicknessAnimation toMove = new ThicknessAnimation();
            toMove.To = new Thickness(0, tag, 0, 0);
            toMove.Duration = duration;

            this.FocusBackgroundPanel.BeginAnimation(Border.MarginProperty, toMove);

            if (tag == 0)
            {
                this.HidePanel(this.SettingPanel, duration);
                this.ShowPanel(this.EditorPanel, duration);
            }
            else if (tag == 40)
            {
                this.HidePanel(this.EditorPanel, duration);
                this.ShowPanel(this.SettingPanel, duration);
            }
            else if (tag == 80)
            {

            }
        }

        private void TextBlock_Date_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            for (var i = 0; i < this.DatePicker.Children.Count; i++)
            {
                TextBlock t = this.DatePicker.Children[i] as TextBlock;

                if (t == txt)
                {
                    t.FontSize = 22;
                    animation.To = new Thickness(0, 20, 0, 20);
                }
                else
                {
                    t.FontSize = 12;
                    animation.To = new Thickness(0, 10, 0, 10);
                }

                t.BeginAnimation(TextBlock.MarginProperty, animation);
            }
        }

        private void TextBox_Setting_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void TextBox_Setting_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEE, 0xEE, 0xEE));
        }

        private void ShowPanel(Grid view, Duration duration)
        {
            Storyboard sb = new Storyboard();

            ThicknessAnimation toMargin = new ThicknessAnimation();
            toMargin.Duration = duration;
            toMargin.From = new Thickness(0, 40, 0, 0);
            toMargin.To = new Thickness(0, 0, 0, 0);
            Storyboard.SetTargetProperty(toMargin, new PropertyPath("Margin"));

            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 0;
            toOpacity.To = 1;
            Storyboard.SetTargetProperty(toOpacity, new PropertyPath("Opacity"));

            if (view.Margin.Top != toMargin.To.Value.Top)
                sb.Children.Add(toMargin);
            if (view.Opacity != toOpacity.To.Value)
                sb.Children.Add(toOpacity);

            view.BeginStoryboard(sb);
        }

        private void HidePanel(Grid view, Duration duration)
        {
            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 1;
            toOpacity.To = 0;

            if (view.Opacity != toOpacity.To.Value)
                view.BeginAnimation(Grid.OpacityProperty, toOpacity);
        }
    }
}
