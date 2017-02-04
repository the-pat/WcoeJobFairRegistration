using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.ViewModels
{
    public class ManualStudentViewModel : ObservableObject
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPrintService _printService;

        public ManualStudentViewModel()
        {
            App app = (Application.Current as App);
            _studentRepository = app.StudentRepository;
            _printService = app.PrintService;

            _studentRepository.Load(app.StudentCsvFilePath);
        }

        private string _rNumber;
        public string RNumber
        {
            get { return _rNumber; }
            set
            {
                if (value.Length != 8)
                {
                    CardError = string.IsNullOrWhiteSpace(value) ? "" : "Please enter a valid RNumber";
                    CanPrint = false;
                }
                else
                {
                    CardError = string.Empty;
                    CanPrint = true;
                    if (CanPrint) PrintCommand.ChangeCanExecute();
                }

                SetProperty(ref _rNumber, value);
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
            set
            {
                SetProperty(ref _firstName, value);
                if (CanPrint) PrintCommand.ChangeCanExecute();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                if (CanPrint) PrintCommand.ChangeCanExecute();
            }
        }

        private Command _printCommand;
        public Command PrintCommand
        {
            get { return _printCommand ?? (_printCommand = new Command(async () => await ExecutePrintCommand(), () => CanPrint)); }
        }

        private async Task ExecutePrintCommand()
        {
            CanPrint = false;
            _printCommand.ChangeCanExecute();

            var student = new AttendingStudent { FirstName = FirstName, LastName = LastName, RNumber = int.Parse(RNumber), CheckInTime = DateTime.Now };
            var result = await Task.Run(() => _printService.PrintStudentLabel(student));

            if (result)
            {
                await _studentRepository.Save(student);
                // TODO: Show appropriate message
            }
            else
            {
                var blocking = MessageBox.Show("A printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            ClearData();
            _printCommand.ChangeCanExecute();
        }

        private bool _inputEnabled = true;
        public bool InputEnabled
        {
            get { return _inputEnabled; }
            set { SetProperty(ref _inputEnabled, value); }
        }

        private bool _canPrint = true;
        public bool CanPrint
        {
            get
            {
                return _canPrint &&
                       !(string.IsNullOrWhiteSpace(RNumber) ||
                         string.IsNullOrWhiteSpace(FirstName) ||
                         string.IsNullOrWhiteSpace(LastName));
            }
            set { SetProperty(ref _canPrint, value); }
        }

        private void ClearData()
        {
            RNumber = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;

            CanPrint = true;
        }
    }
}
