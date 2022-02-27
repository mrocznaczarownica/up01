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
    /// Логика взаимодействия для dealsPage.xaml
    /// </summary>
    public partial class dealsPage : Page
    {
        SqlConnection cnt = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public dealsPage()
        {
            InitializeComponent();

            List<string> objects = new() { "House", "Apartment", "Land" };
            objType.ItemsSource = objects;

            string query = "select Id from [dbo].[supplies]";
            List<string> rieltors = new List<string>();
            using (cnt)
            {
                cnt.Open();
                using (SqlCommand cmd = new SqlCommand(query, cnt))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rieltors.Add(reader["Id"].ToString());
                        }
                    }
                }
            }
            supplyId.ItemsSource = rieltors;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into [dbo].[deals]([Demand_Id], [Supply_Id])"
                + " values(@demandId, @supplyId)";
            if (objType.SelectedItem != null)
            {
                if (demandId.SelectedItem != null)
                {
                    if (supplyId.SelectedItem != null)
                    {
                        using (conn)
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.Add("@demandId", SqlDbType.NVarChar).Value = demandId.SelectedItem.ToString();
                                cmd.Parameters.Add("@supplyId", SqlDbType.Int).Value = supplyId.SelectedItem.ToString();
                                int rowsAdded = cmd.ExecuteNonQuery();
                                if (rowsAdded > 0)
                                {
                                    int commision1;
                                    //this.Visibility = Visibility.Hidden;
                                    DataTable dt_price = this.Select($"SELECT Price FROM [dbo].[supplies] where Id = '{supplyId.SelectedItem}'");
                                    int commision2 = Convert.ToInt32(dt_price.Rows[0][0]) / 100 * 3;
                                    if (objType.SelectedIndex == 0)
                                    {
                                        commision1 = 30000 + Convert.ToInt32(dt_price.Rows[0][0]) / 100;
                                        comProd.Content = "Комиссия для продавца составляет " + commision1;
                                    }
                                    if (objType.SelectedIndex == 1)
                                    {
                                        commision1 = 36000 + Convert.ToInt32(dt_price.Rows[0][0]) / 100;
                                        comProd.Content = "Комиссия для продавца составляет " + commision1;
                                    }
                                    if (objType.SelectedIndex == 2)
                                    {
                                        commision1 = 30000 + Convert.ToInt32(dt_price.Rows[0][0]) / 100 * 2;
                                        comProd.Content = "Комиссия для продавца составляет " + commision1;
                                    }
                                    comPok.Content = "Комиссия для покупателя составляет " + commision2;
                                    MessageBox.Show("Данные зарегистрированы");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите предложение");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите потребность");
                }
            }
            else
            {
                MessageBox.Show("Выберите тип объекта");
            }
        }

        private void obSel(object sender, SelectionChangedEventArgs e)
        {
            string source;
            List<string> estate = new List<string>();
            if (objType.SelectedIndex == 0)
            {
                using (connect1)
                {
                    source = "SELECT Id FROM [dbo].[house-demands]";
                    connect1.Open();
                    using (SqlCommand cmd = new SqlCommand(source, connect1))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                estate.Add(reader["Id"].ToString());
                            }
                        }
                    }
                }
                demandId.ItemsSource = estate;
                connect1.Close();
            }
            else if (objType.SelectedIndex == 1)
            {
                using (connect1)
                {
                    source = "SELECT Id FROM [dbo].[apartment-demands]";
                    connect1.Open();
                    using (SqlCommand cmd = new SqlCommand(source, connect1))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                estate.Add(reader["Id"].ToString());
                            }
                        }
                    }
                }
                demandId.ItemsSource = estate;
                connect1.Close();
            }
            else if (objType.SelectedIndex == 2)
            {
                using (connect1)
                {
                    source = "SELECT Id FROM [dbo].[land-demands]";
                    connect1.Open();
                    using (SqlCommand cmd = new SqlCommand(source, connect1))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                estate.Add(reader["Id"].ToString());
                            }
                        }
                    }
                }
                demandId.ItemsSource = estate;
                connect1.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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
