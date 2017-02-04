using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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

        public string JobGridCsvFilePath
        {
            get { return _app.StudentCsvFilePath; }
            set { _app.StudentCsvFilePath = value; }
        }

        public string ReportingFolderPath
        {
            get { return _app.ReportingFolderPath; }
            set { _app.ReportingFolderPath = value; }
        }

        private Command _csvfileDialogCommand;
        public Command CsvFileDialogCommand
        {
            get
            {
                return _csvfileDialogCommand ?? (_csvfileDialogCommand = new Command(() =>
                {
                    var file = new OpenFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                    };
                    file.ShowDialog();

                    if (file.CheckPathExists)
                    {
                        JobGridCsvFilePath = file.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Error on File selection");
                    }
                }));
            }
        }

        private Command _reportingFolderDialogCommand;
        public Command ReportingFolderDialogCommand
        {
            get
            {
                return _reportingFolderDialogCommand ?? (_reportingFolderDialogCommand = new Command(() =>
                {
                    var folder = new FolderBrowserDialog { ShowNewFolderButton = true };

                    folder.ShowDialog();
                    if (!string.IsNullOrWhiteSpace(folder.SelectedPath))
                    {
                        ReportingFolderPath = folder.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("Error on Folder selection");
                    }
                }));
            }
        }
    }
}