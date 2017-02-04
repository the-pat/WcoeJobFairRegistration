using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;
using WcoeJobFairRegistration.ViewModels;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    /// Interaction logic for EmployerPage.xaml
    /// </summary>
    public partial class EmployerPage : Page
    {
        public EmployerPage()
        {
            InitializeComponent();
            DataContext = new EmployerPageViewModel();
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
