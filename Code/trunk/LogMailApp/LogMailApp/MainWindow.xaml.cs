﻿#if DEBUG
using System.Diagnostics;
#endif

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LogMailApp.Preference;

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

            VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
            VM.VMEditorPanel newPanel = mainWindow.NewPanelDataContext;

            UIElementCollection uiDateItem = this.DatePicker.Children;

            // 初始化日期指针字典
            TextBlock tbk = null;
            for (int i = 0; i < uiDateItem.Count; i++)
            {
                tbk = uiDateItem[i] as TextBlock;
                datePointerDic.Add(tbk.Tag as string, tbk);

                if (i == 0)
                {
                    this.TextBlock_Date_PreviewMouseLeftButtonDown(tbk, null);
                }
            }

            #region 窗口启动动画
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
            #endregion

            Button btnStart = null;

            if (UserDefault.Instance.IsFirstUsing)
                btnStart = this.btnSetting;
            else
                btnStart = this.btnNew;

            this.TabButton_PreviewMouseLeftButtonDown(btnStart, null);
        }

        private readonly Duration durationGlobal = new Duration(TimeSpan.FromMilliseconds(200));
        private int lastTag = -1;    // Tab 按钮的标记
        private Dictionary<string, TextBlock> datePointerDic = new Dictionary<string, TextBlock>();
        private bool hasChange = false;
        private string lastContent = string.Empty;

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveData();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UserDefault userDefault = UserDefault.Instance;
            if (userDefault.IsFirstUsing)
            {
#if !DEBUG_FirstUsing
                userDefault.IsFirstUsing = false;
#endif
            }

            App.Current.Shutdown(0);
        }

        private void TabButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            int tag = Convert.ToInt16(btn.Tag);
            if (tag != lastTag)
            {
                lastTag = tag;

                ThicknessAnimation toMove = new ThicknessAnimation();
                toMove.To = new Thickness(0, tag, 0, 0);
                toMove.Duration = durationGlobal;

                this.FocusBackgroundPanel.BeginAnimation(Border.MarginProperty, toMove);

                if (tag == 0)
                {
                    if (!this.txtLogContent.IsFocused)
                        this.ShowButton(this.btnDelete, this.durationGlobal);

                    this.HidePanel(this.SettingPanel, this.durationGlobal);
                    this.HidePanel(this.AboutPanel, this.durationGlobal);
                    this.ShowPanel(this.EditorPanel, this.durationGlobal);
                }
                else if (tag == 40)
                {
                    this.HideButton(this.btnDelete, this.durationGlobal);

                    this.HidePanel(this.EditorPanel, this.durationGlobal);
                    this.HidePanel(this.AboutPanel, this.durationGlobal);
                    this.ShowPanel(this.SettingPanel, this.durationGlobal);
                }
                else if (tag == 80)
                {
                    this.HideButton(this.btnDelete, this.durationGlobal);

                    this.HidePanel(this.EditorPanel, this.durationGlobal);
                    this.HidePanel(this.SettingPanel, this.durationGlobal);
                    this.ShowPanel(this.AboutPanel, this.durationGlobal);
                }
            }
        }

        private void TextBlock_Date_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;

            for (var i = 0; i < this.DatePicker.Children.Count; i++)
            {
                TextBlock t = this.DatePicker.Children[i] as TextBlock;

                if (t == txt)
                {
                    Storyboard sb = new Storyboard();

                    ThicknessAnimation toMargin = new ThicknessAnimation();
                    toMargin.Duration = this.durationGlobal;
                    toMargin.To = new Thickness(0, 20, 0, 20);
                    //toMargin.FillBehavior = FillBehavior.Stop;
                    Storyboard.SetTargetProperty(toMargin, new PropertyPath("Margin"));

                    DoubleAnimation toFontSize = new DoubleAnimation();
                    toFontSize.Duration = this.durationGlobal;
                    toFontSize.To = 22;
                    //toFontSize.FillBehavior = FillBehavior.Stop;
                    Storyboard.SetTargetProperty(toFontSize, new PropertyPath("FontSize"));

                    sb.Children.Add(toMargin);
                    sb.Children.Add(toFontSize);

                    t.BeginStoryboard(sb);
                }
                else
                {
                    t.BeginAnimation(TextBlock.MarginProperty, null);
                    t.BeginAnimation(TextBlock.FontSizeProperty, null);
                }
            }

            DateTime selectedDate = Convert.ToDateTime(txt.Tag);

            // 先保存当前日志内容
            this.SaveData();
            // 再加载另外一天的日志内容
            this.NextData(selectedDate);
        }

        // 鼠标滑轮翻页日志
        //int wheelCount = 0; // 滑轮太灵敏
        private void EditorPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //wheelCount++;
#if DEBUG
            Debug.WriteLine("Wheel: " + e.Delta);
            // Down -120
            // Up   120
#endif
            if (!this.txtLogContent.IsFocused/* && wheelCount == 3*/)
            {
                //wheelCount = 0;
                int delta = e.Delta / 120;

                VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
                VM.VMEditorPanel newPanel = mainWindow.NewPanelDataContext;

                DateTime? shouldDate = newPanel.SelectedDate.Value.AddDays(delta);

                int shouldDay = shouldDate.Value.Day;
                int maxDay = newPanel.LogDate.Value.Day;
                int minDay = newPanel.LogDate.Value.AddDays(-7).Day;

                if (shouldDay > minDay && shouldDay <= maxDay)
                {
                    string key = shouldDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    TextBlock dateItem = this.datePointerDic[key];

                    this.TextBlock_Date_PreviewMouseLeftButtonDown(dateItem, null);

                    Storyboard sb = new Storyboard();
                    Duration myDuration = new Duration(TimeSpan.FromMilliseconds(400));

                    ThicknessAnimation toMargin = new ThicknessAnimation();
                    toMargin.Duration = myDuration;
                    toMargin.FillBehavior = FillBehavior.Stop;
                    Storyboard.SetTargetProperty(toMargin, new PropertyPath("Margin"));

                    DoubleAnimation toOpacity = new DoubleAnimation();
                    toOpacity.Duration = myDuration;
                    toOpacity.FillBehavior = FillBehavior.Stop;
                    toOpacity.From = 0;
                    toOpacity.To = 1;
                    Storyboard.SetTargetProperty(toOpacity, new PropertyPath("Opacity"));

                    if (delta < 0)
                    {
                        toMargin.From = new Thickness(0, -35, 0, 0);
                        toMargin.To = new Thickness(0, 0, 0, 0);
                    }
                    else if (delta > 0)
                    {
                        toMargin.From = new Thickness(0, 35, 0, 0);
                        toMargin.To = new Thickness(0, 0, 0, 0);
                    }

                    sb.Children.Add(toMargin);
                    sb.Children.Add(toOpacity);

                    this.gdContent.BeginStoryboard(sb);
                }
            }
        }

        private void TextBox_LogContent_GotFocus(object sender, RoutedEventArgs e)
        {
            this.lastContent = (sender as TextBox).Text;

            this.ShowButton(this.btnSave, this.durationGlobal);
            this.ShowButton(this.btnBack, this.durationGlobal);
            this.HideButton(this.btnDelete, this.durationGlobal);
        }

        private void TextBox_LogContent_LostFocus(object sender, RoutedEventArgs e)
        {
            this.hasChange = !this.lastContent.Equals((sender as TextBox).Text);

            this.ShowButton(this.btnDelete, this.durationGlobal);
            this.HideButton(this.btnSave, this.durationGlobal);
            this.HideButton(this.btnBack, this.durationGlobal);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (e.Key == Key.S)
                {
#if DEBUG_UI
                Debug.WriteLine("Save Action, Now!");
#endif
                    this.SaveData();

                }
                else if (e.Key == Key.D)
                {
#if DEBUG_UI
                Debug.WriteLine("Delete Action, Now!");
#endif
                    this.DeleteData();
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                this.txtLogContent.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                this.Image_Close_PreviewMouseLeftButtonDown(null, null);
            }
        }

        private void TextBox_Setting_GotFocus(object sender, RoutedEventArgs e)
        {
            // 还没想好写什么, 先放着
        }

        private void TextBox_Setting_LostFocus(object sender, RoutedEventArgs e)
        {
            // 还没想好写什么, 先放着
        }

        private void btnDelete_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DeleteData();
        }

        private void btnSave_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SaveData(true);
        }

        private void btnBack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
            try
            {
                if (mainWindow.NewPanelDataContext.Back.CanExecute(null))
                {
                    mainWindow.NewPanelDataContext.Back.Execute(null);
                }
            }
            catch (System.Exception ex)
            {
                Window win = new WarningWindow(ex.Message);
                win.ShowDialog();
            }
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
            view.Visibility = System.Windows.Visibility.Collapsed;

            // 撤销动画效果
            view.BeginAnimation(Grid.MarginProperty, null);
            view.BeginAnimation(Grid.OpacityProperty, null);
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

        #region 执行数据存/取/删命令
        /// <summary>
        /// 依赖于SelectedDate值的保存动作
        /// </summary>
        private void SaveData(bool isUserClick = false)
        {
            this.txtLogContent.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            // 不能Undo表示没有修改过内容
            if (hasChange || isUserClick)
            {
                // 保存数据吧
                VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
                try
                {
                    if (mainWindow.NewPanelDataContext.Save.CanExecute(null))
                    {
                        mainWindow.NewPanelDataContext.Save.Execute(null);
                    }
                }
                catch (System.Exception ex)
                {
                    Window win = new WarningWindow(ex.Message);
                    win.ShowDialog();
                }

                hasChange = !hasChange;
            }
        }

        private void NextData(DateTime? date)
        {
            VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
            try
            {
                if (mainWindow.NewPanelDataContext.Load.CanExecute(null))
                {
                    mainWindow.NewPanelDataContext.SelectedDate = date;
                    mainWindow.NewPanelDataContext.Load.Execute(null);
                }
            }
            catch (System.Exception ex)
            {
                Window win = new WarningWindow(ex.Message);
                win.ShowDialog();
            }
#if DEBUG_UI
            mainWindow.NewPanelDataContext.SelectedDate = date;
#endif
        }

        private void DeleteData()
        {
            this.txtLogContent.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            VM.VMMainWindow mainWindow = this.DataContext as VM.VMMainWindow;
            try
            {
                if (mainWindow.NewPanelDataContext.Delete.CanExecute(null))
                {
                    mainWindow.NewPanelDataContext.Delete.Execute(null);
                }
            }
            catch (System.Exception ex)
            {
                Window win = new WarningWindow(ex.Message);
                win.ShowDialog();
            }
        }
        #endregion
    }
}
