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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace up01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dataBase select = new dataBase();
        SignUp signUp = new SignUp();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string pass = pb1.Password;
            if (login.Length > 6)
            {
                if (pass.Length > 0)
                {
                    DataTable dt_user = this.Select("select*from [dbo].[clients] where [Email] ='" + login + "'and [Password] = '" + pass + "'");
                    if (dt_user.Rows.Count > 0)
                    {
                        MessageBox.Show("Ну допустим ты существуешь");
                        DataTable dt_role = this.Select("select*from [dbo].[Users] where [Email] ='" + login + "'and [RoleID] ='" + 1 + "'");
                        if (dt_role.Rows.Count > 0)
                        {
                            //user.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Net");
                    }
                }
                else
                {
                    pb1.ToolTip = "Это поле введено некорректно";
                    pb1.BorderBrush = Brushes.DarkRed;
                }
            }
            else
            {
                tbLogin.ToolTip = "Это поле введено некорректно";//надо отслеживать сколько раз этот дебил неправильно вввел пароль(сделано)
                tbLogin.BorderBrush = Brushes.DarkRed;
            }
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

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            signUp.Show();
            this.Hide();
        }
    }
}
