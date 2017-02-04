using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WcoeJobFairRegistration.ViewModels;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    /// Interaction logic for ManualStudentPage.xaml
    /// </summary>
    public partial class ManualStudentPage : Page
    {
        public ManualStudentPage()
        {
            DataContext = new ManualStudentViewModel();
            InitializeComponent();
        }

        private void NumericTextValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^\d]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreventWhitespace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
            base.OnPreviewKeyDown(e);
        }
    }
}
