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

namespace FoolGame.Uil.Window
{
    /// <summary>
    /// Interaction logic for VkPassword.xaml
    /// </summary>
    public partial class VkPassword : System.Windows.Window
    {
        public VkPassword()
        {
            InitializeComponent();
            DataContext = this;
        }

        public String Email { get; set; }
        public String Password { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
