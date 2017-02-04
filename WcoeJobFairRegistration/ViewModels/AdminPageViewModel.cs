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

        public string csv_FilePath
        {
            get { return _app.csv_FilePath; }
            set { _app.csv_FilePath = value; }
        }

        public string StudentStashPath
        {
            get { return _app.StudentStashPath; }
            set { _app.StudentStashPath = value; }
        }

        private Command _csvfileDialogCommand;
        public Command CSVFileDialogCommand
        {
            get
            {
                return _csvfileDialogCommand ?? (_csvfileDialogCommand = new Command(() =>
                {
                    OpenFileDialog file = new OpenFileDialog();
                    file.ShowDialog();
                    if (file.CheckPathExists)                    
                    {
                        csv_FilePath = file.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Error on File selection");
                    }
                    
                }));
            }
        }
        private Command _studentFileDialogCommand;
        public Command StudentFileDialogCommand
        {
            get
            {
                return _studentFileDialogCommand ?? (_studentFileDialogCommand = new Command(() =>
                {
                    OpenFileDialog file = new OpenFileDialog();
                    file.ShowDialog();
                    if (file.CheckPathExists)
                    {
                        StudentStashPath = file.FileName;
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