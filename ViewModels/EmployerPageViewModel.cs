using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.ViewModels
{
    public class EmployerPageViewModel : ObservableObject
    {
        private readonly IPrintService _printService;

        public EmployerPageViewModel()
        {
            _printService = new DymoService();
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

        private string _organization;
        public string Organization
        {
            get { return _organization; }
            set { SetProperty(ref _organization, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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
        public ICommand PrintCommand
        {
            get { return _printCommand ?? (_printCommand = new Command(async () => await ExecutePrintCommand())); }
        }

        private async Task ExecutePrintCommand()
        {
            _printCommand.ChangeCanExecute();

            var employer = new Employer
            {
                FirstName = FirstName,
                LastName = LastName,
                Organization = Organization,
                Title = Title,
                Hotel = Hotel,
                NumberOfNights = int.Parse(NumberOfNights),
                IsAlumni = IsAlumni,
                CheckedInTime = DateTime.Now
            };
            var result = await Task.Run(() => _printService.PrintLabel(employer));

            if (result)
            {
                // TODO: Persist to file
            }
            else
            {
                MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            ClearData();

            _printCommand.ChangeCanExecute();
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
        }
    }
}