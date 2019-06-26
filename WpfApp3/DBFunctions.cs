using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3
{
    class DBFunctions
    {
        public static SqlConnection connect = DB.connect;
        public static void FindService(string Type, string Sign, DataGrid DG, string columns, string tablename)
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("SELECT " + columns + " FROM " + tablename + " WHERE " + Type + " = " + "'" + Sign + "'", connect);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt = ((DataView)DG.ItemsSource).ToTable();
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            DataTable dt2 = new DataTable();
            dt2.Load(reader);
            dt2.PrimaryKey = new DataColumn[] { dt2.Columns[0] };
            foreach (DataRow row2 in dt2.Rows)
                foreach (DataRow row in dt.Rows)
                    if (row.ItemArray[0].ToString().Equals(row2.ItemArray[0].ToString()))
                        DG.SelectedIndex = Convert.ToInt32(row.ItemArray[0].ToString()) - 1;
            reader.Close();
            connect.Close();
        }


        public static void ServiceFilter(string Type, string Sign, DataGrid DG, string columns, string tablename)
        {
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("SELECT " + columns + " FROM " + tablename + " WHERE " + Type + " = " + "'" + Sign + "'", connect);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt.DefaultView.Count > 0)
                    DG.ItemsSource = dt.DefaultView;
                reader.Close();
                connect.Close();
            }
            catch { };
        }
    }
}
