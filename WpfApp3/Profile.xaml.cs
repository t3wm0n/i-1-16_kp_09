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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        Window window = Application.Current.MainWindow;
        DecryptUser user = DB.tae.DecryptUser.FirstOrDefault(u => u.Email == DB.Log);
        public Profile()
        {
            InitializeComponent();
            if (user != null)
            {
                Name.Text = user.Имя;
                SurName.Text = user.Фамилия;
                MidName.Text = user.Отчество;
            }
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (OldPass.Text == user.Пароль)
                if (NewPass.Text == AcceptNewPass.Text)
                {
                    DB.tae.Update_User(user.ID, null, NewPass.Text, null, null, null, null);
                    DB.tae.Entry(user).Reload();
                    OldPass.Text = "";
                    NewPass.Text = "";
                    AcceptNewPass.Text = "";
                }
                else
                    MessageBox.Show("Пароли не совпадают.");
            else
                MessageBox.Show("Введен не верный пароль!");
        }

        private void DelAcc_Click(object sender, RoutedEventArgs e)
        {
            if (AcceptDel.IsChecked == true)
                DB.tae.Delete_User(user.ID);
            Auth auth = new Auth();
            auth.Show();
            window.Close();
            DB.login = false;
        }
    }
}
