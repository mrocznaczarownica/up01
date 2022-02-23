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
    /// Логика взаимодействия для demandPage.xaml
    /// </summary>
    public partial class demandPage : Page
    {
        SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string log;
        public demandPage()
        {
            InitializeComponent();

            string query = "select FirstName from [dbo].[agents]";
            List<string> rieltors = new List<string>();
            using (connect1)
            {
                connect1.Open();
                using (SqlCommand cmd = new SqlCommand(query, connect1))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rieltors.Add(reader["FirstName"].ToString());
                        }
                    }
                }
            }
            rielBox.ItemsSource = rieltors;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            //string query = "update [dbo].[clients] set [FirstName] = @firstName, [MiddleName] = @midName, [LastName] = @lastName, [Phone] = @phone, [Email] = @email, [password] = @pass where login = '" + log + "'";
            //using (connect1)
            //{
            //    connect1.Open();
            //    using (SqlCommand cmd = new SqlCommand(query, connect1))
            //    {
            //        cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firName.Text;
            //        cmd.Parameters.Add("@midName", SqlDbType.NVarChar).Value = mName.Text;
            //        cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lastName.Text;
            //        cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone.Text;
            //        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email.Text;
            //        cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass.Text;
            //        int rowsAdded = cmd.ExecuteNonQuery();
            //        if (rowsAdded > 0)
            //        {
            //            MessageBox.Show("Данные обновлены");
            //            this.Visibility = Visibility.Hidden;
            //        }
            //    }
            //}
        }

        private void minPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void minPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
