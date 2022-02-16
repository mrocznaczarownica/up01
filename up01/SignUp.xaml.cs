using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace up01
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        SignRieltor signRieltor = new SignRieltor();
        public SignUp()
        {
            InitializeComponent();
        }

        private void rieltorBt_Click(object sender, RoutedEventArgs e)
        {
            signRieltor.Show();
            this.Hide();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string fName = firstName.Text;//familia
            string mName = middleName.Text;//imya
            string lName = lastName.Text;//otchestvo
            string phome = phone.Text;
            string email = tbEmail.Text;
            string log = tbLogin.Text;
            string pass = tbPass.Password;


            if (fName.Length > 0)//проверка на имя
            {
                if(mName.Length > 0)//фамилию
                {
                    if(phome.Length > 0 || email.Length > 0)//ноер телефона или имейл
                    {
                        if(log.Length > 0)// proverka na login
                        {
                            if (pass.Length > 0)//i parol'
                            {

                            }
                            else
                            {
                                MessageBox.Show("Введите пароль");
                                
                            }
                        }
                    }
                }
            }
//            if (textBox_login.Text.Length >; 0) // проверяем логин
//{
//                if (password.Password.Length >; 0) // проверяем пароль
//	{
//                    if (password_Copy.Password.Length >; 0) // проверяем второй пароль
//		{


//                    }else MessageBox.Show("Повторите пароль");
//                }else MessageBox.Show("Укажите пароль");
//            }else MessageBox.Show("Укажите логин");
        }
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
