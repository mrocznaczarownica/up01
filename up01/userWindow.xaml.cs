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
using System.Windows.Shapes;

namespace up01
{
    /// <summary>
    /// Логика взаимодействия для userWindow.xaml
    /// </summary>
    public partial class userWindow : Window
    {
        public userWindow()
        {
            InitializeComponent();

            rieldata.DataContext = this.Select("select * from [dbo].[apartments]");
            rieldata.ItemsSource = this.Select("select * from [dbo].[apartments]").DefaultView;//нужно ли фильтровать выводимые данные?????? а как? айди?
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
