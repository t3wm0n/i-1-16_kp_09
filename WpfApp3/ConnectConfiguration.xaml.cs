using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для ConnectConfiguration.xaml
    /// </summary>
    public partial class ConnectConfiguration : UserControl
    {
        static SqlConnection connect = DB.connect;
        public ConnectConfiguration()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchAsync(ServersList,SourceList);
            //Progress(SearchProgress);
            SourceList.IsEnabled = true;
            ServersList.IsEnabled = true;
        }

        delegate void Serv();
        static DataTable table;
        static DataTable dt;
        public static async void SearchAsync(ComboBox Servers, ComboBox Sources)
        {
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand("select name from sys.databases " +
                "where name not in ('master','tempdb','model','msdb')", connect);
                table = new DataTable();
                dt = new DataTable();
                if (connect.State != ConnectionState.Open)
                    connect.Open();
                table.Load(command.ExecuteReader());
                foreach (DataRow row in table.Rows)
                    Servers.Dispatcher.BeginInvoke(new Serv(() => Servers.Items.Add(row[0])));
                DataTable inst = SqlDataSourceEnumerator.Instance.GetDataSources();
                dt = inst;
                connect.Close();
                foreach (DataRow row in dt.Rows)
                    Sources.Dispatcher.BeginInvoke(new Serv(() => Sources.Items.Add(row[0])));
            });            
        }

        public static async void Progress(ProgressBar SearchProgress)
        {
            try
            {
                await Task.Run(() =>
                {
                    Action action = () => { SearchProgress.Value++; };
                    var task = new Task(() =>
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            SearchProgress.Dispatcher.BeginInvoke(action);
                            Thread.Sleep(100);
                        }
                    });
                    task.Start();
                });
            }
            catch
            {

            }
        }

        private void AcceptConnection_Click(object sender, RoutedEventArgs e)
        {
            if (SourceList.SelectedIndex != -1)
                DB.datasource = SourceList.SelectedItem.ToString();
            if (ServersList.SelectedIndex != -1)
                DB.catalog = ServersList.SelectedItem.ToString();
            if (UserDB.Text != "")
                DB.userid = UserDB.Text;
            if (PasswordDB.Text != "")
                DB.password = PasswordDB.Text;
        }
    }
}
