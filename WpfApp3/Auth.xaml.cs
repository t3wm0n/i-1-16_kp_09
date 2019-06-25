using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace WpfApp3
{
    public partial class Auth : Window
    {
        TravelAgencyEntities tae = DB.tae;
        SqlConnection connect = DB.connect;       
        public Auth()
        {
            InitializeComponent();
            var order = tae.Order.Where(or => or.Promocode.Length > 0);
            foreach (Order ord in order)
                DB.NewOrders.Add(ord.ID_Order);
        }

        private void EnterB_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("", connect)
            {
                CommandText = "SELECT * FROM [User] JOIN [Role] on [User].[Role_ID] = [Role].[ID_Role]" +
                " WHERE Email = '" + Log.Text + "' AND " + DB.decrypt + "UserPassword)) = '" + Pass.Password + "'"
            };
            try
            {
                connect.Open();
                if (command.ExecuteScalar() != null)
                {
                    DataTable table = new DataTable();
                    DB.zap(command.CommandText, table);
                    DB.login = true;
                    DB.Log = Log.Text;
                    DB.Add_Source = Convert.ToInt32(table.Rows[0]["Add_Source"]);
                    DB.Add_Data = Convert.ToInt32(table.Rows[0]["Add_Data"]);
                    DB.Delete_Data = Convert.ToInt32(table.Rows[0]["Delete_Data"]);
                    DB.Update_Data = Convert.ToInt32(table.Rows[0]["Update_Data"]);
                    DB.Create_Report = Convert.ToInt32(table.Rows[0]["Create_Report"]);
                    switch (table.Rows[0]["Role_Name"].ToString())
                    {
                        case ("Admin"):
                            DB.RoleName = "Администратор";
                            break;
                        case ("Guest"):
                            DB.RoleName = "Гость";
                            break;
                        case ("Acc"):
                            DB.RoleName = "Бухгалтер";
                            break;
                        case ("Operator"):
                            DB.RoleName = "Оператор";
                            break;
                    }
                    
                    connect.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Введены не верный логин и пароль!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void AdminB_Click(object sender, RoutedEventArgs e)
        {
            DB.login = true;
            DB.Log = "S.U.";
            DB.RoleName = "Супер-пользователь";
            DB.Add_Source = 1;
            DB.Add_Data = 1;
            DB.Delete_Data = 1;
            DB.Update_Data = 1;
            DB.Create_Report = 1;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void RegistrationB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text != "" && Password.Password != "" && Name.Text != "" && SurName.Text != "" && MidName.Text != "")
                {
                    DB.SendMail(Login.Text, DB.regMail, DB.regBodyMail);
                    tae.Add_User(Login.Text, Password.Password, Name.Text, SurName.Text, MidName.Text, 2);
                }
                else
                    MessageBox.Show("Заполните все поля!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данный Email уже зарегистрирован!");
            }
            finally
            {
                TC.SelectedItem = TB1;
            }
        }

        private void LostPassword_Click(object sender, RoutedEventArgs e)
        {
            RemindPassword remind = new RemindPassword();
            remind.ShowDialog();
        }
    }
}
