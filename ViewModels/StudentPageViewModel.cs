using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration.ViewModels
{
    public class StudentPageViewModel : ObservableObject
    {
        IPrintService _printService;

        public StudentPageViewModel()
        {
            _printService = new DymoService();
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
        public ICommand PrintCommand
        {
            get { return _printCommand ?? (_printCommand = new Command(async () => await ExecutePrintCommand())); }
        }

        private async Task ExecutePrintCommand()
        {
            _printCommand.ChangeCanExecute();

            var student = new Student { FirstName = FirstName, LastName = LastName, RNumber = int.Parse(RNumber) };
            var result = await Task.Run(() => _printService.PrintStudentLabel(student));

            if(result)
            {
                // TODO: Persist to file
            }
            else
            {
                MessageBox.Show("An printer error has occured.\n\nPlease ask for assistance.",
                    "Printer Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            _printCommand.ChangeCanExecute();
        }
    }
}