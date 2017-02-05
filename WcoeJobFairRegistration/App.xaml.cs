using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Newtonsoft.Json.Linq;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Services;

namespace WcoeJobFairRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string PathFileName = "paths.json";

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
        public IEmployerRepository EmployerRepository
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

        public string StudentCsvFilePath { get; internal set; } = "";
        public string ReportingFolderPath { get; internal set; } = "";

        protected override void OnNavigated(NavigationEventArgs e)
        {
            base.OnNavigated(e);

            Page page = e.Content as Page;
            if(page != null)
            {
                NavigationService = page.NavigationService;
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await LoadPaths();
        }

        protected Task LoadPaths()
        {
            return Task.Run(async () =>
            {
                try
                {
                    using(var reader = new StreamReader(new FileStream(PathFileName, FileMode.Open)))
                    {
                        var content = await reader.ReadToEndAsync();
                        var graph = JObject.Parse(content);
                        var exportPath = graph["export_path"].Value<string>();
                        if(!string.IsNullOrWhiteSpace(exportPath))
                        {
                            ReportingFolderPath = exportPath;
                        }
                    }
                }
                catch(Exception)
                {
                    // Silently ignore. Will use default path
                }
            });
        }

        public Task<bool> SavePaths()
        {
            return Task.Run(async () =>
            {
                try
                {
                    using(var writer = new StreamWriter(new FileStream(PathFileName, FileMode.Create)))
                    {
                        var graph = new JObject();
                        graph["export_path"] = ReportingFolderPath;
                        await writer.WriteAsync(graph.ToString());
                    }

                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            });
        }
    }
}