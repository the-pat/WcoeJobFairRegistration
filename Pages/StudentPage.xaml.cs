using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    ///     Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        private readonly IDataAccess _dataAccess;

        public StudentPage()
        {
            _dataAccess = new DataAccess.DataAccess(new LocalStudentRepository(), new LocalEmployerRepository());
            InitializeComponent();

            FocusManager.SetFocusedElement(this, txtIdNumber);
        }

        private void OnStudentIdTextChanged(object sender, TextChangedEventArgs e)
        {
            var rNumber = 0;

            // TODO: Format what the card returns (i.e. ;01234567=0067?). The first segment contains the RNum
            if (string.IsNullOrWhiteSpace(txtIdNumber.Text) ||
                txtIdNumber.Text.Trim().Length != 8 &&
                !int.TryParse(txtIdNumber.Text.Trim(), out rNumber))
                return;

            var student = _dataAccess.StudentRepo.GetByStudentID(rNumber);

            if (student == null)
            {
                txtErrorMessage.Text = "No student with that ID registered.\n\nPlease enter your information.";
                txtErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                txtFirstName.Text = student.FirstName;
                txtLastName.Text = student.LastName;

                // Since all of the information is ready, allow the student to print.
                PrintButton.IsEnabled = true;
            }

            FocusManager.SetFocusedElement(this, txtFirstName);
        }

        private void OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintButton.IsEnabled = false;
            PrintButton.Content = "Printing...";

            IPrintService printService = new DymoService();
            MessageBoxResult result;
            var student = new Student
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };

            if (printService.PrintStudentLabel(student))
                result = MessageBox.Show("Printing complete.\n\nGood luck!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
            else
                result = MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

            NavigationService.Navigate(this);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PrintButton.IsEnabled = !string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                                    !string.IsNullOrWhiteSpace(txtLastName.Text);
        }
    }
}
