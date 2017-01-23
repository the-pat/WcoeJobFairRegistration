using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        private readonly IDataAccess _dataAccess;

        public StudentPage()
        {
            _dataAccess = new DataAccess.DataAccess();
            InitializeComponent();

            // TODO: Add the majors to the combobox

            FocusManager.SetFocusedElement(this, this.txtIdNumber);
        }

        private void OnStudentIdTextChanged(object sender, TextChangedEventArgs e)
        {
            var rNumber = 0;

            if (string.IsNullOrWhiteSpace(this.txtIdNumber.Text) ||
                this.txtIdNumber.Text.Trim().Length != 8 &&
                !int.TryParse(this.txtIdNumber.Text.Trim(), out rNumber))
            {
                return;
            }

            var student = _dataAccess.GetStudentByRNum(rNumber);

            if (student == null)
            {
                this.txtErrorMessage.Text = "No student with that ID registered.\n\nPlease enter your information.";
                this.txtErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                this.txtFirstName.Text = student.FirstName;
                this.txtLastName.Text = student.LastName;
                this.cbxMajor.SelectedIndex = this.cbxMajor.Items.IndexOf(student.Major);

                // Since all of the information is ready, allow the student to print.
                this.PrintButton.IsEnabled = true;
            }

            FocusManager.SetFocusedElement(this, this.txtFirstName);
        }

        private void OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintButton.IsEnabled = false;
            PrintButton.Content = "Printing...";

            IPrintService printService = new DymoService();
            MessageBoxResult result;
            var student = new Student
            {
                FirstName = this.txtFirstName.Text,
                LastName = this.txtLastName.Text,
                Major = this.cbxMajor.Text
            };

            if (printService.PrintStudentLabel(student))
            {
                // TODO: Log student attendance (rnumber, name, time)
                result = MessageBox.Show("Printing complete.\n\nGood luck!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
            }
            else
            {
                // TODO: Log error
                result = MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            this.NavigationService.Navigate(this);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.PrintButton.IsEnabled = !string.IsNullOrWhiteSpace(this.txtFirstName.Text) &&
                                         !string.IsNullOrWhiteSpace(this.txtLastName.Text);
        }
    }
}
