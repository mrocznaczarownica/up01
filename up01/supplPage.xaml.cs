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
    /// Логика взаимодействия для supplPage.xaml
    /// </summary>
    public partial class supplPage : Page
    {
        SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection cnt = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string log;
        public supplPage()
        {
            InitializeComponent();

            List<string> objects = new() { "House", "Apartment", "Land" };
            objType.ItemsSource = objects;

            string query = "select Id from [dbo].[agents]";
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
                            rieltors.Add(reader["Id"].ToString());
                        }
                    }
                }
            }
            rielBox.ItemsSource = rieltors;
        }

        private void minPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into [dbo].[supplies]([Price], [AgentId], [ClientId], [RealEstateId])"
                + " values(@price, @agentId, @clientId, @realEstate)";
            int id_riel = int.Parse((string)rielBox.SelectedItem);
            DataTable dt_client = this.Select($"SELECT id from [dbo].[clients] where login ='{log}'");
            if (rielBox.SelectedItem != null)
            {
                if (objType.SelectedItem != null)
                {
                    if (price.Text.Length > 0)
                    {
                        if (realEstateId.SelectedItem != null)
                        {
                            using (conn)
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.Add("@price", SqlDbType.NVarChar).Value = price.Text;
                                    cmd.Parameters.Add("@agentId", SqlDbType.Int).Value = id_riel;
                                    cmd.Parameters.Add("@clientId", SqlDbType.Int).Value = Convert.ToInt32(dt_client.Rows[0][0].ToString());
                                    cmd.Parameters.Add("@realEstate", SqlDbType.Int).Value = realEstateId.SelectedItem.ToString();
                                    int rowsAdded = cmd.ExecuteNonQuery();
                                    if (rowsAdded > 0)
                                    {
                                        MessageBox.Show("Данные зарегистрированы");
                                        this.Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите объект");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите цену");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите тип объекта");
                }
            }
            else
            {
                MessageBox.Show("Поле Риэлтор не может быть пустым");
            }
        }

        private void minPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void obSel(object sender, SelectionChangedEventArgs e)
        {
            string source;
            List<string> estate = new List<string>();
            if (objType.SelectedIndex == 0)
            {
                using (cnt)
                {
                    source = "SELECT Id FROM [dbo].[houses]";
                    cnt.Open();
                    using (SqlCommand cmd = new SqlCommand(source, cnt))
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
                realEstateId.ItemsSource = estate;
            }
            else if (objType.SelectedIndex == 1)
            {
                using (cnt)
                {
                    source = "SELECT Id FROM [dbo].[apartments]";
                    cnt.Open();
                    using (SqlCommand cmd = new SqlCommand(source, cnt))
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
                realEstateId.ItemsSource = estate;
            }
            else if (objType.SelectedIndex == 2)
            {
                using (cnt)
                {
                    source = "SELECT Id FROM [dbo].[lands]";
                    cnt.Open();
                    using (SqlCommand cmd = new SqlCommand(source, cnt))
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
                realEstateId.ItemsSource = estate;
            }
        }
    }
}
