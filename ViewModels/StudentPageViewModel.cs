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
        private readonly IStudentRepository _studentRepository;
        private readonly IPrintService _printService;
        private readonly App _app;

        public StudentPageViewModel()
        {
            _app = (Application.Current as App);
            _studentRepository = _app.StudentRepository;
            _printService = _app.PrintService;
            if (_app.IsManualEntry)
            {
                NameInputEnabled = true;
                CanPrint = true;
            }
            // TODO: Remove
            _studentRepository.Load(_app.FilePath);
        }

        private string _rNumber;
        public string RNumber
        {
            get { return _rNumber; }
            set
            {
                if(value.Length == 15)
                {
                    SetProperty(ref _rNumber, value.Substring(1, 8));
                    CardError = "";
                    if(_app.IsManualEntry == false) { FindStudent(); }
                    
                }
                else
                {
                    CardError = "Swipe your card again to retry.";
                }
            }
        }

        private string _cardError;
        public string CardError
        {
            get { return _cardError; }
            set { SetProperty(ref _cardError, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private Command _printCommand;
        public Command PrintCommand
        {
            get { return _printCommand ?? (_printCommand = new Command(async () => await ExecutePrintCommand(), () => CanPrint)); }
        }

        private async Task ExecutePrintCommand()
        {
            _printCommand.ChangeCanExecute();

            var student = new AttendingStudent { FirstName = FirstName, LastName = LastName, RNumber = int.Parse(RNumber) };
            var result = await Task.Run(() => _printService.PrintStudentLabel(student));

            if(result)
            {
                await _studentRepository.Save(student);
                // TODO: Show appropriate message and reset page
            }
            else
            {
                MessageBox.Show("A printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            _printCommand.ChangeCanExecute();            
        }    

        private bool _inputEnabled = true;
        public bool InputEnabled
        {
            get { return _inputEnabled; }
            set { SetProperty(ref _inputEnabled, value); }
        }
        private bool _nameInputEnabled = false;
        public bool NameInputEnabled
        {
            get { return _nameInputEnabled; }
            set { SetProperty(ref _nameInputEnabled, value); }
        }

        private bool _canPrint = false;
        public bool CanPrint
        {
            get { return _canPrint; }
            set { SetProperty(ref _canPrint, value); }
        }

        private async Task FindStudent()
        {
            InputEnabled = false;

            var student = await _studentRepository.Find("R" + RNumber);
            if(student == null)
            {
                // TODO: Show better error
                MessageBox.Show("Student not found");
                InputEnabled = true;
            }
            else
            {
                FirstName = student.FirstName;
                LastName = student.LastName;

                CanPrint = true;
                PrintCommand.ChangeCanExecute();
            }
        }
    }
}