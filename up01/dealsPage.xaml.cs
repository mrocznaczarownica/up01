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
        //SqlConnection cnt = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public dealsPage()
        {
            InitializeComponent();

            List<string> objects = new() { "House", "Apartment", "Land" };
            objType.ItemsSource = objects;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {

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
