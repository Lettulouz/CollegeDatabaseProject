using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using CollegeDatabaseProject;
using CollegeDatabaseProject.Stores;
using CollegeDatabaseProject.ViewModels;
using CollegeDatabaseProject.Models;
using CollegeDatabaseProject.Services;
using CollegeDatabaseProject.Stores;
using CollegeDatabaseProject.ViewModels;
using Timer = System.Threading.Timer;

namespace CollegeDatabaseProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateMainViewModel();
            _navigationStore.TopBarViewModel = new TopBarViewModel(0);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore),
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private static HomePageViewModel CreateMainViewModel()
        {
            return new HomePageViewModel();
        }
        
    }
    
}