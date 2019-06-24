using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        TravelAgencyEntities tae = DB.tae;
        int NO = DB.NewOrders[0];
        public Orders()
        {
            InitializeComponent();
            FullOrder order = tae.FullOrder.FirstOrDefault(p => p.ID_Order == NO);
            ClientName.Text = order.Name;
            ClientSurName.Text = order.SurName;
            ClientMidName.Text = order.MidName;
            Gender.SelectedIndex = 0;
            Passport_Ser.Text = order.Passport_Ser;
            Passport_Num.Text = order.Passport_Num;
            IntPassport_Ser.Text = order.IntPassport_Ser;
            IntPassport_Num.Text = order.IntPassport_Num;
            Date_Passport.Text = order.Date_Passport.ToString();
            Phone_Number.Text = order.Phone_Number;
            Voucher_Num.Text = order.Voucher_Num;
            Hotel.Text = order.Hotel_Name;
            Company.Text = order.Company_Name;
            Address.Text = order.Address;
            City.Text = order.City;
            Country.Text = order.Country;
            Cost.Text = order.Cost.ToString();
            Cost_Ticket.Text = order.Cost_Ticket.ToString();
            Dates.Text = order.Dates.ToString();
            Num_Trip.Text = order.Num_Trip.ToString();
            ID_Order.Text = order.ID_Order.ToString();
            Order_Cost.Text = order.Order_Cost.ToString();
            Payment.Text = order.Payment_Method;
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@nomer", NO);
            var toDG = tae.FullOrder.SqlQuery("SELECT * FROM FullOrder WHERE ID_Order = @nomer", param);
            DG.ItemsSource = toDG.ToList();
        }

        private void Passport_Num_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void AcceptOrder_Click(object sender, RoutedEventArgs e)
        {
            //Order ord = tae.Order.Find(NO);
            //ord.Promocode = "";
            //tae.SaveChanges();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
