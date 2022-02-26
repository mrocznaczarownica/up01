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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into [dbo].[apartment-demands]([Address_City],[Address_Street],"
                + "[Address_House],[Address_Number],[MinPrice],[MaxPrice],[AgentId],[ClientId],[MinArea]," +
                "[MaxArea],[MinRooms],[MaxRooms],[MinFloor],[MaxFloor]) values(@city, @street, @house, @addNum, @minPrice, @maxPrice,"
                + " @agentId, @clientId, @minArea, @maxArea, @minRooms, @maxRooms, @minFloor, @maxFloor)";
            int id_riel = int.Parse((string)rielBox.SelectedItem);
            //DataTable dt_riel = this.Select($"select * from [dbo].[agents] where FirstName = '{rielBox.SelectedItem.ToString()}'");
            DataTable dt_client = this.Select($"SELECT id from [dbo].[clients] where login ='{log}'");
            if (rielBox.SelectedItem != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = ParseString(city.Text);
                        cmd.Parameters.Add("@street", SqlDbType.NVarChar).Value = ParseString(street.Text);
                        cmd.Parameters.Add("@house", SqlDbType.NVarChar).Value = ParseString(house.Text);
                        cmd.Parameters.Add("@addNum", SqlDbType.NVarChar).Value = ParseString(adrNum.Text);
                        cmd.Parameters.Add("@minPrice", SqlDbType.NVarChar).Value = ParseString(minPrice.Text);
                        cmd.Parameters.Add("@maxPrice", SqlDbType.Int).Value = ParseString(maxPrice.Text);
                        cmd.Parameters.Add("@agentId", SqlDbType.Int).Value = id_riel;
                        cmd.Parameters.Add("@clientId", SqlDbType.Int).Value = Convert.ToInt32(dt_client.Rows[0][0].ToString());
                        cmd.Parameters.Add("@minArea", SqlDbType.Int).Value = ParseString(minArea.Text);
                        cmd.Parameters.Add("@maxArea", SqlDbType.NVarChar).Value = ParseString(maxArea.Text);
                        cmd.Parameters.Add("@minRooms", SqlDbType.Int).Value = ParseString(minRooms.Text);
                        cmd.Parameters.Add("@maxRooms", SqlDbType.NVarChar).Value = ParseString(maxRooms.Text);
                        cmd.Parameters.Add("@minFloor", SqlDbType.Int).Value = ParseString(minFloor.Text);
                        cmd.Parameters.Add("@maxFloor", SqlDbType.Int).Value = ParseString(maxFloor.Text);
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
        string ParseString(string input)
        {
            if (input == "")
                return "0";
            return input;
        }
    }
}
