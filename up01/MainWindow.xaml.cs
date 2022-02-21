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
        SignUp signUp = new SignUp();
        userWindow userWindow = new userWindow();
        SignRieltor signRieltor = new SignRieltor();
        clientWindow client = new clientWindow();

        public MainWindow()
        {
            InitializeComponent();
        }
        public string share()
        {
            string share = tbLogin.Text;
            return share;
        }

        public void LogButton_Click(object sender, RoutedEventArgs e)
        {
            string log = tbLogin.Text;
            string pass = pb1.Password;
            if (log.Length > 0)
            {
                if (pass.Length > 0)
                {
                    nofiticftionLog.Content = "Всё прекрасно";
                    DataTable dt_client = this.Select("SELECT * FROM [dbo].[clients] where [login] ='" + log + "'and [password] ='" + pass + "'");
                    if (dt_client.Rows.Count == 0)
                    {
                        DataTable dt_agents = this.Select("SELECT * FROM [dbo].[agents] where [login] ='" + log + "'and [password] ='" + pass + "'");
                        if (dt_agents.Rows.Count == 1)
                        {
                            MessageBox.Show("Вы агент");
                            userWindow.Show();
                            userWindow.log = log;
                            this.Hide();
                        }
                        else
                        {
                            nofiticftionPass.Content = "Логин или пароль введен неверно";
                            nofiticftionLog.Content = null;
                            pb1.Password = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы клиент");
                        client.Show();
                        client.log = log;
                        this.Hide();
                    }
                }
                else
                {
                    nofiticftionPass.Content = "Введите пароль";
                    Style passStyle = (Style)Resources["passStyle"];
                    Style labStyle = (Style)Resources["labelStyle"];
                }
            }
            else
            {
                nofiticftionLog.Content = "Введите логин";
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
            sqlConnection.Close();
            return dataTable;
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            signUp.Show();
            this.Hide();
        }

        private void rieltor_Click(object sender, RoutedEventArgs e)
        {
            signRieltor.Show();
            this.Hide();
        }
    }
}
