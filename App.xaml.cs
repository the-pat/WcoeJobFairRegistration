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
        public IStudentRepository StudentRepository
        {
            get { return _studentRepository.Value; }
        }

        private Lazy<IEmployerRepository> _employeeRepository = new Lazy<IEmployerRepository>(() =>
        {
            var result = new EmployerRepository();
            result.EnsureInitialized().Wait();
            return result;
        }, true);
        public IEmployerRepository EmployeeRepository
        {
            get { return _employeeRepository.Value; }
        }

        private Lazy<IPrintService> _printService = new Lazy<IPrintService>(() => new DymoService(), true);
        public IPrintService PrintService
        {
            get { return _printService.Value; }
        }

        public bool IsManualEntry { get; set; }

        private NavigationService _navigationService;
        public NavigationService NavigationService
        {
            private set { _navigationService = value; }
            get { return _navigationService; }
        }

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