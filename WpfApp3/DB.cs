using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace WpfApp3
{
    class DB
    {
        public static TravelAgencyEntities tae = new TravelAgencyEntities();
        public static string datasource = "LAPTOP-48LUISF8";
        public static string catalog = "TravelAgency";
        public static string userid = "t3wm0n";
        public static string password = "maxim1354";
        public static string docpath = AppDomain.CurrentDomain.BaseDirectory;
        public static SqlConnection connect = new SqlConnection("Data Source = " + datasource + "; Initial Catalog = " + catalog + "; Persist Security Info = true; User ID = " + userid + "; Password = " + password + ";");
        public static bool login = false;
        public static string Log;
        public static string tableName;
        public static string RoleName;
        public static int
            Add_Source = 0,
            Add_Data = 0,
            Delete_Data = 0,
            Update_Data = 0,
            Create_Report = 0;
        public static List<int> NewOrders = new List<int>();
        public const string decrypt = "CONVERT(VARCHAR(512),DecryptByPassphrase('ZOTOVKOSINOVAGLAZUNOV',";

        public static DataTable vrAir_Ticket = new DataTable("Air_Ticket");
        public static DataTable vrAirport = new DataTable("Airport");
        public static DataTable vrCity = new DataTable("City");
        public static DataTable vrClient = new DataTable("Client");
        public static DataTable vrCompany = new DataTable("Company");
        public static DataTable vrCountry = new DataTable("Country");
        public static DataTable vrReport = new DataTable("Report");
        public static DataTable vrHotel = new DataTable("Hotel");
        public static DataTable vrOrder = new DataTable("Order");
        public static DataTable vrPayment = new DataTable("Payment");
        public static DataTable vrRole = new DataTable("Role");
        public static DataTable vrTrip = new DataTable("Trip");
        public static DataTable vrUser = new DataTable("User");
        public static DataTable vrVoucher = new DataTable("Voucher");
        public static DataTable dataTable;

        public static List<DataTable> dT = new List<DataTable>()
        {
            vrAir_Ticket,
            vrAirport,
            vrCity,
            vrClient,
            vrCompany,
            vrCountry,
            vrHotel,
            vrOrder,
            vrPayment,
            vrReport,
            vrRole,
            vrTrip,
            vrUser,
            vrVoucher
        };

        public const string qrAir_Ticket = "SELECT at.ID_Air_Ticket, at.Cost_Ticket, at.Dates, a.Airport_Name, c.Имя, c.Фамилия, c.Отчество " +
            "FROM dbo.Air_Ticket AS at JOIN dbo.Airport AS a ON a.ID_Airport = at.Airport_ID JOIN dbo.DecryptClient AS c ON c.ID = at.Client_ID";
        public const string qrAirport = "SELECT Air.ID_Airport, Air.Airport_Name FROM dbo.Airport Air";
        public const string qrCity = "SELECT c.ID_City, c.City, c2.Country FROM dbo.City AS c JOIN dbo.Country AS c2 ON c2.ID_Country = c.Country_ID";
        public const string qrClient = "SELECT c.ID, c.Имя, c.Фамилия, c.Отчество, c.Пол, c.[Серия паспорта]," +
            "c.[Номер паспорта], c.[Дата выдачи], c.[Серия загранпаспорта], c.[Номер загранпаспорта],c.[Номер телефона] FROM dbo.DecryptClient AS c";
        public const string qrEncryptClient = "SELECT [ID_Client],[Name],[SurName],[MidName],[Gender],[Passport_Ser],[Passport_Num]," +
            "[Date_Passport],[IntPassport_Ser],[IntPassport_Num],[Phone_Number] FROM [dbo].[Client]";
        public const string qrCompany = "SELECT c.ID_Company, c.Company_Name, c.License FROM dbo.Company AS c";
        public const string qrCountry = "SELECT cy.ID_Country, cy.Country FROM dbo.Country AS cy";
        public const string qrReport = "SELECT fr.ID_Report, fr.Report_Date, fr.Costs, fr.Profit,od.Promocode, od.Order_Cost " +
            "FROM dbo.Financial_Report fr JOIN dbo.[Order] od ON fr.Order_ID = od.ID_Order";
        public const string qrHotel = "SELECT H.[ID_Hotel],H.[Hotel_Name],H.[Raiting],H.[Cost],H.[Address],C.City FROM dbo.Hotel H " +
            "JOIN dbo.City C ON H.City_ID = C.ID_City";
        public const string qrOrder = "SELECT O.[ID_Order],O.[Promocode],O.[Order_Cost],PM.Payment_Method,T.Name_Trip FROM dbo.[Order] O" +
            " JOIN dbo.Payment_Method PM ON O.Payment_ID = PM.ID_Payment JOIN dbo.Trip T ON O.Trip_ID = T.ID_Trip";
        public const string qrPayment = "SELECT pm.ID_Payment, pm.Payment_Method FROM dbo.Payment_Method AS pm";
        public const string qrRole = "SELECT r.ID_Role, r.Role_Name, r.Add_Source," +
            "r.Add_Data, r.Delete_Data, r.Update_Data, r.Create_Report FROM dbo.[Role] AS r";
        public const string qrTrip = "SELECT T.[ID_Trip], T.[Num_Trip], T.[Name_Trip], V.Voucher_Num, AT.Dates FROM dbo.Trip T " +
            "JOIN dbo.Voucher V ON T.Voucher_ID = V.ID_Voucher JOIN dbo.Air_Ticket AT ON T.Air_Ticket_ID = AT.ID_Air_Ticket";
        public const string qrUser = "SELECT u.ID, u.Email, u.Пароль, u.Имя, u.Фамилия, u.Отчество, u.Роль " +
            "FROM dbo.[DecryptUser] AS u";
        public const string qrEncryptUser = "SELECT u.ID_User, u.Email, u.UserPassword, u.UserName, u.UserSurName, u.UserMidName, " +
            "r.Role_Name FROM dbo.[User] AS u JOIN dbo.[Role] AS r ON r.ID_Role = u.Role_ID";
        public const string qrVoucher = "SELECT v.ID_Voucher, v.Voucher_Num,h.Hotel_Name, c.Company_Name " +
            "FROM dbo.Voucher AS v JOIN dbo.Hotel AS h ON h.ID_Hotel = v.Hotel_ID JOIN dbo.Company AS c ON c.ID_Company = v.Company_ID";

        public static void zap(string query, DataTable table) //Метод заполнения таблиц DataTable
        {
            try
            {
                if(connect.State != ConnectionState.Open)
                    connect.Open();
                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (table.DefaultView.Count > 0)
                    table.Clear();
                table.Load(rdr);
                tableName = table.TableName;
                rdr.Close();
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

        //Заполнение всех DataTable
        public static void dataAirTicket()
        {
            zap(qrAir_Ticket, dT[0]);
        }
        
        public static void dataAirport()
        {
            zap(qrAirport, dT[1]);
        }

        public static void dataCity()
        {
            zap(qrCity, dT[2]);
        }

        public static void dataClient()
        {
            zap(qrEncryptClient, dT[3]);
        }

        public static void dataCompany()
        {
            zap(qrCompany, dT[4]);
        }

        public static void dataCountry()
        {
            zap(qrCountry, dT[5]);
        }

        public static void dataHotel()
        {
            zap(qrHotel, dT[6]);
        }

        public static void dataOrder()
        {
            zap(qrOrder, dT[7]);
        }

        public static void dataPayment()
        {
            zap(qrPayment, dT[8]);
        }

        public static void dataReport()
        {
            zap(qrReport, dT[9]);
        }

        public static void dataRole()
        {
            zap(qrRole, dT[10]);
        }

        public static void dataTrip()
        {
            zap(qrTrip, dT[11]);
        }

        public static void dataUser()
        {
            zap(qrEncryptUser, dT[12]);
        }
        
        public static void dataVoucher()
        {
            zap(qrVoucher, dT[13]);
        }

        public const string regMail = "Регистрация в программе IstTurist";
        public const string regBodyMail = "Вы зарегистрировались в программе IstTurist. Используйте этот email для входа в программу и для восстановления пароля.";
        public const string remind = "Восстановление пароля";
        public const string remindBody = "Это сообщение было отправлено для восстановления пароля к аккаунту. Ваш пароль: ";

        public static void SendMail(string toAcc, string subject, string bodyMail)
        {
            try
            {
                var from = new MailAddress("istturist@ao-anbu.ru", "IstTurist"); //адрес с которого идёт отправка
                var to = new MailAddress(toAcc); //адрес на который идёт отправка

                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);    //протокол для отправки и порт
                smtp.Credentials = new NetworkCredential("IstTurist@ao-anbu.ru","maxim1354"); //"IstTurist@ao-anbu.ru" maxim1354  //логин и пароль отправителя
                smtp.EnableSsl = true;  //шифрование включено

                MailMessage mail = new MailMessage(from, to);   //создание нового экземпляра класса, mail
                mail.Subject = subject; //переменная Subject получает значение subject
                mail.IsBodyHtml = true;
                mail.Body = "<div><p>Здравствуйте!<br><br><br>" + bodyMail + "<br></p><br></div>"; //переменная Body получает значение bodyMail
                smtp.SendMailAsync(mail);    //отправляет содержимое Subject и Body на сервер SMTP
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка! Попробуйте снова.", MessageBoxButtons.OK, MessageBoxIcon.Error);  //программное сообщение, если сообщение не отправлено
            }
        }
    }
}
