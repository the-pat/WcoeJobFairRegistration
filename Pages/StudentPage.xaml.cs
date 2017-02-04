using System.Windows.Controls;
using WcoeJobFairRegistration.ViewModels;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    ///     Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public StudentPage()
        {
            DataContext = new StudentPageViewModel();
            InitializeComponent();
        }
    }
}