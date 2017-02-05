using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace WcoeJobFairRegistration.ViewModels
{
    public class AdminPageViewModel : ObservableObject
    {
        private readonly App _app;

        public AdminPageViewModel()
        {
            _app = (Application.Current as App);
            ReportingFolderPath = _app.ReportingFolderPath;
        }

        public bool IsManualEntry
        {
            get { return _app.IsManualEntry; }
            set { _app.IsManualEntry = value; }
        }

        private string _jobGridCsvFilePath = "";
        public string JobGridCsvFilePath
        {
            get { return _jobGridCsvFilePath; }
            set { SetProperty(ref _jobGridCsvFilePath, value); }
        }

        private string _reportingFolderPath = "";
        public string ReportingFolderPath
        {
            get { return _reportingFolderPath; }
            set
            {
                if(SetProperty(ref _reportingFolderPath, value))
                {
                    _app.ReportingFolderPath = value;
                }
            }
        }

        private Command _csvfileDialogCommand;
        public Command CsvFileDialogCommand
        {
            get
            {
                return _csvfileDialogCommand ?? (_csvfileDialogCommand = new Command(() =>
                {
                    var fileDialog = new OpenFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                        RestoreDirectory = true
                    };
                    var result = fileDialog.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        JobGridCsvFilePath = fileDialog.FileName;
                        CanImportData = true;
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

                    var result = folder.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        ReportingFolderPath = folder.SelectedPath;
                        CanUpdatePath = true;
                    }
                }));
            }
        }

        private bool _canImportData = false;
        public bool CanImportData
        {
            get { return !string.IsNullOrWhiteSpace(JobGridCsvFilePath); }
            set { SetProperty(ref _canImportData, value, onChanged: () => ImportDataCommand.ChangeCanExecute()); }
        }

        private Command _importDataCommand;
        public Command ImportDataCommand
        {
            get { return _importDataCommand ?? (_importDataCommand = new Command(async () => await ExecuteLoadData(), () => CanImportData)); }
        }

        private async Task ExecuteLoadData()
        {
            CanImportData = false;

            var result = await _app.StudentRepository.Load(JobGridCsvFilePath);
            if(result)
            {
                MessageBox.Show("Data successfully imported");
            }

            CanImportData = true;
        }

        private bool _canUpdatePath = false;
        public bool CanUpdatePath
        {
            get { return _canUpdatePath; }
            set { SetProperty(ref _canUpdatePath, value, onChanged: () => UpdatePathCommand.ChangeCanExecute()); }
        }

        private Command _canUpdatePathCommand;
        public Command UpdatePathCommand
        {
            get { return _canUpdatePathCommand ?? (_canUpdatePathCommand = new Command(async () => await ExecuteUpdatePath(), () => CanUpdatePath)); }
        }

        private async Task ExecuteUpdatePath()
        {
            _app.ReportingFolderPath = ReportingFolderPath;
            var result = await _app.SavePaths();
            if(result)
            {
                MessageBox.Show("Export path updated.");
            }
            else
            {
                MessageBox.Show("Error updating export path");
            }
        }
    }
}