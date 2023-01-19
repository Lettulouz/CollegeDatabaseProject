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
using MySqlConnector;
using Timer = System.Threading.Timer;

namespace CollegeDatabaseProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private static NavigationStore _navigationStore;
        string _connectionString = "";
        private string _databaseVersion = "8.0.30";
        private static HomePageViewModel _homePageViewModel;
        public App()
        {
           // MySqlConnection con = new MySqlConnection(DbConnection.getDbString());
          //  con.Open();
           // var stm = "Select nazwaPanstwa from panstwo";
           // var cmd = new MySqlCommand(stm, con);
           // var output = cmd.ExecuteReader();
            
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _connectionString = DbConnection.getDbString();
            
            _navigationStore.CurrentViewModel = CreateMainViewModel();
            _navigationStore.TopBarViewModel = new TopBarViewModel(2);
            _navigationStore.SideBarViewModel = new SideBarViewModel(_homePageViewModel);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore),
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private static HomePageViewModel CreateMainViewModel()
        {
            _homePageViewModel = new HomePageViewModel();
            return _homePageViewModel;
        }
        
        
    }
    
}