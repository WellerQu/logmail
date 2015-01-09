//#define DEBUG_FirstUsing

#if DEBUG
using System.Diagnostics;
#endif

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
            this.EditorPanel.Visibility = this.SettingPanel.Visibility = System.Windows.Visibility.Collapsed;

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
            Button btnStart = null;

            if (onlySetting)
                btnStart = this.btnSetting;
            else
                btnStart = this.btnNew;

            this.FunctionButton_PreviewMouseLeftButtonDown(btnStart, null);
        }

        private readonly Duration durationGlobal = new Duration(TimeSpan.FromMilliseconds(300));

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
            this.SaveData();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UserDefault userDefault = UserDefault.Instance;
            if (userDefault.IsFirstUsing)
            {
#if DEBUG_FirstUsing
                userDefault.IsFirstUsing = false;
#endif
            }

            App.Current.Shutdown(0);
        }

        private void FunctionButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            int tag = Convert.ToInt16(btn.Tag);

            ThicknessAnimation toMove = new ThicknessAnimation();
            toMove.To = new Thickness(0, tag, 0, 0);
            toMove.Duration = durationGlobal;

            this.FocusBackgroundPanel.BeginAnimation(Border.MarginProperty, toMove);

            if (tag == 0)
            {
                this.HidePanel(this.SettingPanel, this.durationGlobal);
                this.ShowPanel(this.EditorPanel, this.durationGlobal);
            }
            else if (tag == 40)
            {
                this.HidePanel(this.EditorPanel, this.durationGlobal);
                this.ShowPanel(this.SettingPanel, this.durationGlobal);
            }
            else if (tag == 80)
            {
            }
        }

        private void TextBlock_Date_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = this.durationGlobal;

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

            this.SaveData();
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

        private void TextBox_LogContent_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ShowButton(this.btnSave, this.durationGlobal);
            this.HideButton(this.btnDelete, this.durationGlobal);
        }

        private void TextBox_LogContent_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ShowButton(this.btnDelete, this.durationGlobal);
            this.HideButton(this.btnSave, this.durationGlobal);
        }

        #region 控制空间显示/隐藏的方法, 附带动画效果
        private void ShowPanel(Grid view, Duration duration)
        {
            view.Visibility = System.Windows.Visibility.Visible;

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

            sb.Children.Add(toMargin);
            sb.Children.Add(toOpacity);

            view.BeginStoryboard(sb);
        }

        private void HidePanel(Grid view, Duration duration)
        {
            view.Margin = new Thickness(0, 40, 0, 0);
            view.Opacity = 0;
            view.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ShowButton(Button button, Duration duration)
        {
            button.Visibility = System.Windows.Visibility.Visible;

            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 0;
            toOpacity.To = 1;

            button.BeginAnimation(Button.OpacityProperty, toOpacity);
        }

        private void HideButton(Button button, Duration duration)
        {
            DoubleAnimation toOpacity = new DoubleAnimation();
            toOpacity.Duration = duration;
            toOpacity.From = 1;
            toOpacity.To = 0;
            toOpacity.Completed += (object sender, EventArgs e) =>
            {
                button.Visibility = System.Windows.Visibility.Collapsed;
            };

            button.BeginAnimation(Button.OpacityProperty, toOpacity);
        }
        #endregion

        #region 执行保存动作
        private void SaveData()
        {
            if (this.txtLogContent.CanUndo)
            {
                // 保存数据吧
            }

            this.btnSave.Focus();
        }
        #endregion
    }
}
