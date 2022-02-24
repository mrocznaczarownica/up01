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

        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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
            int priceVal;
            string query = "insert into [dbo].[apartment-demands]([Address_City],[Address_Street],"
                + "[Address_House],[Address_Number],[MinPrice],[MaxPrice],[AgentId],[ClientId],[MinArea]," +
                "[MaxArea],[MinRooms],[MaxRooms],[MinFloor],[MaxFloor]) values(@city, @street, @house, @addNum, @minPrice, @maxPrice,"
                + " @agentId, @clientId, @minArea, @maxArea, @minRooms, @maxRooms, @minFloor, @maxFloor)";
            string dt_riel = this.Select("select id from [dbo].[agents] where [FirstName] ='" + rielBox.SelectedItem.ToString() + "'").ToString();
            string dt_client = this.Select("SELECT id from [dbo].[clients] where [login] ='" + log + "'").ToString();
            if (rielBox.SelectedItem != null)
            {
                if (maxPrice.Text == "")
                {
                    priceVal = 0;
                }
                else
                {
                    priceVal = int.Parse(maxPrice.Text);
                }
                using (conn)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = city.Text;
                        cmd.Parameters.Add("@street", SqlDbType.NVarChar).Value = street.Text;
                        cmd.Parameters.Add("@house", SqlDbType.NVarChar).Value = house.Text;
                        cmd.Parameters.Add("@addNum", SqlDbType.NVarChar).Value = adrNum.Text;
                        cmd.Parameters.Add("@minPrice", SqlDbType.NVarChar).Value = minPrice.Text;
                        cmd.Parameters.Add("@maxPrice", SqlDbType.Int).Value = priceVal;
                        cmd.Parameters.Add("@agentId", SqlDbType.Int).Value = dt_riel.ToString();
                        cmd.Parameters.Add("@clientId", SqlDbType.Int).Value = dt_client.ToString();
                        cmd.Parameters.Add("@minArea", SqlDbType.Int).Value = minArea.Text;
                        cmd.Parameters.Add("@maxArea", SqlDbType.NVarChar).Value = maxArea.Text;
                        cmd.Parameters.Add("@minRooms", SqlDbType.Int).Value = minRooms.Text;
                        cmd.Parameters.Add("@maxRooms", SqlDbType.NVarChar).Value = maxRooms.Text;
                        cmd.Parameters.Add("@minFloor", SqlDbType.Int).Value = minFloor.Text;
                        cmd.Parameters.Add("@maxFloor", SqlDbType.Int).Value = maxFloor.Text;
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
                MessageBox.Show("Поле Риэлтор не может быть пустым");
            }
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
