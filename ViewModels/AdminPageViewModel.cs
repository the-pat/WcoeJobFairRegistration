using Microsoft.Win32;
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

        public string FilePath
        {
            get { return _app.FilePath; }
            set { _app.FilePath = value; }
        }



        private Command _fileDialogCommand;
        public Command FileDialogCommand
        {
            get
            {
                return _fileDialogCommand ?? (_fileDialogCommand = new Command(() =>
                {
                    OpenFileDialog file = new OpenFileDialog();
                    file.ShowDialog();
                    if (file.CheckPathExists)                    
                    {
                        _app.FilePath = file.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Error on File selection");
                    }
                    
                }));
            }
        }
    }
}