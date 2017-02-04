using System.Text.RegularExpressions;

namespace WcoeJobFairRegistration.ViewModels
{
    class ManualStudentViewModel : StudentPageViewModel
    {
        /// <summary>
        /// Regular expression for validating R#s
        /// </summary>
        private Regex _rNumRegex = new Regex(@"^\d{8}");

        public override string RNumber
        {
            get { return rNumber; }

            set
            {
                SetProperty(ref rNumber, value);
                if(!_rNumRegex.IsMatch(value))
                {
                    RNumberError = "Please enter a valid R#";
                }
                else
                {
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
                UpdateCanPrint();
            }
        }

        public override string LastName
        {
            get { return base.LastName; }
            set
            {
                base.LastName = value;
                UpdateCanPrint();
            }
        }

        private void UpdateCanPrint()
        {
            if(_rNumRegex.IsMatch(RNumber) && !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrWhiteSpace(LastName))
            {
                CanPrint = true;
            }
            else
            {
                CanPrint = false;
            }
        }
    }
}