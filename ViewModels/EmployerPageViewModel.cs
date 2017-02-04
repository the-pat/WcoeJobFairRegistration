using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.ViewModels
{
    public class EmployerPageViewModel : ObservableObject
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IPrintService _printService;

        public EmployerPageViewModel()
        {
            var app = (Application.Current as App);
            _employerRepository = app.EmployeeRepository;
            _printService = app.PrintService;
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

        private string _organization;
        public string Organization
        {
            get { return _organization; }
            set
            {
                SetProperty(ref _organization, value);
                if (CanPrint) PrintCommand.ChangeCanExecute();
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
                if (CanPrint) PrintCommand.ChangeCanExecute();
            }
        }

        private string _hotel;
        public string Hotel
        {
            get { return _hotel; }
            set { SetProperty(ref _hotel, value); }
        }

        private string _numberOfNights;
        public string NumberOfNights
        {
            get { return _numberOfNights; }
            set { SetProperty(ref _numberOfNights, value); }
        }

        private bool _isAlumni;
        public bool IsAlumni
        {
            get { return _isAlumni; }
            set { SetProperty(ref _isAlumni, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
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

            var employer = new Employer
            {
                FirstName = FirstName,
                LastName = LastName,
                Organization = Organization,
                Title = Title,
                IsAlumni = IsAlumni,
                CheckedInTime = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(Hotel)) employer.Hotel = Hotel;
            if (!string.IsNullOrWhiteSpace(NumberOfNights)) employer.NumberOfNights = int.Parse(NumberOfNights);

            var result = await Task.Run(() => _printService.PrintEmployerLabel(employer));

            if (result)
            {
                await _employerRepository.Save(employer);
                // TODO: Show appropriate message
            }
            else
            {
                var blocking = MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                            "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            ClearData();
            _printCommand.ChangeCanExecute();
        }

        private bool _canPrint = true;
        public bool CanPrint
        {
            get
            {
                return _canPrint &&
                       !(string.IsNullOrWhiteSpace(FirstName) ||
                         string.IsNullOrWhiteSpace(LastName) ||
                         string.IsNullOrWhiteSpace(Title) ||
                         string.IsNullOrWhiteSpace(Organization));
            }
            set { SetProperty(ref _canPrint, value); }
        }

        private void ClearData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Organization = string.Empty;
            Title = string.Empty;
            Hotel = string.Empty;
            NumberOfNights = string.Empty;
            IsAlumni = false;

            CanPrint = true;
        }
    }
}