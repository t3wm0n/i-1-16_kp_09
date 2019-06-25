using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp3
{
    public partial class Views : Window
    {
        static string item;
        TravelAgencyEntities tae = DB.tae;
        SqlDependency sql = new SqlDependency();

        public Views()
        {
            InitializeComponent();
            UpdTable.IsEnabled = DB.Add_Data == 1 ? true : false;
            CBI1.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI2.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI3.IsEnabled = DB.Add_Data == 1 ? true : false;
            CBI4.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI5.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI6.IsEnabled = DB.Create_Report == 1 ? true : false;
            CBI7.IsEnabled = DB.Add_Data == 1 ? true : false;
            CBI8.IsEnabled = DB.Add_Data == 1 ? true : false;
            CBI9.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI10.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI11.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI12.IsEnabled = DB.Add_Source == 1 ? true : false;
            CBI13.IsEnabled = DB.Add_Data == 1 ? true : false;
            CBI14.IsEnabled = DB.Add_Data == 1 ? true : false;
            SqlDependency.Start(DB.connect.ConnectionString);
        }

        private void CB_Views_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            switch (CB_Views.SelectedIndex)
            {
                case 0:
                    DB.dataAirport();
                    DG.ItemsSource = DB.vrAirport.DefaultView;
                    AddEventChanges(DB.qrAirport);
                    break;
                case 1:
                    DB.dataCity();
                    DG.ItemsSource = DB.vrCity.DefaultView;
                    AddEventChanges(DB.qrCity);
                    break;
                case 2:
                    DB.dataClient();
                    DG.ItemsSource = DB.vrClient.DefaultView;
                    AddEventChanges(DB.qrEncryptClient);
                    break;
                case 3:
                    DB.dataCompany();
                    DG.ItemsSource = DB.vrCompany.DefaultView;
                    AddEventChanges(DB.qrCompany);
                    break;
                case 4:
                    DB.dataCountry();
                    DG.ItemsSource = DB.vrCountry.DefaultView;
                    AddEventChanges(DB.qrCountry);
                    break;
                case 5:
                    DB.dataReport();
                    DG.ItemsSource = DB.vrReport.DefaultView;
                    AddEventChanges(DB.qrReport);
                    break;
                case 6:
                    DB.dataHotel();
                    DG.ItemsSource = DB.vrHotel.DefaultView;
                    AddEventChanges(DB.qrHotel);
                    break;
                case 7:
                    DB.dataOrder();
                    DG.ItemsSource = DB.vrOrder.DefaultView;
                    AddEventChanges(DB.qrOrder);
                    break;
                case 8:
                    DB.dataPayment();
                    DG.ItemsSource = DB.vrPayment.DefaultView;
                    AddEventChanges(DB.qrPayment);
                    break;
                case 9:
                    DB.dataRole();
                    DG.ItemsSource = DB.vrRole.DefaultView;
                    AddEventChanges(DB.qrRole);
                    break;
                case 10:
                    DB.dataTrip();
                    DG.ItemsSource = DB.vrTrip.DefaultView;
                    AddEventChanges(DB.qrTrip);
                    break;
                case 11:
                    DB.dataUser();
                    DG.ItemsSource = DB.vrUser.DefaultView;
                    AddEventChanges(DB.qrEncryptUser);
                    break;
                case 12:
                    DB.dataVoucher();
                    DG.ItemsSource = DB.vrVoucher.DefaultView;
                    AddEventChanges(DB.qrVoucher);
                    break;
                case 13:
                    DB.dataAirTicket();
                    DG.ItemsSource = DB.vrAir_Ticket.DefaultView;
                    AddEventChanges(DB.qrAir_Ticket);
                    break;
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            UC_Controller main = new UC_Controller();
            main.ShowDialog();
            main.Owner = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SqlDependency.Stop(DB.connect.ConnectionString);
        }

        public void AddEventChanges(string command)
        {
            item = command;
            SqlConnection connection = new SqlConnection(DB.connect.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(item, connection);
            SqlDependency dependency = new SqlDependency(cmd);
            dependency.OnChange += new OnChangeEventHandler(OnChange);
            cmd.ExecuteNonQuery();
        }

        public void OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
            {
                //MessageBox.Show("Загрузка таблицы, пожалуйста подождите!");
                int dtname = DB.dT.FindIndex(n => n.TableName == DB.tableName);
                DB.zap(item,DB.dT[dtname]);
                this.Dispatcher.Invoke(() =>
                {
                    DG.Items.Refresh();
                });
                AddEventChanges(item);
            }
        }
    }
}
