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
    /// Логика взаимодействия для editRiel.xaml
    /// </summary>
    public partial class editRiel : Page
    {
        public string log;
        SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection connect = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public editRiel()
        {
            InitializeComponent();
        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            using (connect)
            {
                string sqlcmd = "select * from [dbo].[agents] where [login] ='" + log + "'";
                connect.Open();
                SqlCommand cmd = new SqlCommand(sqlcmd, connect);
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    firName.Text = rd[1].ToString();
                    mName.Text = rd[2].ToString();
                    lastName.Text = rd[3].ToString();
                    deal.Text = rd[4].ToString();
                    login.Text = rd[5].ToString();
                    pass.Text = rd[6].ToString();
                }
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string query = "update [dbo].[agents] set [FirstName] = @firstName, [MiddleName] = @midName, [LastName] = @lastName, [DealShare] = @deal, [password] = @pass where login = '" + log + "'";
            using (connect1)
            {
                connect1.Open();
                using (SqlCommand cmd = new SqlCommand(query, connect1))
                {
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firName.Text;
                    cmd.Parameters.Add("@midName", SqlDbType.NVarChar).Value = mName.Text;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lastName.Text;
                    cmd.Parameters.Add("@deal", SqlDbType.NVarChar).Value = deal.Text;
                    cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass.Text;
                    int rowsAdded = cmd.ExecuteNonQuery();
                    if (rowsAdded > 0)
                    {
                        MessageBox.Show("Данные обновлены");
                        this.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
