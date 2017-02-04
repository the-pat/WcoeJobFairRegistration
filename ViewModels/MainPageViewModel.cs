using System.Windows;
using WcoeJobFairRegistration.Pages;

namespace WcoeJobFairRegistration.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        private Command _gotoStudentPageCommand;
        public Command GotoStudentPageCommand
        {
            get
            {
                return _gotoStudentPageCommand ?? (_gotoStudentPageCommand = new Command(() =>
                {
                    GotoStudentPageCommand.ChangeCanExecute();

                    var app = (Application.Current as App);
                    if (app.IsManualEntry) app.NavigationService.Navigate(new ManualStudentPage());
                    else app.NavigationService.Navigate(new StudentPage());

                    GotoStudentPageCommand.ChangeCanExecute();
                }));
            }
        }

        private Command _gotoEmployeePageCommand;
        public Command GotoEmployeePageCommand
        {
            get
            {
                return _gotoEmployeePageCommand ?? (_gotoEmployeePageCommand = new Command(() =>
                {
                    GotoEmployeePageCommand.ChangeCanExecute();
                    (Application.Current as App).NavigationService.Navigate(new EmployerPage());
                    GotoEmployeePageCommand.ChangeCanExecute();
                }));
            }
        }

        private Command _gotoAdminPageCommand;
        public Command GotoAdminPageCommand
        {
            get
            {
                return _gotoAdminPageCommand ?? (_gotoAdminPageCommand = new Command(() =>
                {
                    GotoAdminPageCommand.ChangeCanExecute();
                    (Application.Current as App).NavigationService.Navigate(new AdminPage());
                    GotoAdminPageCommand.ChangeCanExecute();
                }));
            }
        }
    }
}