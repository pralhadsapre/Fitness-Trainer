using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

namespace TrainingApp.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(App.fileName))
            {
                DummyPanel.Visibility = Visibility.Visible;
                DetailsPanel.Visibility = Visibility.Collapsed;
                NavigationService.Navigate(new Uri("/Pages/CurrentPlanPage.xaml", UriKind.Relative));                
            }
            else
            {
                DummyPanel.Visibility = Visibility.Collapsed;
                DetailsPanel.Visibility = Visibility.Visible;
            }            
        }

        private void OnTapViewPlans(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.marathonDate = (DateTime)MarathonDate.Value;
            NavigationService.Navigate(new Uri("/Pages/TrainingPlans.xaml?level=" + RunnerLevel.SelectedIndex.ToString() + "&date=" + MarathonDate.Value.ToString(), UriKind.Relative));
        }
    }
}