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
        public string log;
        public demandPage()
        {
            InitializeComponent();
            SqlConnection connect1 = new SqlConnection("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=esoft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string query = "select [FirstName] from [dbo].[agents]";
            using (connect1)
            {
                connect1.Open();
                using(SqlCommand cmd = new SqlCommand(query, connect1))
                {
                    var adapter = new SqlDataAdapter(query, connect1);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "[dbo].[agents]");

                    ComboBoxItem.DataContext = ds.Tables["[dbo].[agents]"].DefaultView;
                    ComboBoxItem.DisplayMemberPath = ds.Tables["[dbo].[agents]"].Columns["FirstName"].ToString();
                }
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {

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
