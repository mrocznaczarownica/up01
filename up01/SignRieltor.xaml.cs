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
    /// Логика взаимодействия для SignRieltor.xaml
    /// </summary>
    public partial class SignRieltor : Window
    {
        userWindow user = new userWindow();
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public SignRieltor()
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
            string fName = firstName.Text;//familia
            string mName = middleName.Text;//imya
            string lName = lastName.Text;//otchestvo
            string fract = fraction.Text; //не проверяй!!!!
            string log = tbLogin.Text;
            string pass = tbPass.Password;


            if (fName.Length > 0)//проверка на имя
            {
                if (mName.Length > 0)//фамилию
                {
                    if (lName.Length > 0)//ноер телефона или имейл
                    {
                        if (log.Length > 0)// proverka na login
                        {
                            if (pass.Length > 0)//i parol'
                            {
                                DataTable dt_log = this.Select("SELECT * FROM [dbo].[clients] where [login] ='" + log + "'");
                                DataTable dt_ag = this.Select("SELECT * FROM[dbo].[agents] where[login] = '" + log + "'");
                                if (dt_log.Rows.Count == 0 && dt_ag.Rows.Count == 0)//все верно
                                {
                                    string query = "insert into [dbo].[agents]([FirstName],[MiddleName],[LastName],[DealShare],[login],[password]) values(@firstName, @midName, @lastName, @fract, @login, @pass)";
                                    using (conn)
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand(query, conn))
                                        {
                                            cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = fName;
                                            cmd.Parameters.Add("@midName", SqlDbType.NVarChar).Value = mName;
                                            cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lName;
                                            cmd.Parameters.Add("@fract", SqlDbType.Int).Value = fract;
                                            cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = log;
                                            cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass;
                                            int rowsAdded = cmd.ExecuteNonQuery();
                                            if (rowsAdded > 0)
                                            {
                                                MessageBox.Show("Агент загеристрирован");

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
                        lastNofitication.Content = "Введите отчество";
                    }
                }
                else
                {
                    midNofitication.Content = "Введите фамилию";
                }
            }
            else
            {
                nameNofitication.Content = "Введите имя";
            }
        }

        private void fraction_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void fraction_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
