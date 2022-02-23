using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для demandTypePage.xaml
    /// </summary>
    public partial class demandTypePage : Page
    {
        demandPage demand = new demandPage();
        //clientWindow client = new clientWindow();
        public string log;
        public demandTypePage()
        {
            InitializeComponent();
        }

        private void apart_Click(object sender, RoutedEventArgs e)
        {
            demFr.NavigationService.Navigate(demand);
        }

        private void house_Click(object sender, RoutedEventArgs e)
        {

        }

        private void land_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cans_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
