using System.Windows.Controls;
using WcoeJobFairRegistration.ViewModels;

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
            DataContext = new MainPageViewModel();
        }
    }
}