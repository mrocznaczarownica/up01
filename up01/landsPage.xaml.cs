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
    /// Логика взаимодействия для landsPage.xaml
    /// </summary>
    public partial class landsPage : Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public landsPage()
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

        private void adressHouse_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void adressHouse_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string city = adressCity.Text;
            string street = adressStreet.Text;
            string numHouse = adressHouse.Text;
            string numFl = adressNumber.Text;
            string latit = latitude.Text;
            string longit = longitude.Text;
            string totalAr = totalArea.Text;

            if (city.Length > 0)
            {
                if (street.Length > 0)
                {
                    if (numHouse.Length > 0)
                    {
                        if (numFl.Length > 0)
                        {
                            if (latit.Length > 0)
                            {
                                if (longit.Length > 0)
                                {
                                    if (totalAr.Length > 0)
                                    {
                                        string query = "insert into [dbo].[lands]([Address_City],[Address_Street],[Address_House],[Address_Number],[Coordinate_latitude],[Coordinate_longitude],[TotalArea]) values(@city, @street, @house, @number, @latit, @long, @area)";
                                        using (conn)
                                        { //@city, @street, @house, @number, @latit, @long, @area, @rooms, @floor
                                            conn.Open();
                                            using (SqlCommand cmd = new SqlCommand(query, conn))
                                            {
                                                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = city;
                                                cmd.Parameters.Add("@street", SqlDbType.NVarChar).Value = street;
                                                cmd.Parameters.Add("@house", SqlDbType.NVarChar).Value = numHouse;
                                                cmd.Parameters.Add("@number", SqlDbType.NVarChar).Value = numFl;
                                                cmd.Parameters.Add("@latit", SqlDbType.NVarChar).Value = latit;
                                                cmd.Parameters.Add("@long", SqlDbType.NVarChar).Value = longit;
                                                cmd.Parameters.Add("@area", SqlDbType.NVarChar).Value = totalAr;
                                                int rowsAdded = cmd.ExecuteNonQuery();
                                                if (rowsAdded > 0)
                                                {
                                                    MessageBox.Show("Объект недвижимости создан");
                                                         
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        totalArea.Text = "Заполните это поле";
                                    }
                                }
                                else
                                {
                                    longitude.Text = "Заполните это поле";
                                }
                            }
                            else
                            {
                                latitude.Text = "Заполните это поле";
                            }
                        }
                        else
                        {
                            adressNumber.Text = "Заполните это поле";
                        }
                    }
                    else
                    {
                        adressHouse.Text = "Заполните это поле";
                    }
                }
                else
                {
                    adressStreet.Text = "Заполните это поле";
                }
            }
            else
            {
                adressCity.Text = "Заполните это поле";
            }

        }
    }
}
