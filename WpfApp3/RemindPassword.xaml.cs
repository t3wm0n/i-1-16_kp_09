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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для RemindPassword.xaml
    /// </summary>
    public partial class RemindPassword : Window
    {
        TravelAgencyEntities tae = DB.tae;
        public RemindPassword()
        {
            InitializeComponent();
        }

        private string getPassword(string Email)
        {
            string password;
            DecryptUser user = tae.DecryptUser.FirstOrDefault(u => u.Email == Email);
            password = user.Пароль;
            if (user == null)
                password = "";
            return user.Пароль;
        }

        private void Remind_Click(object sender, RoutedEventArgs e)
        {
            DB.SendMail(Email.Text, DB.remind, DB.remindBody + getPassword(Email.Text));
            this.Close();
        }
    }
}
