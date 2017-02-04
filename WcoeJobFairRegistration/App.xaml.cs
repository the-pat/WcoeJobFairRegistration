using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Lazy<IStudentRepository> _studentRepository = new Lazy<IStudentRepository>(() =>
        {
            var result = new StudentRepository();
            result.EnsureInitialized().Wait();
            return result;
        }, true);
        public IStudentRepository StudentRepository => _studentRepository.Value;

        private Lazy<IEmployerRepository> _employeeRepository = new Lazy<IEmployerRepository>(() =>
        {
            var result = new EmployerRepository();
            result.EnsureInitialized().Wait();
            return result;
        }, true);
        public IEmployerRepository EmployeeRepository => _employeeRepository.Value;

        private Lazy<IPrintService> _printService = new Lazy<IPrintService>(() => new DymoService(), true);
        public IPrintService PrintService => _printService.Value;

        public bool IsManualEntry { get; set; }

        public NavigationService NavigationService { get; private set; }

        public string StudentCsvFilePath { get; internal set; } = string.Empty;

        public string ReportingFolderPath { get; internal set; } = string.Empty;

        protected override void OnNavigated(NavigationEventArgs e)
        {
            base.OnNavigated(e);

            Page page = e.Content as Page;
            if(page != null)
            {
                NavigationService = page.NavigationService;
            }
        }
    }
}