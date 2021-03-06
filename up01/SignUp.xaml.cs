using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        userWindow user = new userWindow();
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public SignUp()
        {
            InitializeComponent();
            
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
                                DataTable dt_log = this.Select("SELECT * FROM [dbo].[clients] where [login] ='" + log + "'");
                                DataTable dt_ag = this.Select("SELECT * FROM[dbo].[agents] where[login] = '" + log + "'");
                                if(dt_log.Rows.Count == 0 && dt_ag.Rows.Count == 0)
                                {
                                    string query = "insert into [dbo].[clients]([FirstName],[MiddleName],[LastName],[Phone],[Email],[login],[password]) values(@firstName, @midName, @lastName, @phone, @email, @login, @pass)";
                                    using (conn)
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand(query, conn))
                                        {
                                            cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = fName;
                                            cmd.Parameters.Add("@midName", SqlDbType.NVarChar).Value = mName;
                                            cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lName;
                                            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phome;
                                            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                                            cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = log;
                                            cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass;
                                            int rowsAdded = cmd.ExecuteNonQuery();
                                            if (rowsAdded > 0)
                                            {
                                                MessageBox.Show("Пользователь загеристрирован");
                                                //passNofotication.Content = "Пользователь загеристрирован";

                                                user.Show();
                                                this.Hide();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    logNofotication.Content = "Пользователь с таким логином уже существует";
                                }
                            }
                            else
                            {
                                passNofotication.Content = "Введите пароль";
                            }
                        }
                        else
                        {
                            logNofotication.Content = "Введите логин";
                        }
                    }
                    else
                    {
                        phoneNofiticftion.Content = "Введите номер телефона или адрес электронной почты";
                    }
                }
                else
                {
                    famNofiticftion.Content = "Введите фамилию";
                }
            }
            else
            {
                nameNofiticftion.Content = "Введите имя";
            }

        }

        private void num_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if(!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void tbLogin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
