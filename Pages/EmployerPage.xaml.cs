using System;
using System.Windows;
using System.Windows.Controls;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.Models;
using WcoeJobFairRegistration.Services;
using WcoeJobFairRegistration.ViewModels;

namespace WcoeJobFairRegistration.Pages
{
    /// <summary>
    /// Interaction logic for EmployerPage.xaml
    /// </summary>
    public partial class EmployerPage : Page
    {
        public EmployerPage()
        {
            InitializeComponent();
            DataContext = new EmployerPageViewModel();
        }   
    }
}
