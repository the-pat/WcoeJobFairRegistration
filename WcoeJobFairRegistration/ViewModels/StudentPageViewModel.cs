using System;
using System.Threading.Tasks;
using System.Windows;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.ViewModels
{
    public class StudentPageViewModel : ObservableObject
    {
        protected readonly IStudentRepository studentRepository;
        protected readonly IPrintService printService;

        public StudentPageViewModel()
        {
            App app = (Application.Current as App);
            studentRepository = app.StudentRepository;
            printService = app.PrintService;
        }

        protected string rNumber;
        public virtual string RNumber
        {
            get { return rNumber; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref rNumber, string.Empty);
                }
                else if(value.Length == 15)
                {
                    SetProperty(ref rNumber, value.Substring(1, 8));
                    RNumberError = "";
                    FindStudent();
                }
                else
                {
                    RNumberError = "Swipe your card again to retry.";
                }
            }
        }

        private string _rNumberError = "";
        public virtual string RNumberError
        {
            get { return _rNumberError; }
            set { SetProperty(ref _rNumberError, value); }
        }

        private string _firstName;
        public virtual string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public virtual string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private Command _printCommand;
        public virtual Command PrintCommand
        {
            get
            {
                return _printCommand ??
                       (_printCommand = new Command(async () => await ExecutePrintCommand(), () => CanPrint));
            }
        }

        private async Task ExecutePrintCommand()
        {
            CanPrint = false;

            var student = new AttendingStudent
            {
                FirstName = FirstName,
                LastName = LastName,
                RNumber = RNumber,
                CheckInTime = DateTime.Now
            };
            var result = await Task.Run(() => printService.PrintStudentLabel(student));

            if(result)
            {
                await studentRepository.Save(student);
                // TODO: Show appropriate message and reset page
            }
            else
            {
                MessageBox.Show("A printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            Reset();
        }

        private bool _inputEnabled = true;
        public virtual bool InputEnabled
        {
            get { return _inputEnabled; }
            set { SetProperty(ref _inputEnabled, value); }
        }

        private bool _canPrint = false;
        public virtual bool CanPrint
        {
            get { return _canPrint; }
            set { SetProperty(ref _canPrint, value, onChanged: () => PrintCommand.ChangeCanExecute()); }
        }

        private bool _shouldFocusOnRNumber = true;
        public bool ShouldFocusOnRNumber
        {
            get { return _shouldFocusOnRNumber; }
            set { SetProperty(ref _shouldFocusOnRNumber, value); }
        }

        private async Task FindStudent()
        {
            InputEnabled = false;

            var student = await studentRepository.Find("R" + RNumber);
            if(student == null)
            {
                // TODO: Show better error
                MessageBox.Show($"Unable to locate student with R# '{RNumber}'");
                InputEnabled = true;
            }
            else
            {
                FirstName = student.FirstName;
                LastName = student.LastName;

                CanPrint = true;
            }
        }

        protected virtual void Reset()
        {
            RNumber = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;

            InputEnabled = true;
            CanPrint = false;

            // NECESSARY: just setting to true would not trigger the notification
            // since it's value is unchanged (originally true) due to the way that SetProperty works
            ShouldFocusOnRNumber = false;
            ShouldFocusOnRNumber = true;

            RNumberError = "";
        }
    }
}