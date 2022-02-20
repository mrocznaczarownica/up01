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
    /// Логика взаимодействия для clientWindow.xaml
    /// </summary>
    public partial class clientWindow : Window
    {
        flatPage flatPage1 = new flatPage();
        housePage housePage1 = new housePage();
        landsPage landsPage1 = new landsPage();
        editPage edit = new editPage();

        public clientWindow()
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(edit);
        }

        private void apart_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(flatPage1);
        }

        private void house_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(housePage1);
        }

        private void land_Click_1(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(landsPage1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clientdata.DataContext = this.Select("select * from [dbo].[apartments]");
            clientdata.ItemsSource = this.Select("select * from [dbo].[apartments]").DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clientdata.DataContext = this.Select("select * from [dbo].[houses]");
            clientdata.ItemsSource = this.Select("select * from [dbo].[houses]").DefaultView;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clientdata.DataContext = this.Select("select * from [dbo].[lands]");
            clientdata.ItemsSource = this.Select("select * from [dbo].[lands]").DefaultView;
        }
    }
}
