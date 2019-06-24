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
        SqlConnection connect = DB.connect;
        TravelAgencyEntities tae = DB.tae;
        public UC_Controller()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    connect.Open();
            //    SqlCommand command = new SqlCommand("select ID_Tovar,Name,Model,Cost from [Tovar]", connect);
            //    DataTable datab1 = new DataTable();
            //    datab1.Load(command.ExecuteReader());
            //    DG_Basic.ItemsSource = datab1.DefaultView;
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    connect.Close();
            //}
            switch (DB.tableName)
            {
                case "Air_Ticket":
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
                case "Financial_Report":
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
                case "Payment_Method":
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

        private void DG_Basic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DG_Basic.SelectedItem.ToString() != "{NewItemPlaceholder}")
            //{
            //    DataRowView row = (DataRowView)DG_Basic.SelectedItem;
                
            //}
        }     

        private void FindService(string Type, string Sign)
        {
            //connect.Open();
            //SqlCommand cmd = new SqlCommand("select ID_Tovar,Name,Model,Cost from [Tovar] where " + Type + " = " + "'" + Sign + "'", connect);
            //SqlDataReader reader = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt = ((DataView)DG_Basic.ItemsSource).ToTable();
            //dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            //DataTable dt2 = new DataTable();
            //dt2.Load(reader);
            //dt2.PrimaryKey = new DataColumn[] { dt2.Columns[0] };
            //foreach (DataRow row2 in dt2.Rows)
            //        foreach (DataRow row in dt.Rows)
            //            if (row.ItemArray[0].ToString().Equals(row2.ItemArray[0].ToString()))
            //                    DG_Basic.SelectedIndex = Convert.ToInt32(row.ItemArray[0].ToString()) - 1;
            //reader.Close();
            //connect.Close();
        }
    

        private void ServiceFilter(string Type,string Sign)
        {
            //try
            //{
            //    connect.Open();
            //    SqlCommand cmd = new SqlCommand("select ID_Tovar,Name,Model,Cost from [Tovar] where " + Type + " = " + "'" + Sign + "'", connect);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    DataTable dt = new DataTable();
            //    dt.Load(reader);
            //    if (dt.DefaultView.Count > 0)
            //        DG_Basic.ItemsSource = dt.DefaultView;
            //    reader.Close();
            //    connect.Close();
            //}
            //catch
            //{

            //};
        }

        private void FindB_Click(object sender, RoutedEventArgs e)
        {
            //FindService(CmbBox.Text, TxtBox.Text);
        }

        private void FilterB_Click(object sender, RoutedEventArgs e)
        {
            //Window_Loaded(sender, e);
            //ServiceFilter(CmbBox.Text, TxtBox.Text);
        }
    }
}