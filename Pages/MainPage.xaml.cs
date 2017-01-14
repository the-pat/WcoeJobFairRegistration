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

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Page page = new AdminPage();

            if (button.Equals(this.StudentButton))
            {
                page = new StudentPage();
            }
            else if (button.Equals(this.EmployerButton))
            {
                page = new EmployerPage();
            }

            this.NavigationService.Navigate(page);
        }
    }
}
