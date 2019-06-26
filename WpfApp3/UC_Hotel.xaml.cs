using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для UC_Hotel.xaml
    /// </summary>
    public partial class UC_Hotel : UserControl
    {
        public string cols = "";
        public UC_Hotel()
        {
            InitializeComponent();
            DG.ItemsSource = DB.dataTable.DefaultView;
            foreach (DataColumn column in DB.dataTable.Columns)
            {
                if (column.Ordinal + 1 != DB.dataTable.Columns.Count)
                    cols += column.ToString() + ", ";
                else
                    cols += column.ToString();
                ColumnsList.Items.Add(column.ToString());
            }
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            DBFunctions.FindService(ColumnsList.Text, TBSearchFilter.Text, DG, cols, DB.tableName);
        }

        private void FilterB_Click(object sender, RoutedEventArgs e)
        {
            DBFunctions.ServiceFilter(ColumnsList.Text, TBSearchFilter.Text, DG, cols, DB.tableName);
        }

        private void InsertB_Click(object sender, RoutedEventArgs e)
        {
            DB.tae.Add_Hotel(Column2.Text, Convert.ToInt32(Column3.Text), Convert.ToInt32(Column4.Text), Column5.Text, Convert.ToInt32(Column6.Text));
        }

        private void UpdateB_Click(object sender, RoutedEventArgs e)
        {
            DB.tae.Update_Hotel(Convert.ToInt32(Column1.Text), Column2.Text, Convert.ToInt32(Column3.Text), Convert.ToInt32(Column4.Text), Column5.Text, Convert.ToInt32(Column6.Text));
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            DB.tae.Delete_Hotel(Convert.ToInt32(Column1.Text));
        }
    }
}
