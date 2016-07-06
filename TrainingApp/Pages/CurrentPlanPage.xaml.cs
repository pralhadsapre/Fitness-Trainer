using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TrainingApp.Models;
using System.IO.IsolatedStorage;

namespace TrainingApp.Pages
{
    public partial class CurrentPlanPage : PhoneApplicationPage
    {
        PlanSchedule schedule;
        IsolatedStorageSettings isolatedSettings = IsolatedStorageSettings.ApplicationSettings;
        PlanCurrent currentPlan;
        bool logPanelOpen = false;

        public CurrentPlanPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
            
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(App.fileName))
                currentPlan = App.serializer.LoadPlan();
            else
            {
                schedule = Utilities.FilesHandler.GetPlanSchedule(App.planSelected.FileName);
                currentPlan = new PlanCurrent(schedule, App.plansInfo.ElementAt(App.index), App.marathonDate);
                App.serializer.SavePlan(currentPlan);
            }
            currentPlan.PrepareFiveDays();
            LayoutRoot.DataContext = currentPlan;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (logPanelOpen)
            {
                LogGrid.Visibility = Visibility.Collapsed;
                PlanPanel.Visibility = Visibility.Visible;
                logPanelOpen = false;
            }
            else
            {                
            }

            //if (isolatedSettings.Contains("CurrentPlan"))
            //    isolatedSettings.Remove("CurrentPlan");
        }

        private void OnTapLog(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PlanPanel.Visibility = Visibility.Collapsed;
            LogGrid.Visibility = Visibility.Visible;
            logPanelOpen = true;                        
        }

        private void LogDetails(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (String.IsNullOrEmpty(Distance.Text) || String.IsNullOrEmpty(Duration.Text))
                MessageBox.Show("Please enter all details");
            else
            {
                currentPlan.LogData(double.Parse(Distance.Text), double.Parse(Duration.Text));
                PlanPanel.Visibility = Visibility.Visible;
                LogGrid.Visibility = Visibility.Collapsed;
                LayoutRoot.DataContext = null;
                currentPlan.PrepareFiveDays();
                LayoutRoot.DataContext = currentPlan;
                logPanelOpen = false;
                App.serializer.SavePlan(currentPlan);
            }
        }

        private void OnTapChangePlan(object sender, EventArgs e)
        {
            IsolatedStorageFile.GetUserStoreForApplication().DeleteFile(App.fileName);
            NavigationService.Navigate(new Uri("/Pages/MainPage.xaml", UriKind.Relative));
            NavigationService.RemoveBackEntry();
        }
    }
}