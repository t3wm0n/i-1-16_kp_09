﻿using System;
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
    /// Логика взаимодействия для UC_Ticket.xaml
    /// </summary>
    public partial class UC_Ticket : UserControl
    {
        public string cols = "";
        public UC_Ticket()
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
            DB.tae.Add_Ticket(Convert.ToInt32(Column2.Text), DateTime.Parse(Column3.Text), Convert.ToInt32(Column4.Text), Convert.ToInt32(Column5.Text));
        }

        private void UpdateB_Click(object sender, RoutedEventArgs e)
        {
            DB.tae.Update_Ticket(Convert.ToInt32(Column1.Text), Convert.ToInt32(Column2.Text), DateTime.Parse(Column3.Text), Convert.ToInt32(Column4.Text), Convert.ToInt32(Column5.Text));
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            DB.tae.Delete_Ticket(Convert.ToInt32(Column1.Text));
        }
    }
}
