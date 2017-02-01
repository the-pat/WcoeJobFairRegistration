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
                    (Application.Current as App).NavigationService.Navigate(new StudentPage());
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
    }
}