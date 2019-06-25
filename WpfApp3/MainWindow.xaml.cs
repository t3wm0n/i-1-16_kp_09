using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.Current.MainWindow = this;
            InitializeComponent();
            if (DB.login)
                LoginLbl.Content += DB.Log;
            LoginRole.Content += DB.RoleName;
            if (DB.Add_Source == 1)
                ConfConnect.Visibility = Visibility.Visible;
            if (DB.Create_Report == 0)
            {
                FinIcon.Fill = Brushes.Gray;
                FinReport.IsEnabled = false;
            }
            if (DB.Add_Data == 0)
            {
                AddOrderIcon.Fill = Brushes.Gray;
                AddOrder.IsEnabled = false;
            }
            if (DB.Update_Data == 0)
            {
                TableEditIcon.Fill = Brushes.Gray;
                TableEdit.IsEnabled = false;
            }

            if (DB.NewOrders != null & DB.Add_Data != 0)
                if (DB.NewOrders.Count > 0)
                {
                    NewOrders.Visibility = Visibility.Visible;
                    DoubleAnimation buttonAnimation = new DoubleAnimation();
                    buttonAnimation.From = NewOrders.Opacity;
                    buttonAnimation.To = 0;
                    buttonAnimation.Duration = TimeSpan.FromSeconds(4);
                    buttonAnimation.AutoReverse = true;
                    buttonAnimation.RepeatBehavior = new RepeatBehavior(5);
                    NewOrders.BeginAnimation(Button.OpacityProperty, buttonAnimation);
                    //DoubleAnimation buttonAnimation = new DoubleAnimation();
                    //buttonAnimation.From = NewOrders.ActualWidth;
                    //buttonAnimation.To = 450;
                    //buttonAnimation.Duration = TimeSpan.FromSeconds(3);
                    //buttonAnimation.AutoReverse = true;
                    //buttonAnimation.RepeatBehavior = new RepeatBehavior(5);
                    //NewOrders.BeginAnimation(Button.WidthProperty, buttonAnimation);
                }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new Orders());
        }

        private void TableEdit_Click(object sender, RoutedEventArgs e)
        {
            Views view = new Views();
            view.ShowDialog();
        }

        private void Unlogin_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            Close();
            DB.login = false;
        }

        private void NewOrders_Click(object sender, RoutedEventArgs e)
        {
            AddOrder_Click(sender, e);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new Profile());
        }

        private void ConfConnect_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new ConnectConfiguration());
        }

        private void FinReport_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new Reports());
        }
    }
}
