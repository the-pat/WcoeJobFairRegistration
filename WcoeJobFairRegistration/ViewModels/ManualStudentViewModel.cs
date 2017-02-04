using System.Text.RegularExpressions;

namespace WcoeJobFairRegistration.ViewModels
{
    class ManualStudentViewModel : StudentPageViewModel
    {
        /// <summary>
        /// Regular expression for validating R#s
        /// </summary>
        private Regex _rNumRegex = new Regex(@"^\d{8}");

        private bool _validRNumber = false;
        private bool _validFirst = false;
        private bool _validLast = false;

        public override string RNumber
        {
            get { return rNumber; }

            set
            {
                SetProperty(ref rNumber, value);
                if(!_rNumRegex.IsMatch(value))
                {
                    _validRNumber = false;
                    RNumberError = "Please enter a valid R#";
                }
                else
                {
                    _validRNumber = true;
                    RNumberError = "";
                }
                UpdateCanPrint();
            }
        }

        public override string FirstName
        {
            get { return base.FirstName; }
            set
            {
                base.FirstName = value;
                if(string.IsNullOrWhiteSpace(value))
                {
                    _validFirst = false;
                    FirstNameError = "Please enter your first name.";
                }
                else
                {
                    _validFirst = true;
                    FirstNameError = "";
                }
                UpdateCanPrint();
            }
        }

        public override string LastName
        {
            get { return base.LastName; }
            set
            {
                base.LastName = value;
                if(string.IsNullOrWhiteSpace(value))
                {
                    _validLast = false;
                    LastNameError = "Please enter a valid last name.";
                }
                else
                {
                    _validLast = true;
                    LastNameError = "";
                }
                UpdateCanPrint();
            }
        }

        private void UpdateCanPrint()
        {
            if(_validRNumber && _validFirst && _validLast)
            {
                CanPrint = true;
            }
            else
            {
                CanPrint = false;
            }
        }

        private string _firstNameError = "";
        public string FirstNameError
        {
            get { return _firstNameError; }
            set { SetProperty(ref _firstNameError, value); }
        }

        private string _lastNameError = "";
        public string LastNameError
        {
            get { return _lastNameError; }
            set { SetProperty(ref _lastNameError, value); }
        }
    }
}