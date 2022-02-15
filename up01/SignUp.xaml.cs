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
using System.Windows.Shapes;

namespace up01
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        SignRieltor signRieltor = new SignRieltor();
        public SignUp()
        {
            InitializeComponent();
        }

        private void rieltorBt_Click(object sender, RoutedEventArgs e)
        {
            signRieltor.Show();
            this.Hide();
        }
    }
}
