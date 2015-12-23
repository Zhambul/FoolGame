using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FoolGame.Annotations;

namespace FoolGame.Uil.Window
{
    /// <summary>
    /// Interaction logic for CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : System.Windows.Window, INotifyPropertyChanged
    {
        private string _captchaKey;

        public String CaptchaKey
        {
            get { return _captchaKey; }
            set
            {
                _captchaKey = value; 
                OnPropertyChanged();
            }
        }

        public CaptchaPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
