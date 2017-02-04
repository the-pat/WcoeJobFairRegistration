using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WcoeJobFairRegistration.ViewModels
{
    public class AdminPageViewModel : ObservableObject
    {
        private readonly App _app;

        public AdminPageViewModel()
        {
            _app = (Application.Current as App);
        }

        public bool IsManualEntry
        {
            get { return _app.IsManualEntry; }
            set { _app.IsManualEntry = value; }
        }
    }
}