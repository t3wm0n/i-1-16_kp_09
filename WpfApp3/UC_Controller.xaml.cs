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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ComponentModel;

namespace WpfApp3
{
    public partial class UC_Controller : Window
    {
        public UC_Controller()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (DB.tableName)
            {
                case "Ticket":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Ticket());
                    break;
                case "Airport":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Airport());
                    break;
                case "City":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_City());
                    break;
                case "Client":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Client());
                    break;
                case "Company":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Company());
                    break;
                case "Country":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Country());
                    break;
                case "Report":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Report());
                    break;
                case "Hotel":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Hotel());
                    break;
                case "Order":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Order());
                    break;
                case "Payment":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Payment());
                    break;
                case "Role":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Role());
                    break;
                case "Trip":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Trip());
                    break;
                case "User":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_User());
                    break;
                case "Voucher":
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UC_Voucher());
                    break;
            }
        }     
    }
}