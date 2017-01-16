using System.Windows;
using System.Windows.Controls;
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
        private readonly ICardService _cardReader;
        private readonly IStudentManager _studentManager;

        private int _rNumber;

        public StudentPage()
        {
            this._studentManager = new StudentManager();
            InitializeComponent();

            // TODO: Add the majors to the combobox

            // TODO: Listen for student to swipe their student ID

            // TODO: Get student name and major from database

            // TODO: Enable the student to print their label
        }

        /// <summary>
        /// When the student id is swiped, get the student's information
        /// </summary>
        private void OnCardSwipe()
        {
            this._rNumber = this._cardReader.GetRNumber();
            var student = this._studentManager.GetStudentById(this._rNumber);

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
            }
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
    }
}
