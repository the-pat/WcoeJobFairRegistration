using System;
using System.Windows;
using System.Windows.Controls;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

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
        }

        private void OnTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            this.PrintButton.IsEnabled = !string.IsNullOrWhiteSpace(this.txtFirstName.Text) &&
                                         !string.IsNullOrWhiteSpace(this.txtLastName.Text) &&
                                         !string.IsNullOrWhiteSpace(this.txtOrganization.Text);
        }

        private void OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintButton.IsEnabled = false;
            PrintButton.Content = "Printing...";

            IPrintService printService = new DymoService();
            MessageBoxResult result;

            var employer = new Employer
            {
                FirstName = this.txtFirstName.Text,
                LastName = this.txtLastName.Text,
                Organization = this.txtOrganization.Text
            };

            if (printService.PrintLabel(employer))
            {
                result = MessageBox.Show("Printing complete.\n\nHave a great day!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
            }
            else
            {
                result = MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            this.NavigationService.Navigate(this);
        }    
    }
}
