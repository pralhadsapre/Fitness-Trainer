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

namespace TrainingApp.Pages
{
    public partial class TrainingPlans : PhoneApplicationPage
    {
        DateTime marathonDate;
        
        public TrainingPlans()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            marathonDate = DateTime.Parse(NavigationContext.QueryString["date"]);
            int index = int.Parse(NavigationContext.QueryString["level"]);
            App.plansInfo = Utilities.FilesHandler.GetAvailablePlans();
            App.plansInfo.ElementAt(index).Type += " (Recommended for you)";
            PlansList.ItemsSource = App.plansInfo;
        }

        private void OnTapPlan(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int index = ((PlanInfo)((StackPanel)sender).DataContext).Index;
            NavigationService.Navigate(new Uri("/Pages/PlanDetails.xaml?index=" + index.ToString(), UriKind.Relative));
        }
    }
}